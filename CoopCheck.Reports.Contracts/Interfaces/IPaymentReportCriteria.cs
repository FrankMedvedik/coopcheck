using System;
using System.ComponentModel;

namespace CoopCheck.Reports.Contracts.Interfaces
{
    public interface IPaymentReportCriteria
    {
        string JobNumber { get; set; }
        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        string CheckNumber { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string LastName { get; set; }
        bool IsPrinted { get; set; }
        bool IsCleared { get; set; }
        Ibank_account Account { get; set; }
        string FirstName { get; set; }
        int? BatchNumber { get; set; }
        string ToFormattedString(char token);
        string ToSummaryFormattedString(char token);
        event PropertyChangedEventHandler PropertyChanged;
    }
}