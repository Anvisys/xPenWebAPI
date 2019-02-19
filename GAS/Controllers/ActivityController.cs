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
    [RoutePrefix("api/Activity")]
    public class ActivityController : ApiController
    {
        // GET: api/Activity/ByOrg/id
        [Route("Organization/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<NewViewActivity> GetByOrg(int id, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

             

                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Approved";
                }
                else if (Status == "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Added";
                    ints1[1] = "Initiated";
                    ints1[2] = "Submitted";
                    ints1[3] = "Approved";
                    ints1[4] = "Quick";
                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                }
                var ctx = new GASEntities();
                var actData = (from tr in ctx.NewViewActivities
                               where tr.OrgID == id && ints1.Contains(tr.ActivityStatus)
                               orderby tr.ActivityID descending
                               select tr).Take(20);
                return actData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Activity/Summary
        [Route("Organization/{id}/Summary")]
        [HttpGet]
        public IEnumerable<ViewActivitySummary> GetSummary(int id)
        {
            try
            {
                var ctx = new GASEntities();
                var actData = (from tr in ctx.ViewActivitySummaries
                               where tr.OrgID == id
                                select tr);
                return actData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //// GET: api/Activity/Summary
        //[Route("Organization/{OrgID}/Dashboard/Employee/{id}")]
        //[HttpGet]
        //public ViewActivityDashboard GetDashboard(int OrgID, int id)
        //{
        //    try
        //    {
        //        var ctx = new GASEntities();
        //        var actData = (from act in ctx.ViewActivityDashboards
        //                       where act.EmployeeID == id 
        //                       select act).First();
        //        return actData;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}


        // GET: api/Activity/ByID/5
        [Route("Organization/{OrgID}/Activity/{id}")]
        [HttpGet]
        public ViewActivity GetByActivityID(int id, int OrgID)
        {
            try
            {
                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewActivities
                               where ex.ActivityID == id && ex.OrgID == OrgID
                               select ex).First();
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Activity/5
        [Route("Organization/{OrgID}/Admin/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<NewViewActivity> GetByAdmin(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

                var ctx = new GASEntities();

                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Quick";
                    ints1[5] = "Approved";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else if (Status == "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Added";
                    ints1[1] = "Initiated";
                    ints1[2] = "Submitted";
                    ints1[3] = "Approved";
                    ints1[4] = "Quick";
                    var actData = (from tr in ctx.NewViewActivities
                                   where  tr.OrgID == OrgID && ints1.Contains(tr.ActivityStatus) && tr.Settlement == 0
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;

                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.OrgID == OrgID && (tr.Settlement > 0 || tr.ActivityStatus == "Closed")
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else
                {
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Activity/5
        [Route("Organization/{OrgID}/Approver/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<NewViewActivity> GetByApprover(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

                var ctx = new GASEntities();
                
                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Approved";
                    ints1[5] = "Quick";

                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.Approver == id && tr.OrgID == OrgID 
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else if (Status == "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Submitted";
                    ints1[1] = "Approved";
                    ints1[2] = "Quick";
                    ints1[3] = "Added";
                    ints1[4] = "Initiated";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.Approver == id && tr.OrgID == OrgID && ints1.Contains(tr.ActivityStatus) && tr.Settlement==0
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                   var actData = (from tr in ctx.NewViewActivities
                                   where tr.Approver == id && tr.OrgID == OrgID && (tr.Settlement >0|| tr.ActivityStatus=="Closed")
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }

                else
                {
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.Approver == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;

                }
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Activity/5
        [Route("Organization/{OrgID}/Employee/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<NewViewActivity> GetByEmployee(int id, int OrgID, String Status )
        {
            try
            {
                String[] ints1 = new String[0]; 
                
                var ctx = new GASEntities();
                
                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Quick";
                    ints1[5] = "Approved";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else if (Status== "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Added";
                    ints1[1] = "Initiated";
                    ints1[2] = "Submitted";
                    ints1[3] = "Approved";
                    ints1[4] = "Quick";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID && ints1.Contains(tr.ActivityStatus) && tr.Settlement == 0
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;

                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID && (tr.Settlement > 0 || tr.ActivityStatus == "Closed")
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else
                {
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        // GET: api/Activity/5
        [Route("Organization/{OrgID}/Employee/{id}")]
        [HttpGet]
        public IEnumerable<NewViewActivity> GetNewByEmployee(int id, int OrgID)
        {
            try
            {
                
                var ctx = new GASEntities();
         
                var actData = (from tr in ctx.NewViewActivities
                               where tr.EmployeeID == id && tr.OrgID == OrgID 
                               orderby tr.ActivityID descending
                               select tr);

                return actData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Activity/5
        [Route("Organization/{OrgID}/Project/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<NewViewActivity> GetByProject(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

                var ctx = new GASEntities();

                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Quick";
                    ints1[5] = "Approved";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else if (Status == "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Added";
                    ints1[1] = "Initiated";
                    ints1[2] = "Submitted";
                    ints1[3] = "Approved";
                    ints1[4] = "Quick";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.ProjectID == id && tr.OrgID == OrgID && ints1.Contains(tr.ActivityStatus) && tr.Settlement == 0
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;

                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.ProjectID == id && tr.OrgID == OrgID && (tr.Settlement > 0 || tr.ActivityStatus == "Paid")
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else
                {
                    var actData = (from tr in ctx.NewViewActivities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: api/Expense/PostNew
        [Route("CreateActivity")]
        [HttpPost]
        public HttpResponseMessage PostCreateActivity([FromBody]QuickActivityModel activity)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();

                if (activity != null)
                {
                    activity.CreationDate = DateTime.UtcNow;
                    var e = ctx.Activities.Add(
                        new Activity{
                        ActivityName = activity.ActivityName,
                        CreatedBy = activity.CreatedBy,
                        EmployeeID = activity.EmployeeID,
                        ProjectID = activity.ProjectID,
                        ActivityDescription = activity.ActivityDescription,
                        OrgID = activity.OrgID,
                        CreationDate = DateTime.UtcNow,
                        ApproverID = activity.ApproverID,
                        SelectedRow = false
                        }
                        );
                    ctx.SaveChanges();
                    ctx.ExpenseItems.Add(new ExpenseItem
                    {
                        ActivityID = e.ActivityID,
                        ExpenseAmount = activity.ExpenseAmount,
                        ExpenseDate = DateTime.UtcNow,
                        ItemName = e.ActivityName,
                        ReceiveAmount = 0,
                        ExpenseDescription = activity.ActivityDescription,
                        Action = activity.ActivityStatus,
                        SelectedRow = false
                    });

                    ctx.SaveChanges();
                    resp = "{\"Response\":\"OK\",\"ExpenseID\":" + e.ActivityID + "}";
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



        // PUT: api/Activity/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Activity/5
        public void Delete(int id)
        {
        }
    }
}
