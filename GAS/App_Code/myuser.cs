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

namespace GAS.App_Code
{
    public class myuser
    {
        
        private static string EncryptPassword(string userID, string userPWD)
        {

            MD5CryptoServiceProvider encoder = new MD5CryptoServiceProvider();

            byte[] bytDataToHash = Encoding.UTF8.GetBytes(userID + userPWD);
            byte[] bytHashValue = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(bytDataToHash);
            return BitConverter.ToString(bytHashValue).Replace("-", "");
        }

    
        private static void SendMailToUsers(string UserLogin, string password, string EmailID, string FirstName)
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
    }
    }
