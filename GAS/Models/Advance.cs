using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class Advance
    {
       public int ActivityID { get; set; }
        public string AdvanceName { get; set; }
        public int RequestAmount { get; set; }
        public int ReceiveAmount { get; set; }
        public string AdvanceRemarks { get; set; }
        public DateTime CreationDate { get; set; }
        public bool SelectedRow { get; set; }
        public string Status { get; set; }

        public int AccID { get; set; }
        public int ProjectID { get; set; }
        public int OrgID { get; set; }
        

    }
}