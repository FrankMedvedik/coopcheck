using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using muiCoopCheck.Models;

namespace muiCoopCheck.Pages.Vouchers
{
    /// <summary>
    /// Interaction logic for VouchersPage.xaml
    /// </summary>
    public partial class VouchersPage : UserControl
    {
        public VouchersPage()
        {
            InitializeComponent();
            SetVisibilityState();

        }

        private ProcessStep _currentState = ProcessStep.Import;
        private ProcessStep _nextState = ProcessStep.Import;

        enum ProcessStep        
	{
            Import, BatchEdit, Validate, Done
         
	}
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            SetVisibilityState();

        }

        private void SetVisibilityState()
        {
            switch (_currentState)
            {
                case ProcessStep.Import:
                    bev.Visibility = Visibility.Collapsed;

                    iv.Visibility = Visibility.Visible;
                    vv.Visibility = Visibility.Collapsed;
                    _nextState = ProcessStep.BatchEdit;
                    btnDOIT.Content = "Edit Batch Details";
                    break;
                case ProcessStep.BatchEdit:
                    bev.Visibility = Visibility.Visible;
                    iv.Visibility = Visibility.Collapsed;
                    vv.Visibility = Visibility.Collapsed;
                    _nextState = ProcessStep.Validate;
                    btnDOIT.Content = "Review Addresses";
                    bev.Vouchers = iv.Vouchers;
                    break;
                case ProcessStep.Validate:
                    vv.Visibility = Visibility.Visible;
                    bev.Visibility = Visibility.Collapsed;
                    iv.Visibility = Visibility.Collapsed;
                    vv.Vouchers = bev.Vouchers;
                    btnDOIT.Content = "Close Batch";
                    _nextState = ProcessStep.Done;
                    break;
                case ProcessStep.Done:
                    iv.Visibility = Visibility.Collapsed;
                    bev.Visibility = Visibility.Collapsed;
                    vv.Visibility = Visibility.Collapsed;
                    btnDOIT.Content = "BYE!";
                    break;

                default:
                    break;
            }
            _currentState = _nextState;
        }

    }
}
