using System;
using System.Collections.Generic;
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

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para AdminTicketsView.xaml
    /// </summary>
    public partial class AdminTicketsView : Page
    {
        public AdminTicketsView()
        {
            InitializeComponent();
        }

        private void cmbFiltrarBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltrarBusqueda.SelectedItem != null && cmbFiltrarBusqueda.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled) {

                    cmbFiltrarBusqueda.Text = "Filtrar tickets";
                }
                else
                {
                    cmbFiltrarBusqueda.Text = selectedItem.Content.ToString();
                }
            }
        }

        private void cmbFiltrarBusqueda_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFiltrarBusqueda.SelectedIndex = 0;
        }

        private void btnAgregarResponsable_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminTicketEdit());

            AdminTicketEdit editTicket = new AdminTicketEdit();

            NavigationService.Navigate(editTicket);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Window.GetWindow(this).Title = "Administrar Tickets";
        }
    }
}
