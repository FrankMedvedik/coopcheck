//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CoopCheck.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class vwUnclearedBatch
    {
        public int batch_num { get; set; }
        public Nullable<int> job_num { get; set; }
        public Nullable<System.DateTime> batch_date { get; set; }
        public Nullable<System.DateTime> pay_date { get; set; }
        public Nullable<int> voucher_cnt { get; set; }
        public Nullable<decimal> total_amount { get; set; }
    }
}
