using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;

namespace CoopCheck.WPF.Content.Report
{
    /// <summary>
    /// Description for GlobalReportCriteriaView.
    /// </summary>
    public partial class GlobalReportCriteriaView : UserControl
    {
        private Visibility _showControl;
        private GlobalReportCriteria grc{ get; set; }

        /// <summary>
        /// Initializes a new instance of the GlobalReportCriteriaView class.
        /// </summary>
        public GlobalReportCriteriaView()
        {
            InitializeComponent();
            DataContext = this;
            grc = new GlobalReportCriteria();
            Messenger.Default.Register<NotificationMessage<GlobalReportCriteria>>(this, message =>
            {
                    grc = message.Content;
                    grc.ReportDateRange = DR.DateRange;
            });
        }

        public Visibility ShowControl
        {
            get { return _showControl; }
            set { _showControl = value; }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            grc.ReportDateRange = DR.DateRange;
            Messenger.Default.Send(new NotificationMessage<GlobalReportCriteria>(this, grc,
                Notifications.GlobalReportCriteriaChanged));
        }
    }
}