using System.Windows;
using System.Windows.Controls;
using CoopCheck.Library;
using CoopCheck.Repository;

namespace CoopCheck.WPF.Content.BankAccount
{
    public partial class AccountSelectView : UserControl
    {
        private AccountViewModel _vm;
        public AccountSelectView()
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
                DependencyProperty.Register("SelectedAccount", typeof(bank_account), typeof(AccountSelectView),
                    new PropertyMetadata(new bank_account()));


    }
}
