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

                var ctx = new XPenEntities();
                var expData = (from ex in ctx.Activities
                               where ex.EmployeeID == employeeID /*&& (ex.Status == "Added" || ex.ActivityStatus == "Submitted" || ex.ActivityStatus == "Approved" || ex.ActivityStatus == "Quick")*/
                               group ex by new { ex.EmployeeID,/* ex.ActivityStatus*/ }
                                   into groupEmpStatus

                                   select new Expenses
                                   {
                                       EmployeeID = groupEmpStatus.Key.EmployeeID,
                                       //Status = groupEmpStatus.Key.ActivityStatus,
                                       //ExpenseAmount = (Int32)groupEmpStatus.Sum(x => x.Expenses),
                                       //ReceiveAmount = (Int32)groupEmpStatus.Sum(x => x.Received),
                                       ActivityCount = groupEmpStatus.Select(c => c.ActivityID).Distinct().Count()
                                   });
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // Get unpaid Expenses for Manager

        [Route("UnpaidExpense/{orgId}/Manager/{mgrId}")]
        [HttpGet]
        public IEnumerable<Expenses> GetUnpaidByManager(int orgId, int mgrId)
        {
            try
            {

                var ctx = new XPenEntities();
                var expData = (from ex in ctx.Activities
                               where ex.CreatedBy == mgrId 
                               && ex.OrgID == orgId
                               //&& (ex.ActivityStatus == "Added" || ex.ActivityStatus == "Submitted" || ex.ActivityStatus == "Approved")
                               group ex by new { ex.EmployeeID, ex.ActivityStatus }
                                   into groupEmpStatus

                               select new Expenses
                               {
                                   EmployeeID = groupEmpStatus.Key.EmployeeID,
                                   Status = groupEmpStatus.Key.ActivityStatus,
                                   ExpenseAmount = 999,// (Int32)groupEmpStatus.Sum(x => x.Expenses),
                                   ReceiveAmount = 999, //(Int32)groupEmpStatus.Sum(x => x.Received),
                                   ActivityCount = groupEmpStatus.Select(c => c.ActivityID).Distinct().Count()
                               });
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Get unpaid Expenses for an Employee

        [Route("UnpaidExpense/{orgId}")]
        [HttpGet]
        public IEnumerable<Expenses> GetUnpaidForAdmin(int orgId)
        {
            try
            {

                var ctx = new XPenEntities();
                var expData = (from ex in ctx.Activities
                               where ex.OrgID == orgId
                               && (ex.ActivityStatus == "Added" || ex.ActivityStatus == "Submitted" || ex.ActivityStatus == "Approved")
                               group ex by new { ex.EmployeeID, ex.ActivityStatus }
                                   into groupEmpStatus

                               select new Expenses
                               {
                                   EmployeeID = groupEmpStatus.Key.EmployeeID,
                                   Status = groupEmpStatus.Key.ActivityStatus,
                                   ExpenseAmount = 999, // (Int32)groupEmpStatus.Sum(x => x.Expenses),
                                   ReceiveAmount = 999,// (Int32)groupEmpStatus.Sum(x => x.Received),
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

                var ctx = new XPenEntities();
                var expData =  (from ex in ctx.ExpenseItems
                               where ex.EmployeeID == employeeID
                               && DbFunctions.TruncateTime(ex.ExpenseDate) == DateTime.Today.Date
                               && (ex.Status == "Paid" || ex.Status == "Submitted" || ex.Status == "Approved" || ex.Status == "Quick")
                               group ex by new {ex.Status }
                                   into groupEmpExpense

                               select new Expenses
                               {
                                   Status = groupEmpExpense.Key.Status,
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

        // Get Today Expense for Manager
        [Route("TodayExpense/{orgId}/Manager/{mgrId}")]
        [HttpGet]
        public IEnumerable<Expenses> GetTodayExpenseOfManager(int orgId, int mgrId)
        {
            try
            {
                var ctx = new XPenEntities();
                var expData = (from ex in ctx.ExpenseItems
                               where ex.ApproverID == mgrId
                               && DbFunctions.TruncateTime(ex.ExpenseDate) == DbFunctions.TruncateTime(DateTime.Today.Date)
                               && (ex.Action == "Added" || ex.Action == "Submitted" )
                               group ex by new { ex.Status }
                                   into groupEmpExpense

                               select new Expenses
                               {
                                   Status = groupEmpExpense.Key.Status,
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


        // Get Today Expense for an Employee
        [Route("Running/{orgId}/Employee/{employeeID}")]
        [HttpGet]
        public IEnumerable<ActiveProject> GetRunningProjectsOfEmployee(int orgId, int employeeID)
        {
            try
            {

                var ctx = new XPenEntities();
                var expData = (from ex in ctx.ExpenseItems
                               where ex.EmployeeID == employeeID
                               && (ex.Status == "Submitted" || ex.Status == "Approved" || ex.Status == "Initiated")
                               select new ActiveProject {
                                   ProjectID = ex.ProjectID,
                                   ProjectName = "Test", //ex.ProjectName,
                                   ProjectManager = "Test",// ex.ProjectOwner
                               }).Distinct();

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
        public IEnumerable<ExpenseItem> GetActiveProjectsOfEmployee(int orgId, int employeeID)
        {
            try
            {

                using (var ctx = new XPenEntities())
                {
                    var expData = (from ex in ctx.ExpenseItems
                                   where ex.EmployeeID == employeeID
                                     && (ex.Status != "Paid")
                                     select ex
                                        ).ToList();

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




        // Get IP Sales for Manager
        [Route("IPSalesInvoice/{orgId}/Employee/{MgrId}/{Margin}")]
        [HttpGet]
        public IEnumerable<SalesInvoice> GetIPSalesInvoice(int orgId, int MgrId, int Margin)
        {
            try
            {

                var ctx = new XPenEntities();
                var invData = (from inv in ctx.SalesInvoices
                               where inv.ProjectId == MgrId
                               && inv.OrgId == orgId
                               && (inv.ServiceCost - Margin >= inv.ServiceCost)
                               select inv);

                return invData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // Get IP Purchase for Manager
        [Route("IPPurchaseInvoice/{orgId}/Employee/{MgrId}/{Margin}")]
        [HttpGet]
        public IEnumerable<PurchaseInvoice> GetIPPurchaseInvoice(int orgId, int MgrId, int Margin)
        {
            try
            {

                var ctx = new XPenEntities();
                var invData = (from inv in ctx.PurchaseInvoices
                               where inv.ProjectId == MgrId
                               && inv.OrgId == orgId
                               && (inv.ServiceCost - Margin >= inv.ServiceCost)
                               select inv);

                return invData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        // Get IP Sales for Admin
        [Route("IPSalesInvoice/{orgId}/{Margin}")]
        [HttpGet]
        public IEnumerable<SalesInvoice> GetIPSalesInvoiceForAdmin(int orgId, int Margin)
        {
            try
            {

                var ctx = new XPenEntities();
                var invData = (from inv in ctx.SalesInvoices
                               where inv.OrgId == orgId
                               //&& (inv.Receivable - Margin >= inv.ReceivedAmount)
                               select inv);

                return invData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // Get IP Purchase for Admin
        [Route("IPPurchaseInvoice/{orgId}/{Margin}")]
        [HttpGet]
        public IEnumerable<PurchaseInvoice> GetIPPurchaseInvoiceForAdmin(int orgId, int Margin)
        {
            try
            {

                var ctx = new XPenEntities();
                var invData = (from inv in ctx.PurchaseInvoices
                               where inv.OrgId == orgId
                               //&& (inv.Payable - Margin >= inv.PaidAmount)
                               select inv);

                return invData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



    }
}
