using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using GAS.Models;
using System.Web.Http.Cors;
using System.Data.Entity;
using System.Data.Entity.Validation;

using System.Threading.Tasks;



namespace GAS.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Dashboard")]
    public class DashboardController : ApiController
    {
      
        // Get unpaid Expenses for an Employee

        [Route("UnpaidExpense/{orgId}/Employee/{employeeID}")]
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

        // Get Today Expense for an Employee

        [Route("TodayExpense/{orgId}/Employee/{employeeID}")]
        [HttpGet]
        public IEnumerable<Expenses> GetTodayExpenseOfEmployee(int orgId, int employeeID)
        {
            try
            {

                var ctx = new GASEntities();
                var expData =  (from ex in ctx.ViewExpenseItemStatusActivities
                               where ex.EmployeeID == employeeID
                               && DbFunctions.TruncateTime(ex.ExpenseDate) == DateTime.Today.Date
                               && (ex.ActivityStatus == "Paid" || ex.ActivityStatus == "Submitted" || ex.ActivityStatus == "Approved" || ex.ActivityStatus == "Quick")
                               group ex by new {ex.ActivityStatus }
                                   into groupEmpExpense

                               select new Expenses
                               {
                                   Status = groupEmpExpense.Key.ActivityStatus,
                                   ExpenseAmount = (Int32)groupEmpExpense.Sum(x => x.ExpenseAmount),
                                   ReceiveAmount = (Int32)groupEmpExpense.Sum(x => x.ReceiveAmount),
                                  
                               });

                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Get Active Projects for an Employee

        [Route("ActiveProjects/{orgId}/Employee/{employeeID}")]
        [HttpGet]
        public IEnumerable<ActiveProject> GetActiveProjectsOfEmployee(int orgId, int employeeID)
        {
            try
            {

                using (var ctx = new GASEntities())
                {
                    var expData = (from ex in ctx.ViewExpenseItemStatusActivities
                                    where ex.EmployeeID == employeeID
                                     && (ex.ActivityStatus != "Paid")
                                     select new ActiveProject
                                     {
                                         ProjectID = ex.ProjectID,
                                         Name = ex.ProjectName,
                                         Manager = ex.ProjectOwner
                                     }
                                        ).Distinct().ToList();

                    //var expData = (from prj in ctx.Projects.Where(p => expData1.Contains(p.ProjectID))
                    //               select new ActiveProject()
                    //               {
                    //                   ProjectID = prj.ProjectID,
                    //                   Name = prj.ProjectName,
                    //                   Manager = 0
                    //               }).ToList();
                    return expData;
                }
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Utility.log(string.Format("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage));
                    }
                }
                throw dbEx;
            }
            catch (Exception ex)
            {
                Utility.log(ex.Message);
                Utility.log(ex.StackTrace);
                throw ex;
            }
        }

    }
}
