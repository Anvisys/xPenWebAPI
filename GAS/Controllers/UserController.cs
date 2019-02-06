using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using GAS.Models;
using GAS.Infrastructure;


namespace GAS.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/User")]
    public class UserController : ApiController
    {
        GASEntities ctx;


        // get List of All users
        [Route("All")]
        [HttpGet]
        public IEnumerable<UserInfo> Get()
        {
            ctx = new GASEntities();
            try
            {
                var userList = (from u in ctx.Users
                                select new UserInfo
                                {
                                    UserId = u.UserID,
                                    UserLogin = u.UserLogin,
                                    UserName = u.UserName,
                                    UserEmail = u.UserEmail,
                                    UserMobile = u.UserMobile,
                                    UserRole = u.Role,
                                    AccountType = u.SolutionType
                                }
                                    );
                return userList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // get List of All users of an organization
        [Route("Organization/{OrgID}")]
        [HttpGet]
        public IEnumerable<UserInfo> GetByOrg(int OrgID)
        {
            ctx = new GASEntities();
            try
            {
                var userList = (from u in ctx.Users
                                where u.OrganizationID == OrgID
                                select new UserInfo
                                {
                                    UserId = u.UserID,
                                    UserLogin = u.UserLogin,
                                    UserName = u.UserName,
                                    UserEmail = u.UserEmail,
                                    UserMobile = u.UserMobile,
                                    UserRole = u.Role
                                }
                                    );
                return userList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // Get user details by mobile number
        [Route("Mobile/{mobile}")]
        [HttpGet]
        public UserInfo GetByMobile(string mobile)
        {
            ctx = new GASEntities();
            try
            {
                var userList = (from u in ctx.Users
                                where u.UserMobile == mobile
                                select new UserInfo
                                {
                                    UserId = u.UserID,
                                    UserLogin = u.UserLogin,
                                    UserName = u.UserName,
                                    UserEmail = u.UserEmail,
                                    UserMobile = u.UserMobile,
                                    UserRole = u.Role,
                                    OrgId = (int)u.OrganizationID,
                                    OrgName = u.OrgName
                                }
                                    ).First();
                return userList;

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // check if MObile number of email already used
        [Route("IfExist")]
        [HttpPost]
        public HttpResponseMessage PostIfExist([FromBody]User user)
        {
            ctx = new GASEntities();
            bool mail = true;
            bool mobile = true;
            String resp = "{\"Response\":\"Undefine\",\"IsMail\":" + mail + ",\"IsMobile:\"" + mobile + "}";
            try
            {
             
                resp = "{\"Response\":\"OK\",\"IsMail\":\"" + IsEmail(user.UserEmail) + "\",\"IsMobile\":\"" + IsMobile(user.UserMobile) + "\"}";
            }
            catch (Exception ex)
            {

                resp = "{\"Response\":\"Fail\",\"IsMail\":" + false + ",\"IsMobile:\"" + false + "}";
                return null;
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }



        // Add new user
        [Route("AddUser")]
        [HttpPost]
        public HttpResponseMessage PostAddUser([FromBody]User user)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                ctx = new GASEntities();

                bool MobileValid = IsMobile(user.UserMobile);
                bool EmailValid = IsEmail(user.UserEmail);
                if (!MobileValid || !EmailValid)
                {
                    resp = "{\"Response\":\"Invalid\",\"IsMail\":\"" + IsEmail(user.UserEmail) + "\",\"IsMobile\":\"" + IsMobile(user.UserMobile) + "\"}";
                }
                else
                {
                    String password = user.Password;
                    String newPassword = UserToken.GetHashedPassword(user.Password);  //Utility.EncryptPassword(user.UserEmail.ToLower(), user.Password);
                    user.Password = newPassword;
                    ctx = new GASEntities();
                    user.RegisterDate = DateTime.UtcNow;
                    ctx.Users.Add(user);
                    ctx.SaveChanges();
                    Utility.SendMailToUsers(user.UserEmail, password, user.UserEmail, user.UserName);
                    resp = "{\"Response\":\"OK\",\"Id\":"+ user.UserID+"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Exception\"}";

            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // Validate user
        [Route("Organization/{OrgID}/User/{login}/lock/{password}")]
        [HttpGet]
        public UserInfo GetValidate(int OrgID, String login, String password)
        {
            ctx = new GASEntities();
            String Email;
            if (login.All(char.IsDigit))
            { 
                Email = (from u in ctx.Users
                               where u.UserMobile == login
                               select u.UserEmail.ToString()).First();
            
            }
            else
            {
                Email = login;
            }

            try
            {
                String enPassword = UserToken.GetHashedPassword(password); //Utility.EncryptPassword(Email.ToLower(), password);
                var userList = (from u in ctx.Users
                                where u.UserEmail.ToLower() == Email.ToLower() && u.Password == enPassword
                                select new UserInfo
                                {
                                    UserId = u.UserID,
                                    UserLogin = u.UserLogin,
                                    UserName = u.UserName,
                                    UserEmail = u.UserEmail,
                                    UserMobile = u.UserMobile,
                                    UserRole = u.Role,
                                    OrgId = (int)u.OrganizationID,
                                    OrgName = u.OrgName,
                                    AccountType = u.SolutionType
                                }
                                    ).First();

                return userList;

            }
            catch (Exception ex)
            {
                UserInfo user = new UserInfo();
                user.UserName = "Error";
                return user;
            }
        }


        // Update Password
        [Route("Change/UserID/{id}/lock/{pwd}")]
        [HttpGet]
        public HttpResponseMessage GetChange(int id, String pwd)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            ctx = new GASEntities();
            try
            {
                var user = (from u in ctx.Users
                            where u.UserID == id
                            select u).First();
                String newPassword = Utility.EncryptPassword(id.ToString(), pwd);
                user.Password = newPassword;
                ctx.SaveChanges();

                Utility.SendMailToUsers(user.UserEmail, pwd, user.UserEmail, user.UserName);

                resp = "{\"Response\":\"OK\"}";

            }
            catch (Exception ex)
            {
                resp = "{\"Response\":\"Exception\"}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;

        }

        // Forgot Password - create new random password
        [Route("Forgot/Email/{Email}/Mobile/{Mobile}")]
        [HttpGet]
        public HttpResponseMessage GetForgot(String Email, String Mobile)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            ctx = new GASEntities();
            try
            {
                var user = (from u in ctx.Users
                            where u.UserEmail == Email && u.UserMobile == Mobile
                            select u);
                if (user.Count() > 0)
                {
                    var x = user.First();
                    String newPswd = Utility.RandomPassword();
                    String newPassword = Utility.EncryptPassword(x.UserID.ToString(), newPswd);
                    x.Password = newPassword;
                    ctx.SaveChanges();
                    Utility.SendMailToUsers(x.UserEmail, newPassword, x.UserEmail, x.UserName);
                    resp = "{\"Response\":\"OK\"}";
                }
                else
                {
                    resp = "{\"Response\":\"Error\"}";
                }



            }
            catch (Exception ex)
            {
                resp = "{\"Response\":\"Exception\"}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;

        }

        // validate user
        [Route("Validate")]
        [HttpPost]
        public UserInfo PostValidateUser(Login login)
        {
            ctx = new GASEntities();
            String Email;
            if (login.User_Login.All(char.IsDigit))
            {
                Email = (from u in ctx.Users
                         where u.UserMobile == login.User_Login
                         select u.UserEmail.ToString()).First();

            }
            else
            {
                Email = login.User_Login;
            }
            try
            {
                String enPassword = UserToken.GetHashedPassword(login.User_Password); //Utility.EncryptPassword(Email.ToLower(), login.User_Password);

                var userList = (from u in ctx.Users
                                where u.UserLogin.ToLower() == Email.ToLower() && u.Password == enPassword
                                select new UserInfo
                                {
                                    UserId = u.UserID,
                                    UserLogin = u.UserLogin,
                                    UserName = u.UserName,
                                    UserEmail = u.UserEmail,
                                    UserMobile = u.UserMobile,
                                    UserRole = u.Role,
                                    OrgId = (int)u.OrganizationID,
                                    OrgName = u.OrgName,
                                    AccountType = u.SolutionType
                                }
                                    ).First();
                string ip = Utility.GetIP(Request);
                string userAgent = Utility.GetUserAgent(Request);
                long tick = DateTime.UtcNow.Ticks;
                userList.UserToken = UserToken.GenerateToken(login.User_Login, login.User_Password, ip, userAgent, tick);

                return userList;

            }
            catch (Exception ex)
            {
                Utility.log(DateTime.Now.ToShortDateString() + ": PostValidateUser :" + ex.Message);
                UserInfo user = new UserInfo();
                user.UserName = "Error";
                return user;
            }
        }

        // Add Existing user to organization
        [Route("Existing")]
        [HttpPost]
        public HttpResponseMessage PostExistingUser(User existing)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            ctx = new GASEntities();
            try
            {
                var user = (from u in ctx.Users
                            where u.UserMobile == existing.UserMobile
                            select u).First();

                user.Role = existing.Role;
                user.SolutionType = existing.SolutionType;
                user.OrganizationID = existing.OrganizationID;
                user.OrgName = existing.OrgName;
                ctx.SaveChanges();

                resp = "{\"Response\":\"OK\",\"Id\":" + user.UserID + "}";

            }
            catch (Exception ex)
            {
                resp = "{\"Response\":\"Exception\"}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // Edit user 
        [Route("Edit")]
        [HttpPost]
        public HttpResponseMessage PostEdit(User edit)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            ctx = new GASEntities();
            try
            {
                var user = (from u in ctx.Users
                            where u.UserID == edit.UserID
                            select u).First();

                if (user.UserEmail != edit.UserEmail)
                {
                    String newPassword = Utility.EncryptPassword(edit.UserEmail, "Password@123");
                    user.UserEmail = edit.UserEmail;
                    user.Password = newPassword;
                    user.UserMobile = edit.UserMobile;
                    user.UserName = edit.UserName;
                    ctx.SaveChanges();

                }
                else {
                    user.UserMobile = edit.UserMobile;
                    user.UserName = edit.UserName;
                    ctx.SaveChanges();
                }

                resp = "{\"Response\":\"OK\",\"Id\":" + user.UserID + "}";

            }
            catch (Exception ex)
            {
                resp = "{\"Response\":\"Exception\"}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        
        }



        private Boolean IsMobile(String Mobile)
        {
            try {
                var userList = (from u in ctx.Users
                                where u.UserMobile == Mobile
                                select u
                                   );
                if (userList.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false; 
            }

        }

        private bool IsEmail(String Email)
        {
            try
            {
                var userList = (from u in ctx.Users
                                where u.UserEmail == Email
                                select u
                                   );
                if (userList.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
