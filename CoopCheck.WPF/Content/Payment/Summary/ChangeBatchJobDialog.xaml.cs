using System;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Payment.Summary
{
    public partial class ChangeBatchJobDialog : ModernDialog
    {
        public ChangeBatchJobDialog()
        {
            InitializeComponent();
            DataContext = this;
            // define the dialog buttons
            this.Buttons = null;
            //this.OkButton.IsEnabled = false;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtNewJob.SelectAll();
            txtNewJob.Focus();
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }
    }
}
