using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class Common
    {

        private static XPenEntities _ctx;
        public static Transaction CreateTransactionForExpenseSettlement(ExpenseItem expen, XPenEntities ctx)
        {
            _ctx = ctx;
            var trans = new Transaction();
            try
            {
                trans.TransName = expen.ItemName;
                trans.AccID = expen.AccountId;
                trans.Deposit = 0;
                trans.Withdraw = expen.ReceiveAmount;
                trans.EntryDate = DateTime.Now;
                trans.TransactionDate = DateTime.Now;
                trans.ProjectID = expen.ProjectID;
                trans.ActivityID = expen.ActivityID;
                trans.TransactionID = 0;
                trans.TransactionRemarks = expen.ExpenseDescription;
                trans.OrgID = expen.OrganizationId;
                trans.TransType = "Expense";
                trans.InvoiceID = 0;
                CalculateBalance(trans);
            }
            catch (Exception ex)
            {

            }
            return trans;
        }


        private static Transaction CalculateBalance(Transaction trans)
        {

            var prevBalance = 0;
            var prevT = (from tr in _ctx.Transactions
                         where tr.OrgID == trans.OrgID
                         orderby tr.TransID descending
                         select tr.Balance).Take(1);
            if (prevT.Count() > 0)
            {
                prevBalance = Convert.ToInt32(prevT.FirstOrDefault());
            }

            var prevAccT = (from tr in _ctx.Transactions
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
            return trans;
        }

    }
}