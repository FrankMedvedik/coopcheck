using System;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.Reports.Models
{
    public class Paymnt : IvwPayment
    {
        public int? account_id { get; set; }
        public string address_1 { get; set; }
        public string address_2 { get; set; }
        public int batch_num { get; set; }
        public DateTime? check_date { get; set; }
        public string check_num { get; set; }
        public decimal? cleared_amount { get; set; }
        public DateTime? cleared_date { get; set; }
        public string cleared_date_exp { get; }
        public bool cleared_flag { get; set; }
        public string company { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public bool IsCheck { get; }
        public bool IsSwiftPayment { get; }
        public int? job_num { get; set; }
        public string last_name { get; set; }
        public string marketing_research_message { get; set; }
        public string middle_name { get; set; }
        public string municipality { get; set; }
        public string name_prefix { get; set; }
        public string name_suffix { get; set; }
        public DateTime? pay_date { get; set; }
        public string pay_date_exp { get; }
        public string person_id { get; set; }
        public string phone_number { get; set; }
        public string phone_number_exp { get; }
        public string postal_code { get; set; }
        public bool print_flag { get; set; }
        public string region { get; set; }
        public string study_topic { get; set; }
        public string thank_you_1 { get; set; }
        public string thank_you_2 { get; set; }
        public string title { get; set; }
        public decimal? tran_amount { get; set; }
        public string tran_amount_exp { get; }
        public int tran_id { get; set; }
        public DateTime? updated { get; set; }
        public string usr { get; set; }
    }
}
