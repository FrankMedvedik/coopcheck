﻿#pragma checksum "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "521E415EC9AB73996F5E3CCA8D1CB061"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace CoopCheck.WPF.Content.Voucher.Import {
    
    
    /// <summary>
    /// ImportWorksheetView
    /// </summary>
    public partial class ImportWorksheetView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOpen;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtFilePath;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cbSheets;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridErrors;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dtColumns;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox dtFoundColumns;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridVouchers;
        
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
            System.Uri resourceLocater = new System.Uri("/CoopCheck.WPF;component/content/voucher/import/importworksheetview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.btnOpen = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
            this.btnOpen.Click += new System.Windows.RoutedEventHandler(this.btnOpen_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtFilePath = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cbSheets = ((System.Windows.Controls.ComboBox)(target));
            
            #line 29 "..\..\..\..\..\Content\Voucher\Import\ImportWorksheetView.xaml"
            this.cbSheets.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cbSheets_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.GridErrors = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.dtColumns = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 6:
            this.dtFoundColumns = ((System.Windows.Controls.ListBox)(target));
            return;
            case 7:
            this.GridVouchers = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

