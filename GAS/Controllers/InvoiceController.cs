using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GAS.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Invoice")]
    public class InvoiceController : ApiController
    {
        // GET: api/Invoice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Invoice/5
        public string Get(int id)
        {
            return "value";
        }

        // Get all Unpaid sales invoice for an organization
        [Route("Sell/Organization/{OrgID}/Unpaid")]
        [HttpGet]
        public IEnumerable<NewViewSellInvoice> GetUnpaidSellInvoice(int OrgID)
        {
            try
            {
                var ctx = new GASEntities();
                var invoiceData = (from inv in ctx.NewViewSellInvoices
                                   where inv.OrgId == OrgID && (inv.Receivable - inv.ReceivedAmount>100)
                                   select inv);
                return invoiceData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }

        // Get all Sales Invoice for a project
        [Route("Sell/Organization/{OrgID}/Project/{ProjectID}")]
        [HttpGet]
        public IEnumerable<NewViewSellInvoice> GetSellInvoice(int OrgID, int ProjectID)
        {
            try
            {
                var ctx = new GASEntities();
                var projectData = (from inv in ctx.NewViewSellInvoices
                                   where inv.OrgId == OrgID && inv.ProjectId == ProjectID
                                   select inv);
                return projectData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }

        // Get all purchase Invoice for a project
        [Route("Purchase/Organization/{OrgID}/Project/{ProjectID}")]
        [HttpGet]
        public IEnumerable<NewViewPurchaseInvoice> GetPurchaseInvoice(int OrgID, int ProjectID)
        {
            try
            {
                var ctx = new GASEntities();
                var projectData = (from inv in ctx.NewViewPurchaseInvoices
                                   where inv.OrgId == OrgID && inv.ProjectId == ProjectID
                                   select inv);
                return projectData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }

        // Get all purchase Invoice of a Organization by Year and Month
        [Route("Purchase/Organization/{OrgID}/{Year}/{Month}")]
        [HttpGet]
        public IEnumerable<NewViewPurchaseInvoice> GetPurchaseForMonth(int OrgID, int Year, int Month)
        {
            try
            {
                var ctx = new GASEntities();
                var tdsData = (from tds in ctx.NewViewPurchaseInvoices
                               where tds.OrgId == OrgID && tds.InvoiceDate.Year == Year && tds.InvoiceDate.Month == Month
                                   select tds);
                return tdsData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }

        // Get all Sales Invoice of a Organization by Year and Month
        [Route("Sell/Organization/{OrgID}/{Year}/{Month}")]
        [HttpGet]
        public IEnumerable<NewViewSellInvoice> GetSellForMonth(int OrgID, int Year, int Month)
        {
            try
            {
                var ctx = new GASEntities();
                var tdsData = (from inv in ctx.NewViewSellInvoices
                               where inv.OrgId == OrgID && inv.InvoiceDate.Year == Year && inv.InvoiceDate.Month == Month
                               select inv);
                return tdsData;
            }
            catch (Exception ex)
            {
                int a = 1;
                return null;
            }
        }

        // Add new Sales Invoice
        [Route("SellInvoice")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]SellInvoice inv)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new GASEntities();

                if (inv != null)
                {
                    ctx.SellInvoices.Add(inv);
                    ctx.SaveChanges();
                   
                    resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            }

            // var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // Add Payment Received
        [Route("ReceivePayment")]
        [HttpPost]
        public HttpResponseMessage PostReceive([FromBody]PaymentReceived pr)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new GASEntities();

                if (pr != null)
                {
                    ctx.PaymentReceiveds.Add(pr);
                    ctx.SaveChanges();

                    resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            }

            // var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // Add Purchase Invoice
        [Route("PurchaseInvoice")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]PurchaseInvoice inv)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new GASEntities();

                if (inv != null)
                {
                    ctx.PurchaseInvoices.Add(inv);
                    ctx.SaveChanges();

                    resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            }

            // var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }

        // Add Payment given
        [Route("GivePayment")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]PaymentGiven pg)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new GASEntities();

                if (pg != null)
                {
                    ctx.PaymentGivens.Add(pg);
                    ctx.SaveChanges();

                    resp = "{\"Response\":\"OK\"}";
                }
            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";
                response = Request.CreateResponse(HttpStatusCode.InternalServerError);

            }

            // var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;
        }


        // PUT: api/Invoice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Invoice/5
        public void Delete(int id)
        {
        }
    }
}
