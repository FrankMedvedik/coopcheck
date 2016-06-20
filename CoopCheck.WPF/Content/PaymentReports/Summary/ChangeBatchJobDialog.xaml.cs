using System;
using System.Windows;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.PaymentReports.Summary
{
    public partial class ChangeBatchJobDialog : ModernDialog
    {
        public ChangeBatchJobDialog()
        {
            InitializeComponent();
            DataContext = this;
            // define the dialog buttons
            Buttons = null;
            //this.OkButton.IsEnabled = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtNewJob.SelectAll();
            txtNewJob.Focus();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}