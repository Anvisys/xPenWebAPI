using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public String UserName { get; set; }
        public String UserEmail { get; set; }
        public String UserMobile { get; set; }
        public String UserLogin { get; set; }
        public String UserRole { get; set; }
        public int OrgId { get; set; }
        public String OrgName { get; set; }

        public String AccountType { get; set; }

        public string UserToken { get; set; }
    }
}