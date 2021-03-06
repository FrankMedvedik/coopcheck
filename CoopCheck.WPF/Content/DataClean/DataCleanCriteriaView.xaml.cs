﻿using System.Windows.Controls;
using DataClean.Models;

namespace CoopCheck.WPF.Content.DataClean
{
    public partial class DataCleanCriteriaView : UserControl
    {
        private readonly DataCleanCriteriaViewModel _vm;

        public DataCleanCriteriaView()
        {
            InitializeComponent();
            _vm = new DataCleanCriteriaViewModel(new DataCleanCriteria

            {
                AutoFixAddressLine1 = true,
                AutoFixCity = true,
                AutoFixPostalCode = true,
                AutoFixState = true,
                ForceValidation = false
            });

            DataContext = _vm;
        }

        public DataCleanCriteria Criteria
        {
            get { return _vm.Model; }
        }
    }
}