﻿using CoopCheck.Domain.Contracts.Models;
using CoopCheck.WPF.Content.PaymentReports.Criteria;
using GalaSoft.MvvmLight.Command;

namespace CoopCheck.WPF.Content.AccountManagement.Reconcile
{
    public interface IReconcileBankViewModel
    {
        AccountPaymentsViewModel AccountPayments { get; set; }
        BankFileViewModel BankFile { get; set; }
        bool CanClearChecks { get; set; }
        RelayCommand ClearMatchedChecksCommand { get; }
        RelayCommand MatchCheckCommand { get; }
        PaymentReportCriteria PaymentReportCriteria { get; set; }
        RelayCommand SaveReconciliationToExcelCommand { get; }
        bool ShowDetails { get; set; }
        StatusInfo Status { get; set; }

        bool CanClearChecksFunc();
        void ClearMatchedChecks();
        void GetPayments();
        void MatchCheck();
        void ResetState();
    }
}