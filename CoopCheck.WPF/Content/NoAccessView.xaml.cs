using System.Windows.Controls;

namespace CoopCheck.WPF.Content
{
    public partial class NoAccessView : UserControl
    {
        public NoAccessView()
        {
            InitializeComponent();
            DataContext = this;
        }

        public string UserName
        {
            get { return UserAuth.Instance.UserName; }
        }
    }
}