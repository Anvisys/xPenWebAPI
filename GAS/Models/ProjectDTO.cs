using System;

namespace GAS.Models
{
    public class ProjectDTO
    {
        public int ProjectID { get; set; }
        public string ProjectNumber { get; set; }
        public string ClientName { get; set; }
        public string ProjectName { get; set; }
        public Nullable<int> ProjectValue { get; set; }
        public string ProjectDescription { get; set; }
        public System.DateTime ProjectCreationDate { get; set; }
        public int CreatedBy { get; set; }
        public int OrgID { get; set; }
        public string Status { get; set; }
        public double Spent {  get; set; }
        public double Received { get; set; }
        public int WorkCompletion { get; set; }
    }
}