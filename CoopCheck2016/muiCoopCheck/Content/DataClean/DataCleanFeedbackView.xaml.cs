﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using DataClean.Models;

namespace CoopCheck.WPF.Content.DataClean
{

    public partial class DataCleanFeedbackView : UserControl
    {
        private DataCleanFeedbackViewModel _vm;
        public DataCleanFeedbackView()
        {
            InitializeComponent();
             _vm = new DataCleanFeedbackViewModel();
            DataContext = _vm;

        }

        public ObservableCollection<DataCleanEvent> DataCleanEvents
        {
            get { return _vm.ValidationResults; }
            set { _vm.ValidationResults = new ObservableCollection<DataCleanEvent>(value); }
        }


    }
}
