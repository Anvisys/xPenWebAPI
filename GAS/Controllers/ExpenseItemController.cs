using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Cors;
using GAS.Models;
using System.Data.Entity.Core.Objects;

namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/ExpenseItem")]
    public class ExpenseItemController : ApiController
    {
       

        // Get expense list of an Organization in given month of a year
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

        // Get expense list in a project

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

     

        // Get expense list of an employee in a project

        [Route("Project/{id}/Employee/{EmployeeID}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetProjectByEmployee(int id, int EmployeeID)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.ProjectID == id && ex.EmployeeID == EmployeeID && (ex.ItemAction == "Added" || ex.ItemAction == "Paid")
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Get expense list of an employee by month of year
        [Route("Activity/{id}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetByActivity(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.ActivityID == id && (ex.ItemAction == "Added" || ex.ItemAction == "Paid")
                               orderby ex.ExpenseDate ascending
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        
        // Get expense list of an employee by month of year

        [Route("Organization/{orgId}/Employee/{employeeID}/Year/{year}/{month}")]
        [HttpGet]
        public IEnumerable<ViewExpenseItemStatusActivity> GetByEmployee(int orgId, int employeeID, int year, int month)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.EmployeeID == employeeID && ex.ExpenseDate.Year == year && ex.ExpenseDate.Month == month 
                               && (ex.ItemAction == "Added" || ex.ItemAction == "Paid")
                               select ex );
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Get expense list of an employee by month of year

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
        // Get Daily Expense with Status for a Manager


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


        // Add set/Array of Expense Item
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

        // Add Single Expense Item
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

        // Select a single Expense Item
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
                    item.Action = "Deleted";
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
