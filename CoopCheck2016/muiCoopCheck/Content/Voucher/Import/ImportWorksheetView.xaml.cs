using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Wrappers;
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
        public ObservableCollection<VoucherImport> Vouchers
        {
            get { return _vm.VoucherImports; }
            set
            {
                _vm.VoucherImports = value;
            }
        }

        public static readonly DependencyProperty VouchersProperty =
           DependencyProperty.Register("Vouchers", typeof(ObservableCollection<VoucherImport>), typeof(ImportWorksheetView),
               new PropertyMetadata(new ObservableCollection<VoucherImport>()));

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            _vm.CreateVoucherBatch();
            vlv.VoucherImports = _vm.VoucherImports.ToList();
            vlv.Visibility = Visibility.Visible;
            
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            _vm.ResetState();
            btnClose.Visibility = Visibility.Collapsed;
        }

    }

}
