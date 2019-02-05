using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.Http;
using System.Web.Http;


namespace GAS.Models
{
    public class Utility
    {
        private static string FileName = System.Web.HttpContext.Current.Server.MapPath(@"~\Content\LogFile.txt");
        public static string EncryptPassword(string userID, string userPWD)
        {

            MD5CryptoServiceProvider encoder = new MD5CryptoServiceProvider();

            byte[] bytDataToHash = Encoding.UTF8.GetBytes(userID + userPWD);
            byte[] bytHashValue = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(bytDataToHash);
            return BitConverter.ToString(bytHashValue).Replace("-", "");
        }


        public static void SendMailToUsers(string UserLogin, string password, string EmailID, string FirstName)
        {
            try
            {
                string URI = "http://www.kevintech.in/NewAccountMailer.php";
                WebRequest request = WebRequest.Create(URI);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string postData = "ToMail=" + EmailID + "&Password=" + password + "&Username=" + UserLogin + "&FirstName=" + FirstName;
                Stream dataStream = request.GetRequestStream();
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse response = request.GetResponse();

                StreamReader reader = new StreamReader(response.GetResponseStream());
                reader.Close();
                dataStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {

            }

        }
    
        public static String RandomPassword()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(4, true));
            builder.Append(RandomString(2, false));
            builder.Append(RandomNumber(100, 999));
            //return builder.ToString();
            return "Password@123";
        }

        // Generate a random string with a given size  
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        } 

        // Generate a random number between two numbers  
        public static int RandomNumber(int min, int max)  
        {  
            Random random = new Random();  
            return random.Next(min, max);  
        }

        
        public static void log(String Message)
        {
            StreamWriter errWriter = new StreamWriter(FileName, true);
            errWriter.WriteLine(Message);

            errWriter.Close();
        }

        public static string GetIP(HttpRequestMessage request)
        {
            if (request.Properties.ContainsKey("MS_HttpContext"))
            {
                return ((HttpContextWrapper)request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
            //else if (request.Properties.ContainsKey(RemoteEndpointMessageProperty.Name))
            //{
            //    RemoteEndpointMessageProperty prop = (RemoteEndpointMessageProperty)request.Properties[RemoteEndpointMessageProperty.Name];
            //    return prop.Address;
            //}
            else if (HttpContext.Current != null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                return string.Empty;
            }
        }

        public static string GetUserAgent(HttpRequestMessage request)
        {
            return request.Headers.UserAgent.ToString();
        }
    
    }
}