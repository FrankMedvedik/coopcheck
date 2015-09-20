using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CoopCheck.WPF.Services;

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : UserControl
    {
        public About()
        {
            InitializeComponent();
            UserName = Environment.UserDomainName + @"\\" + Environment.UserName;
            CanRead = UserAuthSvc.CanRead(UserName) ? "Can Read" : "No Read";
            CanWrite = UserAuthSvc.CanWrite(UserName) ? "Can Write" : "No write";

    }
        private string UserName;
        private string CanRead;
        private string CanWrite;
    }
}
