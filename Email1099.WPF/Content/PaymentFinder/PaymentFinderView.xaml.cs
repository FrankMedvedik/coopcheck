using System.Windows;
using System.Windows.Controls;

namespace Email1099.WPF.Content.PaymentFinder
{
    public partial class PaymentFinderView : UserControl
    {
        public PaymentFinderView()
        {
            InitializeComponent();
            _vm = new PaymentFinderViewModel();
            DataContext = _vm;
        }

        private PaymentFinderViewModel _vm;
        
    }
}