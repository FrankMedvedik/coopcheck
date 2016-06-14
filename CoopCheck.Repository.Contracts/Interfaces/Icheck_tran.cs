using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
    public interface Icheck_tran
    {
        int? account_id { get; set; }
        string address_1 { get; set; }
        string address_2 { get; set; }
        int? batch_num { get; set; }
        DateTime? check_date { get; set; }
        string check_num { get; set; }
        decimal? cleared_amount { get; set; }
        DateTime? cleared_date { get; set; }
        bool cleared_flag { get; set; }
        string company { get; set; }
        string country { get; set; }
        string email { get; set; }
        string first_name { get; set; }
        string last_name { get; set; }
        string middle_name { get; set; }
        string municipality { get; set; }
        string name_prefix { get; set; }
        string name_suffix { get; set; }
        string person_id { get; set; }
        string phone_number { get; set; }
        string postal_code { get; set; }
        bool print_flag { get; set; }
        string region { get; set; }
        string title { get; set; }
        decimal? tran_amount { get; set; }
        int tran_id { get; set; }
        DateTime? updated { get; set; }
        string usr { get; set; }
    }
}