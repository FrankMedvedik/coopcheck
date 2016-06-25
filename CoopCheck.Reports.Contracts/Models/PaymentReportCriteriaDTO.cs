using System;
using System.Collections.Generic;
using CoopCheck.Reports.Contracts.Interfaces;

namespace CoopCheck.Reports.Contracts.Models
{
    public class PaymentReportCriteriaDto : IPaymentReportCriteria
    {
        public string JobNumber { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string CheckNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public bool IsPrinted { get; set; }
        public bool IsCleared { get; set; }
        public BankAccount Account { get; set; }
        public string FirstName { get; set; }
        public int? BatchNumber { get; set; }
        public string ToFormattedString(char token)
        {
            throw new NotImplementedException();
        }

        public string ToSummaryFormattedString(char token)
        {
            throw new NotImplementedException();
        }
    }
}

