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
    
    public partial class bank_account
    {
        public int account_id { get; set; }
        public string account_name { get; set; }
        public string account_dscr { get; set; }
        public string account_number { get; set; }
        public Nullable<decimal> balance { get; set; }
        public Nullable<System.DateTime> last_rec_date { get; set; }
        public Nullable<decimal> last_rec_balance { get; set; }
        public string account_type { get; set; }
        public Nullable<bool> IsDefault { get; set; }
    }
}
