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
    
    public partial class dsa_GetVoucher_Result
    {
        public int tran_id { get; set; }
        public Nullable<int> batch_num { get; set; }
        public Nullable<decimal> tran_amount { get; set; }
        public string person_id { get; set; }
        public string name_prefix { get; set; }
        public string first_name { get; set; }
        public string middle_name { get; set; }
        public string last_name { get; set; }
        public string name_suffix { get; set; }
        public string title { get; set; }
        public string company { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public string municipality { get; set; }
        public string region { get; set; }
        public string postal_code { get; set; }
        public string country { get; set; }
        public string phone_number { get; set; }
        public string email { get; set; }
        public Nullable<System.DateTime> updated { get; set; }
    }
}
