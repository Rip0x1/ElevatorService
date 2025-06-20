﻿#pragma checksum "..\..\..\PartsPage.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "BEE0EE4121CCBFEC2B04686BD2028A85C2DE07EB"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ElevatorServiceApp;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace ElevatorServiceApp {
    
    
    /// <summary>
    /// PartsPage
    /// </summary>
    public partial class PartsPage : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 51 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchTextBox;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MinQuantityTextBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MinPriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MaxPriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PartsGrid;
        
        #line default
        #line hidden
        
        
        #line 98 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.DialogHost PartDialog;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock DialogTitle;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PartNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 103 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PartNumberTextBox;
        
        #line default
        #line hidden
        
        
        #line 104 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox QuantityInStockTextBox;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\PartsPage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox PriceTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.5.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ElevatorServiceApp;component/partspage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\PartsPage.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 16 "..\..\..\PartsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.AddPart_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 26 "..\..\..\PartsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Refresh_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 36 "..\..\..\PartsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ExportToExcel_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.SearchTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 51 "..\..\..\PartsPage.xaml"
            this.SearchTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.MinQuantityTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 52 "..\..\..\PartsPage.xaml"
            this.MinQuantityTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MinQuantityTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.MinPriceTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 53 "..\..\..\PartsPage.xaml"
            this.MinPriceTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MinPriceTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.MaxPriceTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 54 "..\..\..\PartsPage.xaml"
            this.MaxPriceTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MaxPriceTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.PartsGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 11:
            this.PartDialog = ((MaterialDesignThemes.Wpf.DialogHost)(target));
            return;
            case 12:
            this.DialogTitle = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.PartNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 14:
            this.PartNumberTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 15:
            this.QuantityInStockTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.PriceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 17:
            
            #line 107 "..\..\..\PartsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SavePart_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.5.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 9:
            
            #line 70 "..\..\..\PartsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.EditPart_Click);
            
            #line default
            #line hidden
            break;
            case 10:
            
            #line 80 "..\..\..\PartsPage.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.DeletePart_Click);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}

