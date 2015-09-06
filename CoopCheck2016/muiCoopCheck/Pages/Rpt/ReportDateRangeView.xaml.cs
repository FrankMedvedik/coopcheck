using System.Windows;
using System.Windows.Controls;

namespace muiCoopCheck.Pages.Rpt
{

    public partial class ReportDateRangeView : UserControl
    {
        private ReportDateRangeViewModel _vm = null;

        public ReportDateRangeView()
        {
            InitializeComponent();
            _vm = new ReportDateRangeViewModel();
            DataContext = _vm;

        }
        public ReportDateRange DateRange
        {
            get
            {
                return new ReportDateRange()
                {
                    StartRptDate = _vm.StartRptDate,
                    EndRptDate = _vm.EndRptDate
                };
            }
        }

        // Using a DependencyProperty as the backing store for NotificationMessage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DateRangeProperty =
            DependencyProperty.Register("DateRange", typeof(ReportDateRange), typeof(ReportDateRangeView), new PropertyMetadata(new ReportDateRange()));


   
        
    }
}