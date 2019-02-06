using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;

namespace GAS.Controllers
{
      [EnableCors(origins: "*", headers: "*", methods: "*")]
      [RoutePrefix("api/Organization")]
    public class OrganizationController : ApiController
    {
        // Get List of Organizations enrolled
          public IEnumerable<ViewOrganization> Get()
        {
            try
            {
                var ctx = new GASEntities();
                var accData = (from tr in ctx.ViewOrganizations
                               select tr).Take(20);
                return accData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Get organization details by Organization Id
          public ViewOrganization Get(int id)
        {
            try
            {
                var ctx = new GASEntities();
                var accData = (from tr in ctx.ViewOrganizations
                               where tr.OrganizationID == id
                               select tr).First();
                return accData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Add new Organization
        [Route("New")]
        [HttpPost]
        public HttpResponseMessage PostNew([FromBody]Organization org)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();
                if (org != null)
                {
                    org.StartDate = DateTime.UtcNow;
                    ctx.Organizations.Add(org);
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


        // Update Organization Status and Number
        [Route("Update")]
        [HttpPost]
        public HttpResponseMessage PostUpdate([FromBody]Organization org)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();
                if (org != null)
                {
                    var reg = (from o in ctx.Organizations
                               where o.OrganizationID == org.OrganizationID
                               select o).First();
                    reg.Status = org.Status;
                    reg.OrganizationNumber = org.OrganizationNumber;
                    ctx.SaveChanges();
                    resp = "{\"Response\":\"OK\",\"ID\":" + org.OrganizationID + "}";
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



        // PUT: api/Organization/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Organization/5
        public void Delete(int id)
        {
        }
    }
}
