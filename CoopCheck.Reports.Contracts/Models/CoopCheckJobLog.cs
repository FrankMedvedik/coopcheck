using System;

namespace CoopCheck.Reports.Contracts.Models
{
    public class CoopCheckJobLog
    {
        public int jobnum { get; set; }
        public string jobname { get; set; }
        public bool active { get; set; }
        public string client_job__ { get; set; }
        public string clientid { get; set; }
        public string clientname { get; set; }
        public Nullable<System.DateTime> created { get; set; }
        public Nullable<System.DateTime> ended { get; set; }
        public string invoice { get; set; }
        public Nullable<bool> ismaster { get; set; }
        public Nullable<int> location { get; set; }
        public string po__ { get; set; }
        public string primethod { get; set; }
        public Nullable<float> prodrate { get; set; }
        public string product { get; set; }
        public string projmgr { get; set; }
        public string ref_jobs { get; set; }
        public string scdmethod { get; set; }
        public string scdprjmgr { get; set; }
        public Nullable<System.DateTime> started { get; set; }
        public Nullable<int> total { get; set; }
    }
}

