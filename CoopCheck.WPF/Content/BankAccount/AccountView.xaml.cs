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
            DependencyProperty.Register("SelectedAccount", typeof(Models.BankAccount), typeof(AccountView),
                new PropertyMetadata(new Models.BankAccount()));

        private readonly IAccountViewModel _vm;

        public AccountView(IAccountViewModel accountViewModel)
        {
            InitializeComponent();
            _vm = accountViewModel;
            DataContext = _vm;
        }

        public Models.BankAccount SelectedAccount
        {
            get { return _vm.SelectedAccount; }
            set { _vm.SelectedAccount = value; }
        }
    }
}