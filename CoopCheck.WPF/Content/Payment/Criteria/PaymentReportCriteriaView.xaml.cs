﻿using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment.Criteria
{
    public partial class PaymentReportCriteriaView : UserControl
    {
        public static readonly DependencyProperty ShowAllAccountsOptionProperty =
            DependencyProperty.Register("ShowAllAccountsOption", typeof (bool), typeof (PaymentReportCriteriaView),
                new PropertyMetadata(false));

        private readonly PaymentReportCriteriaViewModel _vm;

        /// <summary>
        ///     Initializes a new instance of the CheckReportCriteriaView class.
        /// </summary>
        public PaymentReportCriteriaView()
        {
            InitializeComponent();
            _vm = new PaymentReportCriteriaViewModel();
            DataContext = _vm;
        }

        public PaymentReportCriteria PaymentReportCriteria
        {
            get { return _vm.PaymentReportCriteria; }
            set { _vm.PaymentReportCriteria = value; }
        }

        public bool ShowAllAccountsOption
        {
            get { return _vm.ShowAllAccountsOption; }
            set { _vm.ShowAllAccountsOption = value; }
        }

        public void ResetState()
        {
            _vm.ResetState();
        }

        private void CheckNumTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            _vm.EnableCheckNum = true;
            _vm.EnableMiscFields = false;
        }
    }
}