using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Voucher.Import
{
    /// <summary>
    /// Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class ImportWorksheetView : UserControl
    {
        private ImportWorksheetViewModel _vm;

        public ImportWorksheetView()
        {
            InitializeComponent();
            _vm = new ImportWorksheetViewModel();
            DataContext = _vm;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openfile = new OpenFileDialog();
            openfile.DefaultExt = ".xls";
            openfile.Filter = "(.xls|*.xls|.xlsx|*.xlsx)";
            var browsefile = openfile.ShowDialog();
            if (browsefile == true)
            {
                try {
                    _vm.ExcelFilePath = openfile.FileName;
                }
                catch(Exception x)
                {
                    MessageBox.Show(String.Format("File Open Error: {0} ", x.Message));
                }
                cbSheets.IsEnabled = true;
            }
        }
        public List<VoucherImport> Vouchers
        {
            get { return _vm.VoucherImports.ToList(); }
            set
            {
                _vm.VoucherImports = new ObservableCollection<VoucherImport>(value);
            }
        }

        public static readonly DependencyProperty VouchersProperty =
           DependencyProperty.Register("Vouchers", typeof(List<VoucherImport>), typeof(ImportWorksheetView),
               new PropertyMetadata(new List<VoucherImport>()));

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _vm.CreateVoucherBatch();
            bev.VoucherImports = _vm.VoucherImports;
            bev.Visibility = Visibility.Visible;
            
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            bev.ResetState();
            _vm.ResetState();
            btnClose.Visibility = Visibility.Collapsed;
        }

    }

}
