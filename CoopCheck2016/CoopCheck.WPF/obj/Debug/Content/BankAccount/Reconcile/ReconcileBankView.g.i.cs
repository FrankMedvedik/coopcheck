﻿#pragma checksum "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7F45F01A252FF3464E3A980EBDFC09A4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CoopCheck.WPF.Content.BankAccount.Reconcile;
using CoopCheck.WPF.Content.Payment.Criteria;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Converters;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace CoopCheck.WPF.Content.BankAccount.Reconcile {
    
    
    /// <summary>
    /// ReconcileBankView
    /// </summary>
    public partial class ReconcileBankView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CoopCheck.WPF.Content.Payment.Criteria.PaymentReportCriteriaView prcv;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Payments;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Expander Recon;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DgStats;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal CoopCheck.WPF.Content.BankAccount.Reconcile.BankTransactionsView bctv;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/CoopCheck.WPF;component/content/bankaccount/reconcile/reconcilebankview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.prcv = ((CoopCheck.WPF.Content.Payment.Criteria.PaymentReportCriteriaView)(target));
            return;
            case 2:
            this.Payments = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
            this.Payments.Click += new System.Windows.RoutedEventHandler(this.Refresh_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Recon = ((System.Windows.Controls.Expander)(target));
            return;
            case 4:
            this.DgStats = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 5:
            
            #line 55 "..\..\..\..\..\Content\BankAccount\Reconcile\ReconcileBankView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.bctv = ((CoopCheck.WPF.Content.BankAccount.Reconcile.BankTransactionsView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

