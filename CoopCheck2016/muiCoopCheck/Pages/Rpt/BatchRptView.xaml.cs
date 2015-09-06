using System;
using System.Windows;
using System.Windows.Controls;

namespace muiCoopCheck.Pages.Rpt
{
    /// <summary>
    /// </summary>
    public partial class BatchRptView : UserControl
    {
        private BatchRptViewModel _vm = null;
        /// <summary>
        /// Initializes a new instance of the RecruiterView class.
        /// </summary>
        
        public BatchRptView()
        {
            InitializeComponent();
            _vm = new BatchRptViewModel();
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

        private void BatchesDG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (_vm.SelectedBatch != null)
            //{
            //    pv.Batch = _vm.SelectedBatch;
            //    pv.ReportDateRange = _vm.ReportDateRange;
            //    pv.Refresh();
            //}
            //else
            //    pv.ShowData = false;
        }

    }
}