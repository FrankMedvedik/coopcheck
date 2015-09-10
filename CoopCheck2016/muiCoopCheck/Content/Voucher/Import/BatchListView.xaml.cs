using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Content.Voucher.Import;
using Microsoft.Win32;
using CoopCheck.WPF.Models;

namespace CoopCheck.WPF.Pages.Voucher.Import
{
    /// <summary>
    /// Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class BatchListView : UserControl
    {
        private ImportWorksheetViewModel _vm;

        public BatchListView()
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
                _vm.ExcelFilePath = openfile.FileName;
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
           DependencyProperty.Register("Vouchers", typeof(List<VoucherImport>), typeof(Content.Voucher.Import.ImportWorksheetView),
               new PropertyMetadata(new List<VoucherImport>()));
    }
}
