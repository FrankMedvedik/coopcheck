﻿using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Report;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Report
{
    /// <summary>
    /// </summary>
    public partial class PositivePayRptView : UserControl
    {
        private PositiveRptViewModel _vm = null;
        
        public PositivePayRptView()
        {
            InitializeComponent();
            _vm = new PositiveRptViewModel();
            DataContext = _vm;
        }
 

      private void btnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.PositivePays.Count == 0) return;
            SaveFileDialog saveFileDialog = new SaveFileDialog( );
            saveFileDialog.FileName = string.Format("PositivePay.{0}.{1}.{2}.txt", _vm.GlobalReportCriteria.SelectedAccount.account_name, 
                _vm.GlobalReportCriteria.ReportDateRange.StartRptDate.ToString("yyyyMMdd"),
                  _vm.GlobalReportCriteria.ReportDateRange.EndRptDate.ToString("yyyyMMdd"));

            if (saveFileDialog.ShowDialog() == true)
                File.WriteAllLines(saveFileDialog.FileName, _vm.PositivePayData);
        }
     
    }
}