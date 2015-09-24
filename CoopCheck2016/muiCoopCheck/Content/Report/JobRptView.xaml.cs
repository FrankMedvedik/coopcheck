﻿using System;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Report;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Report
{
    /// <summary>
    /// </summary>
    public partial class JobRptView : UserControl
    {
        private JobRptViewModel _vm = null;
        
        public JobRptView()
        {
            InitializeComponent();
            _vm = new JobRptViewModel();
            DataContext = _vm;
        }

        public PaymentReportCriteria PaymentReportCriteria {
            get { return _vm.PaymentReportCriteria; }
            set
            {
                _vm.PaymentReportCriteria = value;
                _vm.RefreshAll();
            }
        }

    }
}