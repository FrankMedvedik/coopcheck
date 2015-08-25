using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Batch.Import
{
    /// <summary>
    /// Interaction logic for importing the spreadsheet
    /// </summary>
    public partial class ImportView : UserControl
    {
        private ImportViewModel _vm;

        public ImportView()
        {
            InitializeComponent();
            _vm = new ImportViewModel();
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
           DependencyProperty.Register("Vouchers", typeof(List<VoucherImport>), typeof(ImportView),
               new PropertyMetadata(new List<VoucherImport>()));
    }
}
