using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface IJobLog
    {
        bool ACTIVE { get; set; }
        string ClientID { get; set; }
        string Client_Job__ { get; set; }
        DateTime? created { get; set; }
        DateTime? Ended { get; set; }
        string invoice { get; set; }
        bool? IsMaster { get; set; }
        string JobName { get; set; }
        int JobNum { get; set; }
        int? Location { get; set; }
        string PO__ { get; set; }
        string priMethod { get; set; }
        float? ProdRate { get; set; }
        string product { get; set; }
        string ProjMgr { get; set; }
        string ref_jobs { get; set; }
        string scdMethod { get; set; }
        string scdPrjMgr { get; set; }
        DateTime? Started { get; set; }
        int? TOTAL { get; set; }
    }
}