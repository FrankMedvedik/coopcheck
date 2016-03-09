using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.JobFinder
{
    /// <summary>
    /// </summary>
    public partial class JobFinderBatchReportView : UserControl
    {
        private JobFinderBatchReportViewModel _vm = null;

        public JobFinderBatchReportView()
        {
            InitializeComponent();
            _vm = new JobFinderBatchReportViewModel();
            DataContext = _vm;
        }

        public JobReportCriteria JobReportCriteria
        {
            get { return _vm.JobReportCriteria; }
            set
            {
                _vm.JobReportCriteria = value;
                _vm.RefreshAll();
            }
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