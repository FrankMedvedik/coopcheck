using System;

namespace CoopCheck.Repository.Contracts.Interfaces
{
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
}