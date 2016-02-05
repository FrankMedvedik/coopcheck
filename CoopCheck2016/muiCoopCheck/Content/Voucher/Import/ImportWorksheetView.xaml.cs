using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.Voucher.Import
{
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
            //openfile.DefaultExt = ".xls";
            //openfile.Filter = "(.xls|*.xls|.xlsx|*.xlsx)";
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

        private async void cbSheets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                cbSheets.IsEnabled = false;
                _vm.Status = new StatusInfo
                {
                    StatusMessage = "Importing worksheet...",
                    ErrorMessage = "",
                    IsBusy = true
                };

                await Task.Factory.StartNew(() =>
                {
                    _vm.LoadWorkSheetData();
                });
                cbSheets.IsEnabled = true;
            }
            catch (Exception ex)
            {
                var s = new StatusInfo();
                s.ErrorMessage = ex.Message;
                s.StatusMessage = "import failed";
                _vm.Status = s;
            }
        }
 
    }

}
