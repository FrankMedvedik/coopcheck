using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.Repository;

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
        
        public bank_account SelectedAccount
        {
            get { return _vm.SelectedAccount; }
            set
            {
                _vm.SelectedAccount = value;
            }
        }

        public static readonly DependencyProperty SelectedAccountProperty =
                DependencyProperty.Register("SelectedAccount", typeof(bank_account), typeof(AccountView),
                    new PropertyMetadata(new bank_account()));


    }
}
