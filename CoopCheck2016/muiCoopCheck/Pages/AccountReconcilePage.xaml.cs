using System.Windows.Controls;
using CoopCheck.WPF.Content;

namespace CoopCheck.WPF.Pages
{
  
    public partial class AccountReconcilePage : UserControl
    {
        public AccountReconcilePage()
        {
            InitializeComponent();
            DataContext = this;
            if (UserAuth.Instance.CanRead)
            {
                arv.Visibility = System.Windows.Visibility.Visible;
                nov.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                arv.Visibility = System.Windows.Visibility.Collapsed;
                nov.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
    }

