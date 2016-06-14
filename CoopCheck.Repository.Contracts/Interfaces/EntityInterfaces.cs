using System;
namespace CoopCheck.Repository.Contracts.Interfaces
{
        public interface Ibank_account
        {
            int account_id { get; set; }
            string account_name { get; set; }
            string account_dscr { get; set; }
            string account_number { get; set; }
            Nullable<decimal> balance { get; set; }
            Nullable<System.DateTime> last_rec_date { get; set; }
            Nullable<decimal> last_rec_balance { get; set; }
            string account_type { get; set; }
            Nullable<bool> IsDefault { get; set; }
            Nullable<bool> IsActive { get; set; }
        }


        public interface Ibatch
        {
            int batch_num { get; set; }
            Nullable<System.DateTime> batch_date { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<decimal> batch_amount { get; set; }
            Nullable<int> job_num { get; set; }
            string batch_dscr { get; set; }
            Nullable<System.DateTime> updated { get; set; }
            string thank_you_1 { get; set; }
            string study_topic { get; set; }
            string thank_you_2 { get; set; }
            string marketing_research_message { get; set; }
            Nullable<int> respondent_batch_id { get; set; }
            string usr { get; set; }
        }


        public interface Icheck_tran
        {
            int tran_id { get; set; }
            Nullable<System.DateTime> check_date { get; set; }
            Nullable<int> batch_num { get; set; }
            Nullable<int> account_id { get; set; }
            string check_num { get; set; }
            Nullable<decimal> tran_amount { get; set; }
            Nullable<decimal> cleared_amount { get; set; }
            Nullable<System.DateTime> cleared_date { get; set; }
            bool print_flag { get; set; }
            bool cleared_flag { get; set; }
            string person_id { get; set; }
            string name_prefix { get; set; }
            string first_name { get; set; }
            string middle_name { get; set; }
            string last_name { get; set; }
            string name_suffix { get; set; }
            string title { get; set; }
            string company { get; set; }
            string address_1 { get; set; }
            string address_2 { get; set; }
            string municipality { get; set; }
            string region { get; set; }
            string postal_code { get; set; }
            string country { get; set; }
            string phone_number { get; set; }
            string email { get; set; }
            Nullable<System.DateTime> updated { get; set; }
            string usr { get; set; }
        }


        public interface ICoopCheckClient
        {
            string ClientID { get; set; }
            string ClientName { get; set; }
            string Address { get; set; }
            string City { get; set; }
            string State { get; set; }
            string Zip { get; set; }
            string Attention { get; set; }
            string Phone { get; set; }
            string Fax { get; set; }
            string Email { get; set; }
            string Country { get; set; }
            Nullable<bool> inSolomon { get; set; }
        }


        public interface ICoopCheckJobLog
        {
            int jobnum { get; set; }
            string jobname { get; set; }
            bool active { get; set; }
            string client_job__ { get; set; }
            string clientid { get; set; }
            string clientname { get; set; }
            Nullable<System.DateTime> created { get; set; }
            Nullable<System.DateTime> ended { get; set; }
            string invoice { get; set; }
            Nullable<bool> ismaster { get; set; }
            Nullable<int> location { get; set; }
            string po__ { get; set; }
            string primethod { get; set; }
            Nullable<float> prodrate { get; set; }
            string product { get; set; }
            string projmgr { get; set; }
            string ref_jobs { get; set; }
            string scdmethod { get; set; }
            string scdprjmgr { get; set; }
            Nullable<System.DateTime> started { get; set; }
            Nullable<int> total { get; set; }
        }


        public interface IJobLog
        {
            int JobNum { get; set; }
            string ClientID { get; set; }
            string JobName { get; set; }
            bool ACTIVE { get; set; }
            Nullable<bool> IsMaster { get; set; }
            string priMethod { get; set; }
            string scdMethod { get; set; }
            Nullable<System.DateTime> Started { get; set; }
            Nullable<System.DateTime> Ended { get; set; }
            string ProjMgr { get; set; }
            string scdPrjMgr { get; set; }
            Nullable<float> ProdRate { get; set; }
            Nullable<int> TOTAL { get; set; }
            Nullable<int> Location { get; set; }
            string Client_Job__ { get; set; }
            string PO__ { get; set; }
            string product { get; set; }
            string ref_jobs { get; set; }
            string invoice { get; set; }
            Nullable<System.DateTime> created { get; set; }
        }


