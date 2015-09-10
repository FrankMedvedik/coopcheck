using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;

namespace CoopCheck.WPF.Content.BankAccount
{
    /// <summary>
    /// Interaction logic for BatchEditView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        private AccountViewModel _vm;
        public AccountView()
        {
            InitializeComponent();
            _vm = new AccountViewModel();
            DataContext = _vm;
        }
        
        public Account SelectedAccount
        {
            get { return _vm.SelectedAccount; }
            set
            {
                _vm.SelectedAccount = value;
            }
        }

        public static readonly DependencyProperty SelectedAccountProperty =
                DependencyProperty.Register("SelectedAccount", typeof(Account), typeof(AccountView),
                    new PropertyMetadata(Account.NewAccount()));


    }
}
