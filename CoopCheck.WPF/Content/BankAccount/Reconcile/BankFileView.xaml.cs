using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{
    public partial class BankFileView : UserControl
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register("ViewModel", typeof (BankFileViewModel), typeof (BankFileView),
                new PropertyMetadata(new BankFileViewModel()));


        public BankFileView()
        {
            InitializeComponent();
            ViewModel = new BankFileViewModel();
            DataContext = ViewModel;
        }

        public BankFileViewModel ViewModel { get; set; }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openfile = new OpenFileDialog();
            openfile.DefaultExt = ".csv";
            openfile.Filter = "csv | *.csv";
            var browsefile = openfile.ShowDialog();
            if (browsefile == true)
            {
                try
                {
                    ViewModel.FilePath = openfile.FileName;
                }
                catch (Exception x)
                {
                    MessageBox.Show(string.Format("File Open Error: {0} ", x.Message));
                }
            }
        }
    }
}