        public interface Ivoucher
        {
            int tran_id { get; set; }
            Nullable<int> batch_num { get; set; }
            Nullable<decimal> tran_amount { get; set; }
            string person_id { get; set; }
            string name_prefix { get; set; }
            string first_name { get; set; }
            string middle_name { get; set; }
            string last_name { get; set; }
            string name_suffix { get; set; }
            string title { get; set; }
            string company { get; set; }
            string address_1 { get; set; }
            string address_2 { get; set; }
            string municipality { get; set; }
            string region { get; set; }
            string postal_code { get; set; }
            string country { get; set; }
            string phone_number { get; set; }
            string email { get; set; }
            Nullable<System.DateTime> updated { get; set; }
        }


        public interface IvwBatchRpt
        {
            int batch_num { get; set; }
            string batch_dscr { get; set; }
            string job_year { get; set; }
            string clientid { get; set; }
            string clientname { get; set; }
            string jobname { get; set; }
            string projmgr { get; set; }
            Nullable<int> account_id { get; set; }
            string account_name { get; set; }
            Nullable<int> job_num { get; set; }
            Nullable<System.DateTime> batch_date { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<decimal> batch_amount { get; set; }
            string payment_type { get; set; }
            Nullable<int> total_cnt { get; set; }
            Nullable<int> open_cnt { get; set; }
            Nullable<int> cleared_cnt { get; set; }
            Nullable<decimal> open_amount { get; set; }
            Nullable<decimal> cleared_amount { get; set; }
            Nullable<decimal> total_amount { get; set; }
            string first_check_num { get; set; }
            string last_check_num { get; set; }
        }


        public interface IvwCheck
        {
            int batch_num { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<int> job_num { get; set; }
            string thank_you_1 { get; set; }
            string study_topic { get; set; }
            string thank_you_2 { get; set; }
            string marketing_research_message { get; set; }
            Nullable<System.DateTime> check_date { get; set; }
            string check_num { get; set; }
            Nullable<decimal> tran_amount { get; set; }
            string person_id { get; set; }
            string name_prefix { get; set; }
            string first_name { get; set; }
            string middle_name { get; set; }
            string last_name { get; set; }
            string name_suffix { get; set; }
            string title { get; set; }
            string company { get; set; }
            string address_1 { get; set; }
            string address_2 { get; set; }
            string municipality { get; set; }
            string region { get; set; }
            string postal_code { get; set; }
            string country { get; set; }
            bool print_flag { get; set; }
        }


        public interface IvwClientJobBatch
        {
            int batch_num { get; set; }
            Nullable<decimal> batch_amount { get; set; }
            Nullable<System.DateTime> batch_date { get; set; }
            string batch_dscr { get; set; }
            string marketing_research_message { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<int> respondent_batch_id { get; set; }
            string study_topic { get; set; }
            string thank_you_1 { get; set; }
            string thank_you_2 { get; set; }
            Nullable<System.DateTime> updated { get; set; }
            string usr { get; set; }
            bool active { get; set; }
            string client_job__ { get; set; }
            string clientid { get; set; }
            string clientname { get; set; }
            Nullable<System.DateTime> created { get; set; }
            Nullable<System.DateTime> ended { get; set; }
            string invoice { get; set; }
            Nullable<bool> ismaster { get; set; }
            string jobname { get; set; }
            int jobnum { get; set; }
            Nullable<int> location { get; set; }
            string po__ { get; set; }
            string primethod { get; set; }
            Nullable<float> prodrate { get; set; }
            string product { get; set; }
            string projmgr { get; set; }
            string ref_jobs { get; set; }
            string scdmethod { get; set; }
            string scdprjmgr { get; set; }
            Nullable<System.DateTime> started { get; set; }
            Nullable<int> total { get; set; }
        }


