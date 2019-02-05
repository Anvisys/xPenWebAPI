using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using GAS.Models;

namespace GAS.Infrastructure
{
    public class UserToken
    {
        private const string _alg = "HmacSHA256";
        private const string _salt = "rz8LuOtFBXphj9WQfvFh"; // Generated at https://www.random.org/strings
        private const int _expirationMinutes = 10;
        public static string GenerateToken(string username, string password, string ip, string userAgent, long ticks)
        {          
            string hashPwd = GetHashedPassword(password);
            return GenerateTokenUsingHashPassword(username, hashPwd, ip, userAgent, ticks);

        }

        public static string GenerateTokenUsingHashPassword(string username, string hashPassword, string ip, string userAgent, long ticks)
        {
            try
            {
                string hash = string.Join(":", new string[] { username, ip, userAgent, ticks.ToString() });
                string hashLeft = "";
                string hashRight = "";
                using (HMAC hmac = HMACSHA256.Create(_alg))
                {
                    hmac.Key = Encoding.UTF8.GetBytes(hashPassword);
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                    hashLeft = Convert.ToBase64String(hmac.Hash);
                    hashRight = string.Join(":", new string[] { username, ticks.ToString() });
                }
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Join(":", hashLeft, hashRight)));
            }
            catch (Exception ex)
            {
                Utility.log(DateTime.UtcNow.ToShortTimeString() + ": GenerateTokenUsingHashPassword : " + ex.Message);
                return "";
            
            }
        }
        public static string GetHashedPassword(string password)
        {
            try
            {
                string key = string.Join(":", new string[] { password, _salt });
                using (HMAC hmac = HMACSHA256.Create(_alg))
                {
                    // Hash the key.
                    hmac.Key = Encoding.UTF8.GetBytes(_salt);
                    hmac.ComputeHash(Encoding.UTF8.GetBytes(key));
                    var hashPwd = Convert.ToBase64String(hmac.Hash);
                    return hashPwd;
                }
            }
            catch (Exception ex)
            {
                Utility.log(DateTime.UtcNow.ToShortTimeString() + ": GetHashedPassword : " + ex.Message);
                return "";
            }
        }

      
        public static bool IsTokenValid(string token, string ip, string userAgent)
        {
            bool result = false;
            try
            {
                // Base64 decode the string, obtaining the token:username:timeStamp.
                string key = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                // Split the parts.
                string[] parts = key.Split(new char[] { ':' });
                if (parts.Length == 3)
                {
                    // Get the hash message, username, and timestamp.
                    string hash = parts[0];
                    string username = parts[1];
                    long ticks = long.Parse(parts[2]);
                    DateTime timeStamp = new DateTime(ticks);
                    // Ensure the timestamp is valid.
                    bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
                    if (!expired)
                    {
                        String passwordEnc = string.Empty;
                        string hashLeft = string.Empty;
                        using (HMAC hmac = HMACSHA256.Create(_alg))
                        {
                           hmac.ComputeHash(Encoding.UTF8.GetBytes(hash));
                           hashLeft = Convert.ToBase64String(hmac.Hash);
                        }
                        using (var ctx = new GASEntities())
                        {
                            
                            if (username.All(char.IsDigit))
                            {
                                passwordEnc = (from u in ctx.Users
                                         where u.UserMobile == username
                                         select u.Password.ToString()).First();
                            }
                            else
                            {
                                passwordEnc = (from u in ctx.Users
                                               where u.UserEmail == username
                                               select u.Password.ToString()).First();
                            }
                            
                        }
                        if (!string.IsNullOrEmpty(passwordEnc))
                        {
                            string computedToken = GenerateTokenUsingHashPassword(username, passwordEnc, ip, userAgent, ticks);
                            // Compare the computed token with the one supplied and ensure they match.
                            result = (token == computedToken);
                        }
                       
                      
                    }
                }
            }
            catch
            {
            }
            return result;
        }
    }
}