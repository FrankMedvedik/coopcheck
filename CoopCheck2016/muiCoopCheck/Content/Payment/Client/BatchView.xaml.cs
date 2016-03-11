using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.Client
{
    /// <summary>
    /// </summary>
    public partial class BatchView : UserControl
    {
        private BatchViewModel _vm = null;

        public BatchView()
        {
            InitializeComponent();
            _vm = new BatchViewModel();
            DataContext = _vm;
        }

        private void ClipBoard_Click(object sender, RoutedEventArgs e)
        {
            CopyToClipboard();

        }


        private void CopyToClipboard()
        {
            dgBatches.SelectAllCells();
            dgBatches.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgBatches);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            dgBatches.UnselectAllCells();

        }

        private void Excel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();

        }


        private void ExportToExcel()
        {
            dgBatches.SelectAllCells();
            dgBatches.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgBatches);
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            dgBatches.UnselectAllCells();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("Job.{0}.csv", _vm.SelectedBatch.job_num);
            if (saveFileDialog.ShowDialog() == true)
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(saveFileDialog.FileName);
                //file.WriteLine(result.Replace(",", " " ));
                file.WriteLine(result);
                file.Close();
            }
        }


    }
}