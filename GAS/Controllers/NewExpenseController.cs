using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using GAS.Models;
using System.Web.Http.Cors;
using System.Data.Entity.Core.Objects;

namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/NewExpense")]
    public class NewExpenseController : ApiController
    {
        // GET: api/NewExpense
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/NewExpense/5
        public string Get(int id)
        {
            return "value";
        }


        // Get Daywise Expenses for an Employee

        [Route("Organization/{orgId}/Employee/{employeeID}")]
        [HttpGet]
        public IEnumerable<Expenses> GetDailyExpenseByEmployee(int orgId, int employeeID)
        {
            try
            {
               
                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.EmployeeID == employeeID
                               group ex by new { ex.EmployeeID, sDate= EntityFunctions.TruncateTime(ex.ExpenseDate)  }
                                   into groupEmpStatus

                                   select new Expenses
                                   {
                                       EmployeeID = groupEmpStatus.Key.EmployeeID,
                                       Date = (DateTime)groupEmpStatus.Key.sDate,
                                       ExpenseAmount = (Int32)groupEmpStatus.Sum(x => x.ExpenseAmount),
                                       ReceiveAmount = (Int32)groupEmpStatus.Sum(x => x.ReceiveAmount),
                                       
                                   });
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





        // POST: api/NewExpense
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/NewExpense/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/NewExpense/5
        public void Delete(int id)
        {
        }
    }
}
