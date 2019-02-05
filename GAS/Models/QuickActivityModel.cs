using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class QuickActivityModel
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public int EmployeeID { get; set; }
        public int ProjectID { get; set; }
        public int CreatedBy { get; set; }
        public string ActivityDescription { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool SelectedRow { get; set; }

        public int ExpenseAmount { get; set; }

        public String ActivityStatus { get; set; }
        public int OrgID { get; set; }
        public int ApproverID { get; set; }
    }
}