using System;

namespace GAS.Models
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public int OrgId { get; set; }
        public int ProjectId { get; set; }
        public double ServiceCost { get; set; }
        public Nullable<double> CGST { get; set; }
        public Nullable<double> SGST { get; set; }
        public Nullable<double> IGST { get; set; }
        public Nullable<double> TDS { get; set; }
        public System.DateTime InvoiceDate { get; set; }
        public int InvoiceType { get; set; }
        public int Paid { get; set; }
        public int Received { get; set; }
    }
}
