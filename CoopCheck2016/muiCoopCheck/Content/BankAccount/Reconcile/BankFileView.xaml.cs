﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CoopCheck.WPF.Models;
using Microsoft.Win32;

namespace CoopCheck.WPF.Content.BankAccount.Reconcile
{

    public partial class BankFileView : UserControl
    {
        private BankFileViewModel _vm;

        public BankFileViewModel ViewModel
        {
            get { return _vm; }
            set
            {
                _vm = value;
            }
        }


        public BankFileView()
        {
            InitializeComponent();
            _vm = new BankFileViewModel();
            DataContext = _vm;
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            var openfile = new OpenFileDialog();
            openfile.DefaultExt = ".csv";
            openfile.Filter = "csv | *.csv";
            var browsefile = openfile.ShowDialog();
            if (browsefile == true)
            {
                try {
                    _vm.FilePath = openfile.FileName;
                }
                catch(Exception x)
                {
                    MessageBox.Show(String.Format("File Open Error: {0} ", x.Message));
                }
            }
        }
        
        public static readonly DependencyProperty ViewModelProperty =
           DependencyProperty.Register("ViewModel", typeof(BankFileViewModel), typeof(BankFileView),
               new PropertyMetadata(new BankFileViewModel()));


    }

}
