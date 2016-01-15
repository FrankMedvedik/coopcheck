using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoopCheck.WPF.Models;
using GalaSoft.MvvmLight.Messaging;
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
            Messenger.Default.Register<NotificationMessage<BankFileViewModel>>(this, message =>
            {
                _vm.BankFile = message.Content;
            });
            prcv.StartDate.Visibility = Visibility.Collapsed;
            prcv.EndDate.Visibility = Visibility.Collapsed;
        }
        

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {

            _vm.PaymentReportCriteria = prcv.PaymentReportCriteria;
            Recon.IsExpanded = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();

        }


        private void ExportToExcel()
        {
            DgStats.SelectAllCells();
            DgStats.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, DgStats);
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            DgStats.UnselectAllCells();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("Reconcile{0}.csv", Path.GetFileName(_vm.BankFile.FilePath));
            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName);
                file.WriteLine(result);
                file.Close();
            }
        }

    }

}
