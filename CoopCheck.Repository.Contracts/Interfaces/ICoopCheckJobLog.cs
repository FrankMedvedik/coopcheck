using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface ICoopCheckJobLog
    {
        bool active { get; set; }
        string clientid { get; set; }
        string clientname { get; set; }
        string client_job__ { get; set; }
        DateTime? created { get; set; }
        DateTime? ended { get; set; }
        string invoice { get; set; }
        bool? ismaster { get; set; }
        string jobname { get; set; }
        int jobnum { get; set; }
        int? location { get; set; }
        string po__ { get; set; }
        string primethod { get; set; }
        float? prodrate { get; set; }
        string product { get; set; }
        string projmgr { get; set; }
        string ref_jobs { get; set; }
        string scdmethod { get; set; }
        string scdprjmgr { get; set; }
        DateTime? started { get; set; }
        int? total { get; set; }
    }
}