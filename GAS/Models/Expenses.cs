using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class Expenses
    {
        public int EmployeeID { get; set; }
        public int ExpenseAmount { get; set; }

        public int ReceiveAmount { get; set; }
        public int ActivityCount { get; set; }
        public string Status { get; set; }

        public DateTime Date { get; set; }
    }
}