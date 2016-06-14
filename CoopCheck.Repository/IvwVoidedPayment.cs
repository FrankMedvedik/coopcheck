using System;

namespace CoopCheck.Repository
{
    public interface IvwVoidedPayment
    {
        int? account_id { get; set; }
        string address_1 { get; set; }
        string address_2 { get; set; }
        int batch_num { get; set; }
        DateTime? check_date { get; set; }
        string check_num { get; set; }
        decimal? cleared_amount { get; set; }
        DateTime? cleared_date { get; set; }
        string cleared_date_exp { get; }
        bool cleared_flag { get; set; }
        string company { get; set; }
        string country { get; set; }
        string email { get; set; }
        string first_name { get; set; }
        int? job_num { get; set; }
        string last_name { get; set; }
        string marketing_research_message { get; set; }
        string middle_name { get; set; }
        string municipality { get; set; }
        string name_prefix { get; set; }
        string name_suffix { get; set; }
        DateTime? pay_date { get; set; }
        string pay_date_exp { get; }
        string person_id { get; set; }
        string phone_number { get; set; }
        string phone_number_exp { get; }
        string postal_code { get; set; }
        bool print_flag { get; set; }
        string region { get; set; }
        string study_topic { get; set; }
        string thank_you_1 { get; set; }
        string thank_you_2 { get; set; }
        string title { get; set; }
        decimal? tran_amount { get; set; }
        string tran_amount_exp { get; }
        int tran_id { get; set; }
        DateTime? updated { get; set; }
        string usr { get; set; }
    }
}