using System.Windows;
using System.Windows.Controls;
using CoopCheck.Repository.Contracts.Interfaces;

namespace CoopCheck.WPF.Content.AccountManagement
{
    /// <summary>
    ///     Interaction logic for BatchEditView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public static readonly DependencyProperty SelectedAccountProperty =
            DependencyProperty.Register("SelectedAccount", typeof(Ibank_account), typeof(AccountView),
                new PropertyMetadata(new Domain.Models.Reports.BankAccount()));

        private readonly IAccountViewModel _vm;

        public AccountView(IAccountViewModel accountViewModel)
        {
            InitializeComponent();
            _vm = accountViewModel;
            DataContext = _vm;
        }

        public Domain.Models.Reports.BankAccount SelectedAccount
        {
            get { return _vm.SelectedAccount; }
            set { _vm.SelectedAccount = value; }
        }
    }
}