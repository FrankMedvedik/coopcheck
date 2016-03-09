using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.JobFinder
{
    /// <summary>
    /// </summary>
    public partial class JobFinderReportView : UserControl
    {
        private JobFinderReportViewModel _vm = null;
        
        public JobFinderReportView()
        {
            InitializeComponent();
            _vm = new JobFinderReportViewModel();
            DataContext = _vm;
        }

        public JobReportCriteria JobReportCriteria {
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
            dgJobs.SelectAllCells();
            dgJobs.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, dgJobs);
            String result = (string)Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            dgJobs.UnselectAllCells();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("Jobs{0}.csv", JobReportCriteria.ToFormattedString('.'));
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