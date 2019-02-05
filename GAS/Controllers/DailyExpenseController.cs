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
    [RoutePrefix("api/DailyExpense")]
    public class DailyExpenseController : ApiController
    {
        // GET: api/DailyExpense
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/DailyExpense/5
        public string Get(int id)
        {
            return "value";
        }

        // GET: api/ExpenseItem/5
        [Route("Organization/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByOrganization(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewDailyExpenseItemOrganizations
                               where ex.OrgID == id
                               orderby ex.ExpensesDate descending
                               select new DailyExpense { ExpenseDate = (DateTime)ex.ExpensesDate, Status = ex.ActivityStatus, 
                                   ExpenseAmount = (int)ex.expense, ReceiveAmount = (int)ex.Received }).Take(10);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        [Route("Project/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByProject(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewDailyExpenseItemProjects
                               where ex.ProjectID == id
                               orderby ex.ExpensesDate descending
                               select new DailyExpense { ExpenseDate = (DateTime)ex.ExpensesDate, Status = ex.ActivityStatus, 
                                   ExpenseAmount = (int)ex.expense, ReceiveAmount = (int)ex.Received }).Take(10);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("Employee/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByEmployee(int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewDailyExpenseItemEmployees
                               where ex.EmployeeID == id orderby ex.ExpensesDate descending
                               select new DailyExpense { ExpenseDate = (DateTime)ex.ExpensesDate, Status = ex.ActivityStatus, 
                                   ExpenseAmount = (int)ex.expense, ReceiveAmount = (int)ex.Received }).Take(10);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: api/DailyExpense
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/DailyExpense/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/DailyExpense/5
        public void Delete(int id)
        {
        }
    }
}
