﻿#pragma checksum "..\..\AdminTicketsView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "2ED1F3A838BAED3A8F76EC2EBCC85C40AA61D30AABE66741EBE5B97D2FC64409"
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
    /// AdminTicketsView
    /// </summary>
    public partial class AdminTicketsView : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbFiltrarBusqueda;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBuscar;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSearchBar;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnActualizar;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid tableTickets;
        
        #line default
        #line hidden
        
        
        #line 80 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAgregarResponsable;
        
        #line default
        #line hidden
        
        
        #line 82 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSolucionado;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\AdminTicketsView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEliminar;
        
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
            System.Uri resourceLocater = new System.Uri("/Tickets-Bosquejos;component/adminticketsview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\AdminTicketsView.xaml"
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
            
            #line 10 "..\..\AdminTicketsView.xaml"
            ((Tickets_Bosquejos.AdminTicketsView)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.cmbFiltrarBusqueda = ((System.Windows.Controls.ComboBox)(target));
            
            #line 24 "..\..\AdminTicketsView.xaml"
            this.cmbFiltrarBusqueda.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cmbFiltrarBusqueda_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 24 "..\..\AdminTicketsView.xaml"
            this.cmbFiltrarBusqueda.Loaded += new System.Windows.RoutedEventHandler(this.cmbFiltrarBusqueda_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnBuscar = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\AdminTicketsView.xaml"
            this.btnBuscar.Click += new System.Windows.RoutedEventHandler(this.btnBuscar_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.txtSearchBar = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.btnActualizar = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\AdminTicketsView.xaml"
            this.btnActualizar.Click += new System.Windows.RoutedEventHandler(this.btnActualizar_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.tableTickets = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 7:
            this.btnAgregarResponsable = ((System.Windows.Controls.Button)(target));
            
            #line 81 "..\..\AdminTicketsView.xaml"
            this.btnAgregarResponsable.Click += new System.Windows.RoutedEventHandler(this.btnAgregarResponsable_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnSolucionado = ((System.Windows.Controls.Button)(target));
            
            #line 83 "..\..\AdminTicketsView.xaml"
            this.btnSolucionado.Click += new System.Windows.RoutedEventHandler(this.btnSolucionado_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnEliminar = ((System.Windows.Controls.Button)(target));
            
            #line 85 "..\..\AdminTicketsView.xaml"
            this.btnEliminar.Click += new System.Windows.RoutedEventHandler(this.btnEliminar_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

