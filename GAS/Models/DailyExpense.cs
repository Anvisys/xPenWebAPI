using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class DailyExpense
    {
       public DateTime ExpenseDate { get; set; }

       public int ExpenseAmount { get; set; }

       public int ReceiveAmount { get; set; }
       public int Balance { get; set; }

       public string Status { get; set; }
    }
}