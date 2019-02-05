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
     [RoutePrefix("api/Expense")]
    public class ExpenseController : ApiController
    {
        // GET: api/Expense
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Expense/5
        public IEnumerable<MonthExpense> Get(int id)
        {
            try
            {
                var ctx = new GASEntities();
                var exData = (from tr in ctx.ViewMonthCumulativeExpenses
                              where tr.UserID == id
                              select (new MonthExpense { 
                              Year = (int)tr.year,
                              Month = (int)tr.month,
                              Expense = (int)tr.expense
                              }));
                return exData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/Expense/5
        [Route("{id}/Year/{year}")]
        [HttpGet]
        public IEnumerable<Expens> GetYearly(int id)
        {
            try
            {
                var ctx = new GASEntities();
                var exData = (from tr in ctx.Expenses
                              where tr.UserID == id
                              select tr).Take(10);
                return exData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("{id}/Year/{year}/Month/{month}")]
        [HttpGet]
        public IEnumerable<Expens> GetMonthly(int id, int year, int month)
        {
            try
            {
                var ctx = new GASEntities();
                var exData = (from tr in ctx.Expenses
                              where tr.UserID == id && tr.ExpenseDate.Year == year && tr.ExpenseDate.Month == month
                              select tr).Take(10);
                return exData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // POST: api/Expense
        public HttpResponseMessage Post([FromBody]Expens value)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new GASEntities();
                ctx.Expenses.Add(value);
                ctx.SaveChanges();
                resp = "{\"Response\":\"OK\"}";
            }
            catch (Exception ex)
            {
                resp = "{\"Response\":\"Fail\"}";
            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // PUT: api/Expense/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Expense/5
        public void Delete(int id)
        {
        }
    }
}
