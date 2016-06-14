using System.Windows;
using System.Windows.Controls;

namespace CoopCheck.WPF.Content.BankAccount
{
    /// <summary>
    ///     Interaction logic for BatchEditView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public static readonly DependencyProperty SelectedAccountProperty =
            DependencyProperty.Register("SelectedAccount", typeof (Models.BankAccount), typeof (AccountView),
                new PropertyMetadata(new Models.BankAccount()));

        private readonly AccountViewModel _vm;

        public AccountView()
        {
            InitializeComponent();
            _vm = new AccountViewModel();
            DataContext = _vm;
        }

        public Models.BankAccount SelectedAccount
        {
            get { return _vm.SelectedAccount; }
            set { _vm.SelectedAccount = value; }
        }
    }
}