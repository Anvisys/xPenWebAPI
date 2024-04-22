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

                var ctx = new XPenEntities();
          
                var expData = (from ex in ctx.ExpenseItems
                               where ex.OrganizationId == id
                               orderby ex.ExpenseDate descending
                               group ex by new { ex.ExpenseDate, ex.Status }
                            into dailyEx
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpenseDate,
                                   Status = dailyEx.Key.Status,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.ExpenseAmount),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.ReceiveAmount)
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

                var ctx = new XPenEntities();
                            var expData = (from ex in ctx.ExpenseItems
                                           where ex.ProjectID == id
                               orderby ex.ExpenseDate ascending
                               group ex by new { ex.ExpenseDate, ex.Status }
                                into dailyEx
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpenseDate,
                                   Status = dailyEx.Key.Status,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.ExpenseAmount),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.ReceiveAmount)
                               }).Take(10);


                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("{OrgId}/Manager/{id}")]
        [HttpGet]
        public IEnumerable<DailyExpense> GetByManager(int OrgId, int id)
        {
            try
            {
                var ctx = new XPenEntities();
                var expData = (from ex in ctx.ExpenseItems
                               where ex.ApproverID == id && ex.OrganizationId == OrgId 
                               orderby ex.ExpenseDate ascending
                               group ex by new { ex.ExpenseDate, ex.Status }
                    into dailyEx
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpenseDate,
                                   Status = dailyEx.Key.Status,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.ExpenseAmount),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.ReceiveAmount)
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

                var ctx = new XPenEntities();
                var expData = (from ex in ctx.ExpenseItems
                               where ex.EmployeeID == id && ex.OrganizationId == OrgId
                               orderby ex.ExpenseDate descending
                               group ex by new { ex.ExpenseDate, ex.Status }
                                   into dailyEx
                                   orderby dailyEx.Key.ExpenseDate
                               select new DailyExpense
                               {
                                   ExpenseDate = (DateTime)dailyEx.Key.ExpenseDate,
                                   Status = dailyEx.Key.Status,
                                   ExpenseAmount = (int)dailyEx.Sum(x => x.ExpenseAmount),
                                   ReceiveAmount = (int)dailyEx.Sum(x => x.ReceiveAmount)
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
