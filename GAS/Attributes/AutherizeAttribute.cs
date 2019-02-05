using GAS.Infrastructure;
using GAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace GAS.Attributes
{
    public class APIAuthorizeAttribute : AuthorizeAttribute
    {
        private const bool _autherizationRequired = false;
        private const string _securityToken = "token"; // Name of the url parameter.      

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
           
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            if (!_autherizationRequired)
            {
                return true;
            }            
            try
            {
               
                string token = "";
                if (actionContext.Request.Headers.Contains(_securityToken))
                {
                    token = actionContext.Request.Headers.GetValues(_securityToken).First();
                }

                string ip = Utility.GetIP(actionContext.Request);
                string userAgent = Utility.GetUserAgent(actionContext.Request);

                return UserToken.IsTokenValid(token,ip,userAgent);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}