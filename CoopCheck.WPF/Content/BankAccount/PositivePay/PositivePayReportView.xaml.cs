﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.PositivePay
{
    public partial class PositivePayReportView : UserControl
    {
        private readonly PositivePayReportViewModel _vm;

        public PositivePayReportView()
        {
            InitializeComponent();
            _vm = new PositivePayReportViewModel();
            DataContext = _vm;
            prcv.PaymentReportCriteria.StartDate = DateTime.Today;
            prcv.PaymentReportCriteria.EndDate = DateTime.Today;
        }


        private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.PositivePays.Count == 0) return;
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("PositivePay{0}.txt",
                _vm.PaymentReportCriteria.ToFormattedString('.'));
            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, _vm.PositivePayData);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
            _vm.RefreshAll();
        }
    }
}