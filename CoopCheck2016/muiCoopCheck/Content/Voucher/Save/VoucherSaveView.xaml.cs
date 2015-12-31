using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Converters;
using CoopCheck.WPF.Models;
using CoopCheck.WPF.Services;


namespace CoopCheck.WPF.Content.Voucher.Save
{
    public partial class VoucherSaveView : UserControl
    {
        private VoucherSaveViewModel _vm;
        
        public VoucherSaveView()
        {
            InitializeComponent();
            _vm = new VoucherSaveViewModel();
            DataContext = _vm;
        }

    }
    
}