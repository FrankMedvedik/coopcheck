﻿using System;
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

namespace CoopCheck.WPF.Content.Settings
{
    /// <summary>
    /// Interaction logic for Appearance.xaml
    /// </summary>
    public partial class Appearance : UserControl
    {
        private AppearanceViewModel _vm; 
        public Appearance()
        {
            InitializeComponent();

            // create and assign the appearance view model
            _vm = new AppearanceViewModel();
            DataContext = _vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _vm.SaveSettings();
        }
    }
}
