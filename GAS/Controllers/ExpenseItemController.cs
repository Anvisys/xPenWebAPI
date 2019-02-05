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
    [RoutePrefix("api/ExpenseItem")]
    public class ExpenseItemController : ApiController
    {
        // GET: api/ExpenseItem
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Route("Organization/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByOrg(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities orderby ex.UpdatedOn ascending
                               where ex.OrgID == id 
                               orderby ex.UpdatedOn ascending
                               select new DailyExpense { ExpenseDate = (DateTime)ex.UpdatedOn, Status = ex.ActivityStatus, 
                                   ExpenseAmount = (Int32)ex.ExpenseAmount, ReceiveAmount = (Int32)ex.ReceiveAmount })
                               .Take(10);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // GET: api/ExpenseItem/5
        [Route("Organization/{id}/{year}/{month}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetByOrgMonth(int id, int year, int month)
        {
            try {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.OrgID == id && ex.ExpenseDate.Year == year && ex.ExpenseDate.Month == month
                               orderby ex.UpdatedOn ascending
                               select ex);
                return expData;
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        // GET: api/ExpenseItem/5
        
        [Route("Project/{id}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetByProject(int id)
        {
            try
            {
                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.ProjectID == id
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("Employee/{id}/{year}/{month}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByEmployeeByMonth(int id, int year, int month)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.EmployeeID == id && ex.ExpenseDate.Year == year && ex.ExpenseDate.Month == month
                               select new DailyExpense {ExpenseDate = (DateTime)ex.ExpenseDate, Status = ex.ActivityStatus, 
                                   ExpenseAmount =(Int32)ex.ExpenseAmount, ReceiveAmount = (Int32)ex.ReceiveAmount });
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/ExpenseItem/5

        [Route("Project/{id}/Employee/{EmployeeID}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetProjectByEmployee(int id, int EmployeeID)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.ProjectID == id && ex.EmployeeID == EmployeeID
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("Activity/{id}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetByActivity(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.ActivityID == id orderby ex.ExpenseDate ascending
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



   



        // GET: api/ExpenseItem/5

        [Route("Organization/{orgId}/Employee/{employeeID}/Year/{year}/{month}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetByEmployee(int orgId, int employeeID, int year, int month)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.EmployeeID == employeeID && ex.ExpenseDate.Year == year && ex.ExpenseDate.Month == month && (ex.Status == "Added" || ex.Status == "Paid")
                               select ex );
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [Route("Organization/{orgId}/Employee/{employeeID}/{year}/{month}")]
        [HttpGet]
        public IEnumerable<dynamic> GetByDate(int orgId, int employeeID, int year, int month)
        {
            try
            {
                 
                using (var ctx = new GASEntities())
                {
                    var query =
                    from act in ctx.Activities
                    join eItms in ctx.ExpenseItems on act.ActivityID equals eItms.ActivityID
                    join prj in ctx.Projects on act.ProjectID equals prj.ProjectID
                    where act.OrgID == orgId && act.EmployeeID == employeeID
                    && eItms.ExpenseDate.Year == year && eItms.ExpenseDate.Month == month
                    select new 
                    { 
                        ItemName = eItms.ItemName,
                        ExpenseAmount = eItms.ExpenseAmount,
                        ExpenseDate = eItms.ExpenseDate,
                        ActivityName = act.ActivityName,
                        ProjectName = prj.ProjectName
 
                    };

                    return query.ToList();
                }

                
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }
        // GET: api/ExpenseItem/5


        [Route("Manager/{id}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemDailyStatu> GetByManager(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemDailyStatus
                               where ex.ApproverID == id 
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // POST: api/ExpenseItem/Add
        [Route("Add")]
        [HttpPost]
        public HttpResponseMessage PostAdd([FromBody]ExpenseItem[] eItem)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();

                if (eItem != null)
                {
                    var id = ctx.ExpenseItems.AddRange(eItem);

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

        // POST: api/ExpenseItem/Add
        [Route("AddItem")]
        [HttpPost]
        public HttpResponseMessage PostAddItem([FromBody]ExpenseItem eItem)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();

                if (eItem != null)
                {
                    var id = ctx.ExpenseItems.Add(eItem);

                    ctx.SaveChanges();
                    resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                Utility.log(ex.Message);
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";

            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // POST: api/ExpenseItem/Delete
        [Route("Delete")]
        [HttpPost]
        public HttpResponseMessage PostDelete([FromBody]ExpenseItem eItem)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();

                if (eItem != null)
                {
                    var item = (from ex in ctx.ExpenseItems
                                where ex.ItemID == eItem.ItemID 
                              select ex).First();
                    item.Status = "Deleted";
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

        // PUT: api/ExpenseItem/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ExpenseItem/5
        public void Delete(int id)
        {
        }
    }
}