        public interface IvwJobRpt
        {
            int job_num { get; set; }
            string clientid { get; set; }
            string clientname { get; set; }
            string jobname { get; set; }
            string projmgr { get; set; }
            Nullable<int> batch_cnt { get; set; }
            Nullable<System.DateTime> first_batch_date { get; set; }
            Nullable<System.DateTime> last_batch_date { get; set; }
            Nullable<System.DateTime> first_pay_date { get; set; }
            Nullable<System.DateTime> last_pay_date { get; set; }
            Nullable<int> total_cnt { get; set; }
            Nullable<int> open_cnt { get; set; }
            Nullable<int> cleared_cnt { get; set; }
            Nullable<decimal> open_amount { get; set; }
            Nullable<decimal> cleared_amount { get; set; }
            Nullable<decimal> total_amount { get; set; }
            string first_check_num { get; set; }
            string last_check_num { get; set; }
            Nullable<System.DateTime> first_check_date { get; set; }
            Nullable<System.DateTime> last_check_date { get; set; }
            string job_year { get; set; }
        }


        public interface IvwOpenBatch
        {
            int batch_num { get; set; }
            Nullable<int> jobnum { get; set; }
            Nullable<System.DateTime> batch_date { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            string batch_dscr { get; set; }
            Nullable<System.DateTime> updated { get; set; }
            string clientid { get; set; }
            string clientname { get; set; }
            Nullable<System.DateTime> created { get; set; }
            string jobname { get; set; }
            string projmgr { get; set; }
            Nullable<int> voucher_cnt { get; set; }
            Nullable<decimal> total_amount { get; set; }
        }


        public interface IvwPayment
        {
            int tran_id { get; set; }
            int batch_num { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<int> job_num { get; set; }
            string thank_you_1 { get; set; }
            string study_topic { get; set; }
            string thank_you_2 { get; set; }
            string marketing_research_message { get; set; }
            Nullable<System.DateTime> check_date { get; set; }
            string check_num { get; set; }
            Nullable<decimal> tran_amount { get; set; }
            string person_id { get; set; }
            string name_prefix { get; set; }
            string first_name { get; set; }
            string middle_name { get; set; }
            string last_name { get; set; }
            string name_suffix { get; set; }
            string title { get; set; }
            string company { get; set; }
            string address_1 { get; set; }
            string address_2 { get; set; }
            string municipality { get; set; }
            string region { get; set; }
            string postal_code { get; set; }
            string country { get; set; }
            bool print_flag { get; set; }
            string email { get; set; }
            string phone_number { get; set; }
            Nullable<int> account_id { get; set; }
            Nullable<decimal> cleared_amount { get; set; }
            Nullable<System.DateTime> cleared_date { get; set; }
            bool cleared_flag { get; set; }
            Nullable<System.DateTime> updated { get; set; }
            string usr { get; set; }
        }


        public interface IvwPositivePay
        {
            string rpt { get; set; }
            string account_number { get; set; }
            string check_num { get; set; }
            Nullable<decimal> tran_amount { get; set; }
            Nullable<System.DateTime> check_date { get; set; }
            string payee { get; set; }
        }


        public interface IvwUnclearedBatch
        {
            int batch_num { get; set; }
            Nullable<int> job_num { get; set; }
            Nullable<System.DateTime> batch_date { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<int> voucher_cnt { get; set; }
            Nullable<decimal> total_amount { get; set; }
        }


        public interface IvwVoidedPayment
        {
            int tran_id { get; set; }
            int batch_num { get; set; }
            Nullable<System.DateTime> pay_date { get; set; }
            Nullable<int> job_num { get; set; }
            string thank_you_1 { get; set; }
            string study_topic { get; set; }
            string thank_you_2 { get; set; }
            string marketing_research_message { get; set; }
            Nullable<System.DateTime> check_date { get; set; }
            string check_num { get; set; }
            Nullable<decimal> tran_amount { get; set; }
            string person_id { get; set; }
            string name_prefix { get; set; }
            string first_name { get; set; }
            string middle_name { get; set; }
            string last_name { get; set; }
            string name_suffix { get; set; }
            string title { get; set; }
            string company { get; set; }
            string address_1 { get; set; }
            string address_2 { get; set; }
            string municipality { get; set; }
            string region { get; set; }
            string postal_code { get; set; }
            string country { get; set; }
            bool print_flag { get; set; }
            string email { get; set; }
            string phone_number { get; set; }
            Nullable<int> account_id { get; set; }
            Nullable<decimal> cleared_amount { get; set; }
            Nullable<System.DateTime> cleared_date { get; set; }
            bool cleared_flag { get; set; }
            Nullable<System.DateTime> updated { get; set; }
            string usr { get; set; }
        }

    }
}
