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
    
    public partial class TD
    {
        public int ID { get; set; }
        public int OrgID { get; set; }
        public int TDSDeducted { get; set; }
        public int TDSPayable { get; set; }
        public int PreviousTDS { get; set; }
        public double Penalty { get; set; }
        public System.DateTime TaxMonth { get; set; }
    }
}
