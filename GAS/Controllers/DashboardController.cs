using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using GAS.Models;
using System.Web.Http.Cors;



namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
        // GET: api/Dashboard
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Dashboard/5
        public string Get(int id)
        {
            return "value";
        }


        // Get unpaid Expenses for an Employee

        [Route("Organization/{orgId}/Employee/{employeeID}")]
        [HttpGet]
        public IEnumerable<Expenses> GetUnpaidByEmployee(int orgId, int employeeID)
        {
            try
            {

                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewActivities
                               where ex.EmployeeID == employeeID && (ex.ActivityStatus == "Added" || ex.ActivityStatus == "Submitted" || ex.ActivityStatus == "Approved" || ex.ActivityStatus == "Quick")
                               group ex by new { ex.EmployeeID, ex.ActivityStatus }
                                   into groupEmpStatus

                                   select new Expenses
                                   {
                                       EmployeeID = groupEmpStatus.Key.EmployeeID,
                                       Status = groupEmpStatus.Key.ActivityStatus,
                                       ExpenseAmount = (Int32)groupEmpStatus.Sum(x => x.Expenses),
                                       ReceiveAmount = (Int32)groupEmpStatus.Sum(x => x.Received),
                                       ActivityCount = groupEmpStatus.Select(c => c.ActivityID).Distinct().Count()
                                   });
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        // POST: api/Dashboard
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Dashboard/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Dashboard/5
        public void Delete(int id)
        {
        }
    }
}
