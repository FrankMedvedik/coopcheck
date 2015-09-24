using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Report
{
    public partial class PaymentReportView : UserControl
    {
        private ObservableCollection<vwPayment> _payments;

        public PaymentReportView()
        {
            InitializeComponent();
        }

        public ObservableCollection<vwPayment> Payments
        {
            set { dgPayments.ItemsSource = value; }
        }

        public static readonly DependencyProperty PaymentsProperty =
           DependencyProperty.Register("Payments", typeof(ObservableCollection<vwPayment>), typeof(PaymentReportView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

    }

}
