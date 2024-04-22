using GAS;
using System;

namespace GAS.ModelsDTO
{
    public class ActivityDTO
    {
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public int ProjectID { get; set; }
        public int CreatedById { get; set; }
        public string CreatedByName { get; set; }
        public string ActivityDescription { get; set; }
        public System.DateTime CreationDate { get; set; }
        public bool SelectedRow { get; set; }
        public int OrgID { get; set; }
        public int ApproverID { get; set; }
        public string ApproverName { get; set; }
        public string ActivityStatus { get; set; }
        public int Expenses { get; set; }
        public int Settlement { get; set; }
        public int Advance { get; set; }
    }
}
