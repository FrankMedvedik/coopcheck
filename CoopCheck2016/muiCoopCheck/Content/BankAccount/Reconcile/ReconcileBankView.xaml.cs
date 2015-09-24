using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Import;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{

    public partial class ReconcileBankView : UserControl
    {
        private ReconcileBankViewModel _vm;

        public ReconcileBankView()
        {
            InitializeComponent();
            _vm = new ReconcileBankViewModel();
            DataContext = _vm;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openfile = new OpenFileDialog();
            openfile.DefaultExt = ".csv";
            openfile.Filter = "csv | *.csv";
            var browsefile = openfile.ShowDialog();
            if (browsefile == true)
            {
                try {
                    _vm.FilePath = openfile.FileName;
                }
                catch(Exception x)
                {
                    MessageBox.Show(String.Format("File Open Error: {0} ", x.Message));
                }
            }
        }
        public List<BankClearTransaction> BankClearTransactions
        {
            get { return _vm.BankClearTransactions.ToList(); }
            set
            {
                _vm.BankClearTransactions = new ObservableCollection<BankClearTransaction>(value);
            }
        }

        public static readonly DependencyProperty VouchersProperty =
           DependencyProperty.Register("BankClearTransactions", typeof(List<BankClearTransaction>), typeof(ReconcileBankView),
               new PropertyMetadata(new List<BankClearTransaction>()));

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            //_vm.CreateVoucherBatch();
            //bev.VoucherImports = _vm.VoucherImports;
            //bev.Visibility = Visibility.Visible;
            //btnClose.Visibility = Visibility.Visible;
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();
            //bev.Visibility = Visibility.Collapsed;
            btnClose.Visibility = Visibility.Collapsed;
        }

    }

}
