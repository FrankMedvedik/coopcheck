using System;

namespace CoopCheck.Reports.Contracts.Models
{
    public class JobLog 
    {
        public bool ACTIVE { get; set; }
        public string ClientID { get; set; }
        public string Client_Job__ { get; set; }
        public DateTime? created { get; set; }
        public DateTime? Ended { get; set; }
        public string invoice { get; set; }
        public bool? IsMaster { get; set; }
        public string JobName { get; set; }
        public int JobNum { get; set; }
        public int? Location { get; set; }
        public string PO__ { get; set; }
        public string priMethod { get; set; }
        public float? ProdRate { get; set; }
        public string product { get; set; }
        public string ProjMgr { get; set; }
        public string ref_jobs { get; set; }
        public string scdMethod { get; set; }
        public string scdPrjMgr { get; set; }
        public DateTime? Started { get; set; }
        public int? TOTAL { get; set; }
    }
}
