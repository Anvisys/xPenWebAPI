using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using GAS.Models;

namespace GAS.Controllers
{
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [RoutePrefix("api/Account")]
    public class AccountController : ApiController
    {
        // GET: api/Account
       
        public IEnumerable<ViewAccount> GetAll()
        {
            try
            {
                var ctx = new GASEntities();
                var accData = (from tr in ctx.ViewAccounts
                               select tr).Take(10);
                return accData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Account/5
            [Route("Organization/{id}")]
            [HttpGet]
            public IEnumerable<ViewAccount> GetByOrg(int id)
        {
            try
            {
                var ctx = new GASEntities();
                var accData = (from tr in ctx.ViewAccounts
                               where tr.OrgID == id
                               select tr).Take(10).ToList<ViewAccount>();
                return accData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: api/Account
            [ActionName("NewAccount")]
            [HttpPost]
        public HttpResponseMessage PostNewAccount([FromBody]Account acc)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();
                if (acc != null)
                {
                    ctx.Accounts.Add(acc);
                    ctx.SaveChanges();
                    resp = "{\"Response\":\"OK\"}";
                }

            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
            }

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}
