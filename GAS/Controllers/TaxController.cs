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
    [RoutePrefix("api/Tax")]
    public class TaxController : ApiController
    {
        // Get latest TDS data for an organization
        [Route("TDS/{OrgID}")]
        [HttpGet]
        public TD GetTDS(int OrgID)
        {
            try
            {
                var ctx = new XPenEntities();
                var tdsData = (from tds in ctx.TDS
                               where tds.OrgID == OrgID  orderby tds.TaxMonth descending
                               select tds ).FirstOrDefault();
                if (tdsData == null)
                {
                    return new TD ();
                }

                DateTime da2 = tdsData.TaxMonth.AddMonths(-1);
                var tdsPrev = (from t in ctx.TDS
                               where t.OrgID == OrgID && t.TaxMonth.Year == da2.Year && t.TaxMonth.Month == da2.Month
                               select t).FirstOrDefault();
                if (tdsPrev == null)
                {
                    tdsData.PreviousTDS = 0;
                }
                else
                {
                    tdsData.PreviousTDS = (Int32)(tdsPrev.TDSPayable - tdsPrev.TDSDeducted);
                }

                return tdsData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // Get latest GST data for an organization
        [Route("GST/{OrgID}")]
        [HttpGet]
        public GST GetGST(int OrgID)
        {
            try
            {
               
                var ctx = new XPenEntities();
                var gstData = (from gst in ctx.GSTs
                               where gst.OrgID == OrgID orderby gst.TaxMonth descending
                               select gst).FirstOrDefault();

                if (gstData == null)
                    {
                        return new GST();
                    }
                DateTime da2 = gstData.TaxMonth.AddMonths(-1);
                var gstPrev = (from g in ctx.GSTs
                               where g.OrgID == OrgID && g.TaxMonth.Year == da2.Year && g.TaxMonth.Month == da2.Month
                               select g).FirstOrDefault();
                if (gstPrev == null)
                {
                    gstData.PreviousGSTDues = 0;
                }
                else
                {
                    gstData.PreviousGSTDues = (Int32)(gstPrev.GSTPayable - gstPrev.GSTReceived);
                }
                return gstData;
            }
            catch (Exception ex)
            {
                return new GST ();

            }
        }


        // Get  TDS data of an organization by year and month
        [Route("TDS/{OrgID}/{Year}/{Month}")]
        [HttpGet]
        public TD GetTDS(int OrgID, int Year, int Month)
        {
            try
            {
                DateTime d1 = new DateTime(Year, Month, 2);

                DateTime da2 = d1.AddMonths(-1);
                var ctx = new XPenEntities();
                var tdsData = (from tds in ctx.TDS
                               where tds.OrgID == OrgID && tds.TaxMonth.Year == Year && tds.TaxMonth.Month == Month
                               select tds).FirstOrDefault();
                if (tdsData == null)
                {
                    return new TD();
                }

                var tdsPrev = (from t in ctx.TDS
                               where t.OrgID == OrgID && t.TaxMonth.Year == da2.Year && t.TaxMonth.Month == da2.Month
                               select t).FirstOrDefault();
                if (tdsPrev == null)
                {
                    tdsData.PreviousTDS = 0;
                }
                else
                {
                    tdsData.PreviousTDS = (Int32)(tdsPrev.TDSPayable - tdsPrev.TDSDeducted);
                }

                return tdsData;
            }
            catch (Exception ex)
            {
                return new TD ();
            }
        }

        // Get  GST data of an organization by year and month
        [Route("GST/{OrgID}/{Year}/{Month}")]
        [HttpGet]
        public GST GetGST(int OrgID, int Year, int Month)
        {
            try
            {
                DateTime d1 = new DateTime(Year, Month, 2);

                DateTime da2 = d1.AddMonths(-1);
                var ctx = new XPenEntities();
                var gstData = (from gst in ctx.GSTs
                               where gst.OrgID == OrgID && gst.TaxMonth.Year == Year && gst.TaxMonth.Month == Month
                               select gst).FirstOrDefault();

                if (gstData == null)
                {
                    return new GST();
                }
                var gstPrev = (from g in ctx.GSTs
                               where g.OrgID == OrgID && g.TaxMonth.Year == da2.Year && g.TaxMonth.Month == da2.Month
                               select g).FirstOrDefault();
                if (gstPrev == null)
                {
                    gstData.PreviousGSTDues = 0;
                }
                else
                {
                    gstData.PreviousGSTDues = (Int32)(gstPrev.GSTPayable - gstPrev.GSTReceived);
                }
                return gstData;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        // Baseline the TDS Data

        [Route("TDSBaseline")]
        [HttpPost]
        public HttpResponseMessage PostTDS([FromBody]TD tds)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new XPenEntities();
                ctx.TDS.Add(tds);
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

        // Baseline the GST Data
        [Route("GSTBaseline")]
        [HttpPost]
        public HttpResponseMessage PostGST([FromBody]GST gst)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                var ctx = new XPenEntities();
                ctx.GSTs.Add(gst);
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



        // PUT: api/Tax/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Tax/5
        public void Delete(int id)
        {
        }
    }
}
