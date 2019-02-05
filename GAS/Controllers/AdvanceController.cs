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
       [RoutePrefix("api/Advance")]
    public class AdvanceController : ApiController
    {
        // GET: api/Advance/All
           [Route("Organization/{OrgID}/Status/{Status}")]
           [HttpGet]
           public IEnumerable<ViewAdvance> GetAll(int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];
                if (Status == "Show All")
                    {
                        ints1 = new String[5];
                        ints1[0] = "Paid";
                        ints1[1] = "Added";
                        ints1[2] = "Initiated";
                        ints1[3] = "Submitted";
                        ints1[4] = "Approved";
                    }
                    else if (Status == "Open")
                    {
                        ints1 = new String[4];
                        ints1[0] = "Submitted";
                        ints1[1] = "Approved";
                    }
                    else if (Status == "Closed")
                    {
                        ints1 = new String[5];
                        ints1[0] = "Paid";
                    }
                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewAdvances
                               orderby ex.ActivityID ascending
                               where ex.AdvanceStatus != "Deleted" && ex.OrgID == OrgID && ints1.Contains(ex.AdvanceStatus)
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

           // GET: api/Advance/ByActivity/5
        [Route("Organization/{OrgID}/Activity/{id}")]
        [HttpGet]
           public IEnumerable<ViewAdvanceItemName> GetByActivity(int id)
        {
            try
            {
               var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewAdvanceItemNames
                               where ex.ActivityID == id && ex.Status != "Deleted"
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

           // GET: api/Advance/ByApprover/5
        [Route("Organization/{OrgID}/Approver/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<ViewAdvance> GetByApprover(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];
                if (Status == "Show All")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Approved";
                }
                else if (Status == "Open")
                {
                    ints1 = new String[4];
                    ints1[0] = "Submitted";
                    ints1[1] = "Approved";
                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                }
                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewAdvances
                               orderby ex.AdvanceModifiedDate ascending
                               where ex.Approver == id && ex.OrgID == OrgID && ex.AdvanceStatus != "Deleted" && ints1.Contains(ex.AdvanceStatus)
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // POST: api/Advance/Add
           [Route("Add")]
           [HttpPost]
        public HttpResponseMessage PostAdd([FromBody]AdvanceItem ai)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();
                ai.CreationDate = DateTime.UtcNow;
                if (ai != null)
                {
                    var id = ctx.AdvanceItems.Add(ai);

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

        // PUT: api/Advance/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Advance/5
        public void Delete(int id)
        {
        }
    }
}
