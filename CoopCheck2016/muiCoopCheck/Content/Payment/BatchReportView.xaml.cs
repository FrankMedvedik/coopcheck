using System.Windows.Controls;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Content.Payment
{
    /// <summary>
    /// </summary>
    public partial class BatchReportView : UserControl
    {
        private BatchReportViewModel _vm = null;
        
        public BatchReportView()
        {
            InitializeComponent();
            _vm = new BatchReportViewModel();
            DataContext = _vm;
        }

        public PaymentReportCriteria PaymentReportCriteria {
            get { return _vm.PaymentReportCriteria; }
            set
            {
                _vm.PaymentReportCriteria = value;
                _vm.RefreshAll();
            }
        }

        //private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        //{
        //    try
        //    {
        //        //BatchesDG.Export();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error Saving file - " + ex.Message);
        //    }
            
        //}

      
    }
}