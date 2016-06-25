using System.Windows;
using System.Windows.Controls;
using CoopCheck.Reports.Contracts.Models;

namespace CoopCheck.WPF.Content.AccountManagement
{
    /// <summary>
    ///     Interaction logic for BatchEditView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public static readonly DependencyProperty SelectedAccountProperty =
            DependencyProperty.Register("SelectedAccount", typeof(BankAccount), typeof(AccountView),
                new PropertyMetadata(new BankAccount()));

        private readonly IAccountViewModel _vm;

        public AccountView(IAccountViewModel accountViewModel)
        {
            InitializeComponent();
            _vm = accountViewModel;
            DataContext = _vm;
        }

        public BankAccount SelectedAccount
        {
            get { return _vm.SelectedAccount; }
            set { _vm.SelectedAccount = value; }
        }
    }
}