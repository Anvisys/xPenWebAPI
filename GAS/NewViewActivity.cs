//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GAS
{
    using System;
    using System.Collections.Generic;
    
    public partial class NewViewActivity
    {
        public int ActivityID { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        public int EmployeeID { get; set; }
        public string Employee { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public int Approver { get; set; }
        public string ApproverName { get; set; }
        public int Settlement { get; set; }
        public System.DateTime PaidDate { get; set; }
        public Nullable<int> Expenses { get; set; }
        public Nullable<int> Received { get; set; }
        public Nullable<int> Balance { get; set; }
        public string ActivityStatus { get; set; }
        public int Advance { get; set; }
        public int OrgID { get; set; }
    }
}
