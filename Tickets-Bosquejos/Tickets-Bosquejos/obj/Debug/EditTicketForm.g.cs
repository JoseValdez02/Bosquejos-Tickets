﻿#pragma checksum "..\..\EditTicketForm.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "32ADEA31B2D0F5A31D2398C73B9CBC020F308EBFA634C27458E052958D795CDF"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
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
using Tickets_Bosquejos;


namespace Tickets_Bosquejos {
    
    
    /// <summary>
    /// EditTicketForm
    /// </summary>
    public partial class EditTicketForm : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnVolver;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtUsuario;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbSistema;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbPrioridad;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtObservaciones;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtCorreo;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\EditTicketForm.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEnviarEdit;
        
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
            System.Uri resourceLocater = new System.Uri("/Tickets-Bosquejos;component/editticketform.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\EditTicketForm.xaml"
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
            this.btnVolver = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\EditTicketForm.xaml"
            this.btnVolver.Click += new System.Windows.RoutedEventHandler(this.btnVolver_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.txtUsuario = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.cmbSistema = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 4:
            this.cmbPrioridad = ((System.Windows.Controls.ComboBox)(target));
            
            #line 51 "..\..\EditTicketForm.xaml"
            this.cmbPrioridad.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbPrioridad_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 51 "..\..\EditTicketForm.xaml"
            this.cmbPrioridad.Loaded += new System.Windows.RoutedEventHandler(this.cmbPrioridad_Loaded);
            
            #line default
            #line hidden
            return;
            case 5:
            this.txtObservaciones = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.txtCorreo = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.btnEnviarEdit = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

