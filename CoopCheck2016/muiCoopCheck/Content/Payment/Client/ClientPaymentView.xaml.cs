﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository;
using CoopCheck.WPF.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Payment.Client
{
    /// <summary>
    /// </summary>
    public partial class ClientPaymentView : UserControl
    {
        private ClientPaymentViewModel _vm = null;
       
        public ClientPaymentView()
        {
            InitializeComponent();
            _vm = new ClientPaymentViewModel();
            DataContext = _vm;

            Messenger.Default.Register<NotificationMessage<vwBatchRpt>>(this, message =>
            {
                if (message.Notification == Notifications.JobFinderSelectedBatchChanged)
                {

                    _vm.ParentBatch = (message.Content);
                    pgv.Visibility = Visibility.Visible;
                }
            });


            Messenger.Default.Register<NotificationMessage<vwJobRpt>>(this, message =>
            {
                if (message.Notification == Notifications.JobFinderSelectedJobChanged)
                {
                    brv.Visibility = Visibility.Visible;
                    pgv.Visibility = Visibility.Collapsed;
                }
                
            });
        }

        private vwBatchRpt SelectedvwBatchRpt = null;

        private async Task RefreshChildren(vwBatchRpt vwBatchRpt)
        {
            if (vwBatchRpt == null) return;
               if((SelectedvwBatchRpt == null) 
                || (vwBatchRpt != SelectedvwBatchRpt))
                 await Task.Factory.StartNew(() =>
                {
                    _vm.ClientReportCriteria.SelectedBatch.batch_num = vwBatchRpt.batch_num;
                    _vm.RefreshAll();
                });
            SelectedvwBatchRpt = vwBatchRpt;
            pgv.Visibility = Visibility;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            jfrv.ClientReportCriteria = jrcv.ClientReportCriteria;
            _vm.ClientReportCriteria = jrcv.ClientReportCriteria;
            brv.Visibility = Visibility.Collapsed;
            pgv.Visibility = Visibility.Collapsed;
        }


        public ObservableCollection<vwPayment> AllPayments
        {
            get { return _vm.AllPayments; }
        }

        public static readonly DependencyProperty AllPaymentsProperty =
            DependencyProperty.Register("AllPayments", typeof(ObservableCollection<vwPayment>), typeof(ClientPaymentView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        public ObservableCollection<vwPayment> OpenPayments
        {
            get { return _vm.OpenPayments; }
            
        }

        public static readonly DependencyProperty OpenPaymentsProperty =
            DependencyProperty.Register("OpenPayments", typeof(ObservableCollection<vwPayment>), typeof(ClientPaymentView), new PropertyMetadata(new ObservableCollection<vwPayment>()));

        public ObservableCollection<vwPayment> ClosedPayments
        {
            get { return _vm.ClosedPayments; }
        }

        public static readonly DependencyProperty ClosedPaymentsProperty =
            DependencyProperty.Register("ClosedPayments", typeof(ObservableCollection<vwPayment>), typeof(ClientPaymentView), new PropertyMetadata(new ObservableCollection<vwPayment>()));
    }
}