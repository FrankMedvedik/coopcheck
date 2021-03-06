﻿
namespace CoopCheck.Repository
{
    using System;
    using System.Collections.Generic;

    public partial class vwJobRpt
    {
        public int job_num { get; set; }
        public string clientid { get; set; }
        public string clientname { get; set; }
        public string jobname { get; set; }
        public string projmgr { get; set; }
        public Nullable<int> batch_cnt { get; set; }
        public Nullable<System.DateTime> first_batch_date { get; set; }
        public Nullable<System.DateTime> last_batch_date { get; set; }
        public Nullable<System.DateTime> first_pay_date { get; set; }
        public Nullable<System.DateTime> last_pay_date { get; set; }
        public Nullable<int> total_cnt { get; set; }
        public Nullable<int> open_cnt { get; set; }
        public Nullable<int> cleared_cnt { get; set; }
        public Nullable<decimal> open_amount { get; set; }
        public Nullable<decimal> cleared_amount { get; set; }
        public Nullable<decimal> total_amount { get; set; }
        public string first_check_num { get; set; }
        public string last_check_num { get; set; }
        public Nullable<System.DateTime> first_check_date { get; set; }
        public Nullable<System.DateTime> last_check_date { get; set; }
        public string job_year { get; set; }
    }
}
