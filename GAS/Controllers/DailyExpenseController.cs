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

        // Get day wise Expenses for an organization
        [Route("Organization/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByOrganization(int id)
        {
            try
            {

                var ctx = new GASEntities();
          
                var expData = (from ex in ctx.ViewExpenseItemDailyStatus
                               where ex.OrgID == id
                               orderby ex.ExpensesDate descending
                               group ex by new { ex.ExpensesDate, ex.ActivityStatus }
                            into dailyEx
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpensesDate,
                                   Status = dailyEx.Key.ActivityStatus,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.Expense),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.Received)
                               }).Take(10);


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
                            var expData = (from ex in ctx.ViewExpenseItemDailyStatus
                               where ex.ProjectID == id
                               orderby ex.ExpensesDate ascending
                               group ex by new { ex.ExpensesDate, ex.ActivityStatus }
                                into dailyEx
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpensesDate,
                                   Status = dailyEx.Key.ActivityStatus,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.Expense),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.Received)
                               }).Take(10);


                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        [Route("{OrgId}/Employee/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByEmployee(int OrgId, int id)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemDailyStatus
                               where ex.EmployeeID == id && ex.OrgID == OrgId
                               orderby ex.ExpensesDate descending
                               group ex by new { ex.ExpensesDate, ex.ActivityStatus }
                                   into dailyEx
                                   orderby dailyEx.Key.ExpensesDate
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpensesDate,
                                   Status = dailyEx.Key.ActivityStatus,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.Expense),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.Received)
                               }).Take(10);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

  
    }
}
