using GAS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI.WebControls;

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
        public IEnumerable<SalesInvoice> GetUnpaidSellInvoice(int OrgID)
        {
            try
            {
                var ctx = new XPenEntities();
                var invoiceData = (from inv in ctx.SalesInvoices
                                       //where inv.OrgId == OrgID && (inv.Receivable - inv.ReceivedAmount>100)
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
        public IEnumerable<InvoiceDTO> GetSellInvoice(int OrgID, int ProjectID)
        {
            try
            {
                var ctx = new XPenEntities();
                var saleInvoice = (from inv in ctx.SalesInvoices.AsEnumerable()
                                   where inv.OrgId == OrgID && inv.ProjectId == ProjectID
                                   select inv).ToList();
                var transactionData = ctx.Transactions.AsEnumerable()
                                .Where(x=> x.ProjectID == ProjectID)
                                .GroupBy(x => x.InvoiceID)
                                .Select(x => new { InvoiceID = x.Key, Received = x.Sum(v => v.Deposit), Paid = x.Sum(v => v.Withdraw) }).ToList();
                
                var saleInvoiceData = (from s in saleInvoice
                                       join t in transactionData
                                   on s.ID equals t.InvoiceID
                                   into invoiceTransaction
                                   from sales in invoiceTransaction.DefaultIfEmpty( new { InvoiceID = s.ID, Received = 0, Paid = 0 })

                                   select new InvoiceDTO
                                   {
                                       InvoiceId = s.ID,
                                       InvoiceNumber = s.InvoiceNumber,
                                       OrgId = s.OrgId,
                                       ProjectId = s.ProjectId,
                                       ServiceCost = s.ServiceCost,
                                       CGST = s.CGST,
                                       SGST = s.SGST,
                                       IGST = (int)s.IGST,
                                       TDS = s.TDS,
                                       InvoiceDate = s.InvoiceDate,
                                       InvoiceType = (int)s.InvoiceType,
                                       Paid = sales.Paid,
                                       Received = sales.Received
                                   }).ToList();

                return saleInvoiceData;
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
        public IEnumerable<InvoiceDTO> GetPurchaseInvoice(int OrgID, int ProjectID)
        {
            try
            {
                var ctx = new XPenEntities();
                var saleInvoice = (from inv in ctx.PurchaseInvoices.AsEnumerable()
                                   where inv.OrgId == OrgID && inv.ProjectId == ProjectID
                                   select inv).ToList();
                var transactionData = ctx.Transactions.AsEnumerable()
                                .Where(x => x.ProjectID == ProjectID)
                                .GroupBy(x => x.InvoiceID)
                                .Select(x => new { InvoiceID = x.Key, Received = x.Sum(v => v.Deposit), Paid = x.Sum(v => v.Withdraw) }).ToList();

                var saleInvoiceData = (from s in saleInvoice
                                       join t in transactionData
                                   on s.ID equals t.InvoiceID
                                   into invoiceTransaction
                                       from sales in invoiceTransaction.DefaultIfEmpty(new { InvoiceID = s.ID, Received = 0, Paid = 0 })

                                       select new InvoiceDTO
                                       {
                                           InvoiceId = s.ID,
                                           InvoiceNumber = s.InvoiceNumber,
                                           OrgId = s.OrgId,
                                           ProjectId = s.ProjectId,
                                           ServiceCost = s.ServiceCost,
                                           CGST = s.CGST,
                                           SGST = s.SGST,
                                           IGST = (int)s.IGST,
                                           TDS = s.TDS,
                                           InvoiceDate = s.InvoiceDate,
                                           InvoiceType = (int)s.InvoiceType,
                                           Paid = sales.Paid,
                                           Received = sales.Received
                                       }).ToList();

                return saleInvoiceData;
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
        public IEnumerable<PurchaseInvoice> GetPurchaseForMonth(int OrgID, int Year, int Month)
        {
            try
            {
                var ctx = new XPenEntities();
                var tdsData = (from tds in ctx.PurchaseInvoices
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
        public IEnumerable<SalesInvoice> GetSellForMonth(int OrgID, int Year, int Month)
        {
            try
            {
                var ctx = new XPenEntities();
                var tdsData = (from inv in ctx.SalesInvoices
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
        public HttpResponseMessage Post([FromBody] SalesInvoice inv)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new XPenEntities();

                if (inv != null)
                {
                    ctx.SalesInvoices.Add(inv);
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

        //// Add Payment Received
        //[Route("ReceivePayment")]
        //[HttpPost]
        //public HttpResponseMessage PostReceive([FromBody]PaymentReceived pr)
        //{

        //    String resp = "{\"Response\":\"Undefine\"}";
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    try
        //    {
        //        var ctx = new XPenEntities();

        //        if (pr != null)
        //        {
        //            ctx.PaymentReceiveds.Add(pr);
        //            ctx.SaveChanges();

        //            resp = "{\"Response\":\"OK\"}";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        int a = 1;
        //        resp = "{\"Response\":\"Fail\"}";
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError);

        //    }

        //    // var response = Request.CreateResponse(HttpStatusCode.OK);
        //    response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
        //    return response;
        //}

        // Add Purchase Invoice
        [Route("PurchaseInvoice")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]PurchaseInvoice inv)
        {

            String resp = "{\"Response\":\"Undefine\"}";
            var response = Request.CreateResponse(HttpStatusCode.OK);
            try
            {
                var ctx = new XPenEntities();

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

        //// Add Payment given
        //[Route("GivePayment")]
        //[HttpPost]
        //public HttpResponseMessage Post([FromBody]PaymentGiven pg)
        //{

        //    String resp = "{\"Response\":\"Undefine\"}";
        //    var response = Request.CreateResponse(HttpStatusCode.OK);
        //    try
        //    {
        //        var ctx = new XPenEntities();

        //        if (pg != null)
        //        {
        //            ctx.PaymentGivens.Add(pg);
        //            ctx.SaveChanges();

        //            resp = "{\"Response\":\"OK\"}";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        int a = 1;
        //        resp = "{\"Response\":\"Fail\"}";
        //        response = Request.CreateResponse(HttpStatusCode.InternalServerError);

        //    }

        //    // var response = Request.CreateResponse(HttpStatusCode.OK);
        //    response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
        //    return response;
        //}



    }
}
