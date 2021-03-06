﻿using FirstFloor.ModernUI.Windows.Controls;

namespace CoopCheck.WPF.Content.Payment.PaymentFinder
{
    /// <summary>
    ///     Interaction logic for ClearPaymentDialog.xaml
    /// </summary>
    public partial class ClearPaymentDialog : ModernDialog
    {
        public ClearPaymentDialog()
        {
            InitializeComponent();

            // define the dialog buttons
            Buttons = new[] {OkButton, CancelButton};
        }
    }
}