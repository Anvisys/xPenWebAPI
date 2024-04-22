using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using System.Xml.Linq;
using GAS.Models;
using GAS.ModelsDTO;

namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Activity")]
    public class ActivityController : ApiController
    {
        // GET: api/Activity/ByOrg/id
        [Route("Organization/{id}/Status/{Status}")]
        [HttpGet]
        public IHttpActionResult GetByOrg(int id, String Status)
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
                var ctx = new XPenEntities();
                var actData = (from tr in ctx.NewViewActivities
                               where tr.OrgID == id && ints1.Contains(tr.ActivityStatus)
                               orderby tr.ActivityID descending
                               select tr).Take(20);
                return Ok(actData);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        // GET: api/Activity/Summary
        [Route("Organization/{id}/Summary")]
        [HttpGet]
        public IEnumerable<Activity> GetSummary(int id)
        {
            try
            {
                var ctx = new XPenEntities();
                var actData = (from tr in ctx.Activities
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
        //        var ctx = new XPenEntities();
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
        public Activity GetByActivityID(int id, int OrgID)
        {
            try
            {
                var ctx = new XPenEntities();
                var expData = (from ex in ctx.Activities
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
        public IEnumerable<Activity> GetByAdmin(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

                var ctx = new XPenEntities();

                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Quick";
                    ints1[5] = "Approved";
                    var actData = (from tr in ctx.Activities
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
                    var actData = (from tr in ctx.Activities
                                   where  tr.OrgID == OrgID 
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;

                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    var actData = (from tr in ctx.Activities
                                   where tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else
                {
                    var actData = (from tr in ctx.Activities
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
        public IEnumerable<Activity> GetByApprover(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

                var ctx = new XPenEntities();
                
                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Approved";
                    ints1[5] = "Quick";

                    var actData = (from tr in ctx.Activities
                                   where tr.ApproverID == id && tr.OrgID == OrgID 
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
                    var actData = (from tr in ctx.Activities
                                   where tr.ApproverID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                   var actData = (from tr in ctx.Activities
                                   where tr.ApproverID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).Take(20);
                    return actData;
                }

                else
                {
                    var actData = (from tr in ctx.Activities
                                   where tr.ApproverID == id && tr.OrgID == OrgID
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
        public IEnumerable<ActivityDTO> GetByEmployee(int id, int OrgID, String Status )
        {
            try
            {
                String[] ints1 = new String[0]; 
                
                var ctx = new XPenEntities();
                var actData = (from tr in ctx.Activities
                               where tr.EmployeeID == id && tr.OrgID == OrgID
                               orderby tr.ActivityID descending
                               select tr).ToList();

                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Quick";
                    ints1[5] = "Approved";
                    actData = (from tr in ctx.Activities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();

                }
                else if (Status== "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Added";
                    ints1[1] = "Initiated";
                    ints1[2] = "Submitted";
                    ints1[3] = "Approved";
                    ints1[4] = "Quick";
                    actData = (from tr in ctx.Activities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();

                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    actData = (from tr in ctx.Activities
                                   where tr.EmployeeID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();
                }
                //else
                //{
                //    actData = (from tr in ctx.Activities
                //                   where tr.EmployeeID == id && tr.OrgID == OrgID
                //                   orderby tr.ActivityID descending
                //                   select tr).ToList();
                //}
                var transactionData = ctx.Transactions.AsEnumerable()
                        .Where(x => x.ProjectID == id)
                        .GroupBy(x => x.ActivityID)
                        .Select(x => new { ActivityID = x.Key, Received = x.Sum(v => v.Deposit), Paid = x.Sum(v => v.Withdraw) }).ToList();

                var expenseData = ctx.ExpenseItems.AsEnumerable()
                            .Where(ei => ei.ProjectID == id)
                            .GroupBy(ei => ei.ActivityID)
                            .Select(ei => new { ActivityID = ei.Key, Expenses = ei.Sum(v => v.ExpenseAmount), Received = ei.Sum(v => v.ReceiveAmount) }).ToList();


                if (actData.Count() > 0)
                {
                    var result = (from act in actData
                                  join c in ctx.Users on act.CreatedBy equals c.UserID
                                  join e in ctx.Users on act.EmployeeID equals e.UserID
                                  join app in ctx.Users on act.ApproverID equals app.UserID
                                  join t in transactionData
                                  on act.ActivityID equals t.ActivityID
                                  into activityTransaction
                                  from activity in activityTransaction.DefaultIfEmpty(new { ActivityID = act.ActivityID, Received = 0, Paid = 0 })
                                  join ex in expenseData
                                  on act.ActivityID equals ex.ActivityID
                                  into activityExpenses
                                  from expense in activityExpenses.DefaultIfEmpty(new { ActivityID = act.ActivityID, Expenses = 0, Received = 0 })
                                  select new ActivityDTO
                                  {
                                      ActivityId = act.ActivityID,
                                      ActivityName = act.ActivityName,
                                      EmployeeID = act.EmployeeID,
                                      EmployeeName = e.UserName,
                                      ProjectID = act.ProjectID,
                                      CreatedById = (int)act.CreatedBy,
                                      CreatedByName = c.UserName,
                                      ActivityDescription = act.ActivityDescription,
                                      CreationDate = act.CreationDate,
                                      SelectedRow = act.SelectedRow,
                                      OrgID = act.OrgID,
                                      ApproverID = (int)act.ApproverID,
                                      ApproverName = app.UserName,
                                      ActivityStatus = act.ActivityStatus,
                                      Expenses = expense.Expenses,
                                      Settlement = activity.Received,
                                      Advance = activity.Received,
                                  }).ToList();
                    return result;
                }
                else
                {
                    return null;
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
        public IEnumerable<ActivityDTO> GetNewByEmployee(int id, int OrgID)
        {
            try
            {
                
                var ctx = new XPenEntities();
         
                var actData = (from tr in ctx.Activities
                               where tr.EmployeeID == id && tr.OrgID == OrgID 
                               orderby tr.ActivityID descending
                               select tr);

                var transactionData = ctx.Transactions.AsEnumerable()
                        .Where(x => x.ProjectID == id)
                        .GroupBy(x => x.ActivityID)
                        .Select(x => new { ActivityID = x.Key, Received = x.Sum(v => v.Deposit), Paid = x.Sum(v => v.Withdraw) }).ToList();

                var expenseData = ctx.ExpenseItems.AsEnumerable()
                            .Where(ei => ei.ProjectID == id)
                            .GroupBy(ei => ei.ActivityID)
                            .Select(ei => new { ActivityID = ei.Key, Expenses = ei.Sum(v => v.ExpenseAmount), Received = ei.Sum(v => v.ReceiveAmount) }).ToList();


                if (actData.Count() > 0)
                {
                    var result = (from act in actData
                                  join c in ctx.Users on act.CreatedBy equals c.UserID
                                  join e in ctx.Users on act.EmployeeID equals e.UserID
                                  join app in ctx.Users on act.ApproverID equals app.UserID
                                  join t in transactionData
                                  on act.ActivityID equals t.ActivityID
                                  into activityTransaction
                                  from activity in activityTransaction.DefaultIfEmpty(new { ActivityID = act.ActivityID, Received = 0, Paid = 0 })
                                  join ex in expenseData
                                  on act.ActivityID equals ex.ActivityID
                                  into activityExpenses
                                  from expense in activityExpenses.DefaultIfEmpty(new { ActivityID = act.ActivityID, Expenses = 0, Received = 0 })
                                  select new ActivityDTO
                                  {
                                      ActivityId = 1,
                                      ActivityName = act.ActivityName,
                                      EmployeeID = act.EmployeeID,
                                      EmployeeName = e.UserName,
                                      ProjectID = act.ProjectID,
                                      CreatedById = (int)act.CreatedBy,
                                      CreatedByName = c.UserName,
                                      ActivityDescription = act.ActivityDescription,
                                      CreationDate = act.CreationDate,
                                      SelectedRow = act.SelectedRow,
                                      OrgID = act.OrgID,
                                      ApproverID = (int)act.ApproverID,
                                      ApproverName = app.UserName,
                                      ActivityStatus = act.ActivityStatus,
                                      Expenses = expense.Expenses,
                                      Settlement = activity.Received,
                                      Advance = activity.Received,
                                  }).ToList();
                    return result;
                }
                else
                {
                    return null;
                }


            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Activity/5
        [Route("Organization/{OrgID}/Project/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<ActivityDTO> GetByProject(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];

                var ctx = new XPenEntities();
                var actData = (from tr in ctx.Activities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList(); ;

                if (Status == "Show All")
                {
                    ints1 = new String[6];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Quick";
                    ints1[5] = "Approved";
                    actData = (from tr in ctx.Activities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();
                    //return actData;
                }
                else if (Status == "Open")
                {
                    ints1 = new String[5];
                    ints1[0] = "Added";
                    ints1[1] = "Initiated";
                    ints1[2] = "Submitted";
                    ints1[3] = "Approved";
                    ints1[4] = "Quick";
                    actData = (from tr in ctx.Activities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();
                    //return actData;

                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    actData = (from tr in ctx.Activities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();
                    //return actData;
                }
                else
                {
                    actData = (from tr in ctx.Activities
                                   where tr.ProjectID == id && tr.OrgID == OrgID
                                   orderby tr.ActivityID descending
                                   select tr).ToList();
                    //return actData;
                }

                var transactionData = ctx.Transactions.AsEnumerable()
                            .Where(x => x.ProjectID == id)
                            .GroupBy(x => x.ActivityID)
                            .Select(x => new { ActivityID = x.Key, Received = x.Sum(v => v.Deposit), Paid = x.Sum(v => v.Withdraw) }).ToList();

                var expenseData = ctx.ExpenseItems.AsEnumerable()
                            .Where(ei => ei.ProjectID == id)
                            .GroupBy(ei => ei.ActivityID)
                            .Select(ei => new { ActivityID = ei.Key, Expenses = ei.Sum(v => v.ExpenseAmount), Received = ei.Sum(v => v.ReceiveAmount) }).ToList();


                if (actData.Count() > 0)
                {
                    var result = (from act in actData
                                 join c in ctx.Users on act.CreatedBy equals c.UserID
                                 join e in ctx.Users on act.EmployeeID equals e.UserID
                                 join app in ctx.Users on act.ApproverID equals app.UserID
                                 join t in transactionData
                                 on act.ActivityID equals t.ActivityID
                                 into activityTransaction
                                 from activity in activityTransaction.DefaultIfEmpty(new { ActivityID = act.ActivityID, Received = 0, Paid = 0 })
                                 join ex in expenseData
                                 on act.ActivityID equals ex.ActivityID
                                 into activityExpenses
                                 from expense in activityExpenses.DefaultIfEmpty(new { ActivityID = act.ActivityID, Expenses = 0, Received = 0 })
                                    select new ActivityDTO {
                                    ActivityId=1,
                                    ActivityName = act.ActivityName,
                                    EmployeeID = act.EmployeeID,
                                    EmployeeName = e.UserName,
                                    ProjectID = act.ProjectID,
                                    CreatedById = (int)act.CreatedBy,
                                    CreatedByName = c.UserName,
                                    ActivityDescription = act.ActivityDescription,
                                    CreationDate = act.CreationDate,
                                    SelectedRow = act.SelectedRow,
                                    OrgID = act.OrgID,
                                    ApproverID = (int)act.ApproverID,
                                    ApproverName = app.UserName,
                                    ActivityStatus = act.ActivityStatus,
                                    Expenses = expense.Expenses,
                                    Settlement = activity.Received,
                                    Advance = activity.Received,
                                }).ToList();
                    return result;
                }
                else
                {
                    return null;
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
                var ctx = new XPenEntities();

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
                        SelectedRow = false,
                        ActivityStatus = "Open"
                        }
                        );
                    ctx.SaveChanges();
                    ctx.ExpenseItems.Add(new ExpenseItem
                    {
                        ActivityID = e.ActivityID,
                        ItemName = e.ActivityName,
                        ExpenseAmount = activity.ExpenseAmount,
                        ReceiveAmount = 0,
                        ExpenseDescription = activity.ActivityDescription,
                        ExpenseDate = DateTime.UtcNow,
                        SelectedRow = false,
                        Action = activity.ActivityStatus,
                        OrganizationId = activity.OrgID,
                        AccountId = 0,
                        ProjectID = activity.ProjectID,
                        Status = "Open"

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
