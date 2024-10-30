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
    /// Lógica de interacción para MyTickets.xaml
    /// </summary>
    public partial class MyTickets : Page
    {
        public MyTickets()
        {
            InitializeComponent();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           
          this.NavigationService.Navigate(new EditTicketForm());

            EditTicketForm editTicket = new EditTicketForm();

            NavigationService.Navigate(editTicket);
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("¿Estas seguro de eliminar este ticket", "Tu ticket podria estar en proceso?", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }

        private void cmbFiltrarStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltrarStatus.SelectedItem != null && cmbFiltrarStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled)
                {

                    cmbFiltrarStatus.Text = "Filtrar por Status";
                }
                else
                {
                    cmbFiltrarStatus.Text = selectedItem.Content.ToString();
                }
            }
        }

        private void cmbFiltrarStatus_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFiltrarStatus.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Window.GetWindow(this).Title = "Mis Tickets";
        }
    }
}
