﻿using System.Windows;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Messaging;
using muiCoopCheck.Models;
using muiCoopCheck.Pages.Batch.Import;

namespace muiCoopCheck.Pages.Batch
{
    /// <summary>
    /// Interaction logic for VouchersPage.xaml
    /// </summary>
    public partial class ImportPage : UserControl
    {
        private ImportPageViewModel _vm;

        public ImportPage()
        {
            InitializeComponent();
            _vm = new ImportPageViewModel();
            DataContext = _vm;
            SetControlVisibility();
            btnNext.IsEnabled = true;
        }


        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (_vm.CurrentProcessingStep == ImportProcessStep.BatchEdit) ;
                bev.SelectedBatch.Save();
            _vm.GoToNextStep();
            SetControlVisibility();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            _vm.GoBackAStep();
            SetControlVisibility();
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            _vm.StartOver();
            SetControlVisibility();
        }


        private void SetControlVisibility()
        {
            switch (_vm.CurrentProcessingStep)
            {
                case ImportProcessStep.Import:
                    bev.Visibility = Visibility.Collapsed;
                    iv.Visibility = Visibility.Visible;
                    vv.Visibility = Visibility.Collapsed;
                    btnBack.Visibility = Visibility.Collapsed;
                    break;
                case ImportProcessStep.BatchEdit:
                    bev.Visibility = Visibility.Visible;
                    iv.Visibility = Visibility.Collapsed;
                    vv.Visibility = Visibility.Collapsed;
                    btnBack.Visibility = Visibility.Visible;
                    bev.Vouchers = iv.Vouchers;
                    break;
                case ImportProcessStep.Validate:
                    vv.Visibility = Visibility.Visible;
                    bev.Visibility = Visibility.Collapsed;
                    iv.Visibility = Visibility.Collapsed;
                    vv.Vouchers = bev.Vouchers;
                    btnBack.Visibility = Visibility.Visible;
                    break;
                case ImportProcessStep.Done:
                    iv.Visibility = Visibility.Collapsed;
                    bev.Visibility = Visibility.Collapsed;
                    vv.Visibility = Visibility.Collapsed;
                    break;
            }
        }

    }
}