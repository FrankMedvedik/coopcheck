﻿using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    /// <summary>
    /// </summary>
    public partial class JobView : UserControl
    {
        private readonly JobViewModel _vm;

        public JobView()
        {
            InitializeComponent();
            _vm = new JobViewModel();
            DataContext = _vm;
        }

        public ClientReportCriteria ClientReportCriteria
        {
            get { return _vm.ClientReportCriteria; }
            set
            {
                _vm.ClientReportCriteria = value;
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
            var result = (string) Clipboard.GetData(DataFormats.CommaSeparatedValue);
            //String result = (string)Clipboard.GetData(DataFormats..Text);
            dgJobs.UnselectAllCells();
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = string.Format("Jobs{0}.csv", ClientReportCriteria.ToFormattedString('.'));
            if (saveFileDialog.ShowDialog() == true)
            {
                var file = new StreamWriter(saveFileDialog.FileName);
                //file.WriteLine(result.Replace(",", " " ));
                file.WriteLine(result);
                file.Close();
            }
        }
    }
}