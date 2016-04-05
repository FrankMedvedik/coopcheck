﻿using System.Windows.Controls;
using CoopCheck.WPF.Content;

namespace CoopCheck.WPF.Pages
{
    /// <summary>
    /// Interaction logic for AccountsPage.xaml
    /// </summary>
    public partial class AccountsPage : UserControl
    {
        public AccountsPage()
        {
            InitializeComponent();
            DataContext = this;
            if (UserAuth.Instance.CanRead)
            {
                av.Visibility = System.Windows.Visibility.Visible;
                nov.Visibility = System.Windows.Visibility.Collapsed;

            }
            else
            {
                av.Visibility = System.Windows.Visibility.Collapsed;
                nov.Visibility = System.Windows.Visibility.Visible;
            }

        }
    }
    }

