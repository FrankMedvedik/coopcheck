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
    
    public partial class vwPositivePay
    {
        public string rpt { get; set; }
        public string account_number { get; set; }
        public string check_num { get; set; }
        public Nullable<decimal> tran_amount { get; set; }
        public Nullable<System.DateTime> check_date { get; set; }
        public string payee { get; set; }
    }
}
