using System;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Report;

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
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                //BatchesDG.Export();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Saving file - " + ex.Message);
            }
            
        }

        private void JobsDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_vm.SelectedJob != null)
            {
                pv.Job = _vm.SelectedJob;
                //pv.ReportDateRange = _vm.ReportDateRange;
                pv.Refresh();
            }
            else
                pv.ShowData = false;
        }

    }
}