﻿#pragma checksum "..\..\Edit.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "92B5EFABD0D3194F0397D273460CAD1C43CF24B45D804622637B3ACC8D8BBFF0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Test;


namespace Test {
    
    
    /// <summary>
    /// Edit
    /// </summary>
    public partial class Edit : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ConfigureTool;
        
        #line default
        #line hidden
        
        
        #line 65 "..\..\Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem ToolZOffset;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Fixtures;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Reconfigure;
        
        #line default
        #line hidden
        
        
        #line 83 "..\..\Edit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem Utilities;
        
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
            System.Uri resourceLocater = new System.Uri("/Test;component/edit.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Edit.xaml"
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
            
            #line 10 "..\..\Edit.xaml"
            ((Test.Edit)(target)).Initialized += new System.EventHandler(this.Window_Initialized);
            
            #line default
            #line hidden
            
            #line 10 "..\..\Edit.xaml"
            ((Test.Edit)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Edit_loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 27 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnNewFile);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 28 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnNewFile);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 30 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnFileSave);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 42 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.NewPatternTeach_click);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 59 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.btnLive);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 62 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ProductionConguration);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 63 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfigureProductMap_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ConfigureTool = ((System.Windows.Controls.MenuItem)(target));
            
            #line 64 "..\..\Edit.xaml"
            this.ConfigureTool.Click += new System.Windows.RoutedEventHandler(this.ToolConfigure_click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ToolZOffset = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 11:
            this.Fixtures = ((System.Windows.Controls.MenuItem)(target));
            
            #line 66 "..\..\Edit.xaml"
            this.Fixtures.Click += new System.Windows.RoutedEventHandler(this.FixtureConfigure_click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Reconfigure = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 13:
            
            #line 69 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.logConfigure_click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 70 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.MaintenanceConfiguration);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 71 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Custom_click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 73 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.Rabbit_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 74 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.NeederFinder_click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 75 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.FanWidth_click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 76 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ConveyorSeetings_click);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 77 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfigureSpeed_click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 78 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfigureSpeed_click);
            
            #line default
            #line hidden
            return;
            case 22:
            
            #line 79 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfigureSpeed_click);
            
            #line default
            #line hidden
            return;
            case 23:
            
            #line 80 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.ConfigureSpeed_click);
            
            #line default
            #line hidden
            return;
            case 24:
            
            #line 81 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PasswordManager_Click);
            
            #line default
            #line hidden
            return;
            case 25:
            this.Utilities = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 26:
            
            #line 94 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PressControl);
            
            #line default
            #line hidden
            return;
            case 27:
            
            #line 95 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PressControl);
            
            #line default
            #line hidden
            return;
            case 28:
            
            #line 96 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PressControl);
            
            #line default
            #line hidden
            return;
            case 29:
            
            #line 97 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PressControl);
            
            #line default
            #line hidden
            return;
            case 30:
            
            #line 98 "..\..\Edit.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.PressControl);
            
            #line default
            #line hidden
            return;
            case 31:
            
            #line 176 "..\..\Edit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.btnNewFile);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

