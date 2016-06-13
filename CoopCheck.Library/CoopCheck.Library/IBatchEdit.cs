namespace CoopCheck.Library
{
    public interface IBatchEdit
    {
        decimal? Amount { get; set; }
        string Date { get; set; }
        string Description { get; set; }
        int? JobNum { get; set; }
        string MarketingResearchMessage { get; set; }
        int Num { get; }
        string PayDate { get; set; }
        string StudyTopic { get; set; }
        string ThankYou1 { get; set; }
        string ThankYou2 { get; set; }
        string Updated { get; set; }
        VoucherList Vouchers { get; }
    }
}