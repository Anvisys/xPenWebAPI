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
       [RoutePrefix("api/Advance")]
    public class AdvanceController : ApiController
    {
       GASEntities ctx;
      // get advance request in an Organization by Status
      [Route("Organization/{OrgID}/Status/{Status}")]
           [HttpGet]
           public IEnumerable<ViewAdvance> GetAll(int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];
                if (Status == "Show All")
                    {
                        ints1 = new String[5];
                        ints1[0] = "Paid";
                        ints1[1] = "Added";
                        ints1[2] = "Initiated";
                        ints1[3] = "Submitted";
                        ints1[4] = "Approved";
                    }
                    else if (Status == "Open")
                    {
                        ints1 = new String[4];
                        ints1[0] = "Submitted";
                        ints1[1] = "Approved";
                    }
                    else if (Status == "Closed")
                    {
                        ints1 = new String[5];
                        ints1[0] = "Paid";
                    }
                ctx = new GASEntities();
                var expData = (from ex in ctx.ViewAdvances
                               orderby ex.ActivityID ascending
                               where ex.AdvanceStatus != "Deleted" && ex.OrgID == OrgID && ints1.Contains(ex.AdvanceStatus)
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

           //Get Advance for an Activity
        [Route("Organization/{OrgID}/Activity/{id}")]
        [HttpGet]
           public IEnumerable<ViewAdvanceItemName> GetByActivity(int id)
        {
            try
            {
                ctx = new GASEntities();
                var expData = (from ex in ctx.ViewAdvanceItemNames
                               where ex.ActivityID == id && ex.Status != "Deleted"
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

           // Get Advance by Approver/Manager and status
        [Route("Organization/{OrgID}/Approver/{id}/Status/{Status}")]
        [HttpGet]
        public IEnumerable<ViewAdvance> GetByApprover(int id, int OrgID, String Status)
        {
            try
            {
                String[] ints1 = new String[0];
                if (Status == "Show All")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                    ints1[1] = "Added";
                    ints1[2] = "Initiated";
                    ints1[3] = "Submitted";
                    ints1[4] = "Approved";
                }
                else if (Status == "Open")
                {
                    ints1 = new String[4];
                    ints1[0] = "Submitted";
                    ints1[1] = "Approved";
                }
                else if (Status == "Closed")
                {
                    ints1 = new String[5];
                    ints1[0] = "Paid";
                }
                var ctx = new GASEntities();
                var expData = (from ex in ctx.ViewAdvances
                               orderby ex.AdvanceModifiedDate ascending
                               where ex.Approver == id && ex.OrgID == OrgID && ex.AdvanceStatus != "Deleted" && ints1.Contains(ex.AdvanceStatus)
                               select ex);
                return expData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        // Add Advance request
           [Route("Add")]
           [HttpPost]
        public HttpResponseMessage PostAdd([FromBody]Advance ai)
        {
            String resp = "{\"Response\":\"Undefine\"}";
            try
            {
                ctx = new GASEntities();
               
                using (var dbContextTransaction = ctx.Database.BeginTransaction())
                {
                    try
                    {
                        ai.CreationDate = DateTime.UtcNow;
                        if (ai != null)
                        {
                            AdvanceItem advanceItem = new AdvanceItem();
                            advanceItem.ActivityID = ai.ActivityID;
                            advanceItem.AdvanceName = ai.AdvanceName;
                            advanceItem.AdvanceRemarks = ai.AdvanceRemarks;
                            advanceItem.CreationDate = ai.CreationDate;
                            advanceItem.ReceiveAmount = ai.ReceiveAmount;
                            advanceItem.RequestAmount = ai.RequestAmount;
                            advanceItem.SelectedRow = false;
                            advanceItem.Status = ai.Status;
                           

                            var id = ctx.AdvanceItems.Add(advanceItem);
                            ctx.SaveChanges();
                            Transaction trans_GST = new Transaction();
                            trans_GST.AccID = ai.AccID;
                            trans_GST.Deposit = 0;
                            trans_GST.EntryDate = DateTime.UtcNow;
                            trans_GST.InvoiceID = advanceItem.AdvanceID;
                            trans_GST.OrgID = ai.OrgID;
                            trans_GST.ProjectID = ai.ProjectID;
                            trans_GST.TransactionDate = ai.CreationDate;
                            trans_GST.Withdraw = ai.ReceiveAmount;
                            trans_GST.TransType = "Advance";
                            trans_GST.TransactionRemarks = ai.AdvanceRemarks;
                            trans_GST.TransName = ai.AdvanceName;


                             CalculateBalance(trans_GST);
                            ctx.Transactions.Add(trans_GST);

                            ctx.SaveChanges();
                          
                        }
                        dbContextTransaction.Commit();
                        resp = "{\"Response\":\"OK\"}";

                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                        resp = "{\"Response\":\"Fail\"}";

                    }

                }





            }
            catch (Exception ex)
            {
                int a = 1;
                resp = "{\"Response\":\"Fail\"}";

            }
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(resp, System.Text.Encoding.UTF8, "application/json");
            return response;

        }

       


        private void CalculateBalance(Transaction trans)
        {

            var prevBalance = 0;
            var prevT = (from tr in ctx.Transactions
                         where tr.OrgID == trans.OrgID
                         orderby tr.TransID descending
                         select tr.Balance).Take(1);
            if (prevT.Count() > 0)
            {
                prevBalance = Convert.ToInt32(prevT.FirstOrDefault());
            }

            var prevAccT = (from tr in ctx.Transactions
                            where tr.AccID == trans.AccID && tr.OrgID == trans.OrgID
                            orderby tr.TransID descending
                            select tr.AccountBalance).Take(1);
            var prevAccBalance = 0;
            if (prevAccT.Count() > 0)
            {
                prevAccBalance = Convert.ToInt32(prevAccT.FirstOrDefault());
            }

            trans.Balance = prevBalance + trans.Deposit - trans.Withdraw;
            trans.AccountBalance = prevAccBalance + trans.Deposit - trans.Withdraw;
            trans.EntryDate = System.DateTime.UtcNow;

            // ctx.Transactions.Add(trans);
            //ctx.SaveChanges();
            return ;
        }
    }
}
