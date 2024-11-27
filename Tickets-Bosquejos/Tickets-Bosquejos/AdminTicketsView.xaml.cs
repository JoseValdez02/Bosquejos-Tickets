using Microsoft.Toolkit.Uwp.Notifications;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
using Tickets_Bosquejos.Classes;

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

            CargarTickets();
        }

        //Placeholder
        private void cmbFiltrarBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltrarBusqueda.SelectedItem != null && cmbFiltrarBusqueda.SelectedItem is ComboBoxItem selectedItem)
            {
                string filtrar = selectedItem.Content.ToString();

                if (filtrar == "Filtrar tickets")
                {

                    CargarTickets();
                }
                else
                {
                    FiltrarBusqueda(filtrar);
                }
            }
        }

        private void cmbFiltrarBusqueda_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFiltrarBusqueda.SelectedIndex = 0;
        }


        //Cargar titulo de la pagina
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Window.GetWindow(this).Title = "Administrar Tickets";
        }

        //Cargar tickets en el datagrid
        private void CargarTickets()
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_usuClave", UserSession.usuClave);
                    cmd.Parameters.AddWithValue("p_puesto", UserSession.usuPuesto);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableTickets.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar los tickets: " + ex.Message);

                }
            }
        }

        //Recargar el datagrid cuando se haga un cambio en un registro
        public void RecargarTickets()
        {
            CargarTickets();
        }


        //Filtrar busqueda por status
        private void FiltrarBusqueda(string filtrar)
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("filtrarbusqueda", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_statusFiltro", filtrar);
                    cmd.Parameters.AddWithValue("p_prioridadFiltro", filtrar);
                    cmd.Parameters.AddWithValue("p_puesto", UserSession.usuPuesto);
                    cmd.Parameters.AddWithValue("p_usuClave", UserSession.usuClave);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if(dt.Rows.Count > 0)
                    {
                        tableTickets.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros en el filtro seleccionado");
                        tableTickets.ItemsSource = null;
                    }
                   
                }

                catch (Exception ex)
                {

                    MessageBox.Show("Error al filtrar tickets: " + ex.Message);

                }
            }
        }

        //Barra de busqueda
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarTickets(txtSearchBar.Text);
        }

        private void BuscarTickets(string criterio)
        {


            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_puesto", UserSession.usuPuesto);
                    cmd.Parameters.AddWithValue("p_usuClave", UserSession.usuClave);

                    if (int.TryParse(criterio, out int criterioInt))
                    {
                        cmd.Parameters.AddWithValue("p_criterio", criterioInt);
                        cmd.Parameters.AddWithValue("p_criterioNombre", " ");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("p_criterio", -1);
                        cmd.Parameters.AddWithValue("p_criterioNombre", criterio);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        tableTickets.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron tickets con el criterio proporcionado");
                    }


                }
                catch (Exception ex)
                {

                    MessageBox.Show("No existe este ticket: " + ex.Message);

                }
            }
        }

        //Botón de actualizar
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
  
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();


                    MySqlCommand cmd = new MySqlCommand("cargartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_puesto", UserSession.usuPuesto);
                    cmd.Parameters.AddWithValue("p_usuClave", UserSession.usuClave);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableTickets.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar los tickets: " + ex.Message);

                }
            }
        }

        //Ir a la vista para agregar un responsable al ticket
        private void btnAgregarResponsable_Click(object sender, RoutedEventArgs e)
        {
            if (tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {
                string status = ticketSeleccionado["tic_status"].ToString();

                if (status == "Resuelto")
                {
                    MessageBox.Show("Este ticket ya se encuentra resuelto");
                    return;
                }

                AdminTicketEdit adminEditTicket = new AdminTicketEdit(ticketSeleccionado);
                this.NavigationService.Navigate(adminEditTicket);
            }
            else
            {
                MessageBox.Show("Por favor seleccione un ticket");
            }
        }

        private void btnResuelto_Click(object sender, RoutedEventArgs e)
        {
            if (tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {
                string status = ticketSeleccionado["tic_status"].ToString();

                if(status == "Resuelto")
                {
                    MessageBox.Show("Este ticket ya se encuentra resuelto");
                    return;
                }

                int ticClave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

                MessageBoxResult result = MessageBox.Show("Este ticket se marcará como resuelto \n ¿Está seguro?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    ResueltoStatus(ticClave);
                    RecargarTickets();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un ticket");
            }
        }

        //Cambiar estado a resuelto
        private void ResueltoStatus(int ticClave)
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("statusresuelto", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_status", "Resuelto");
                    cmd.Parameters.AddWithValue("v_fechafin", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", ticClave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Ticket resuelto");
                    NotificarResuelto(ticClave, DateTime.Now);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el ticket" + ex.Message);
                }
            }
        }

        private void NotificarResuelto(int clave, DateTime fecha)
        {
            // Generar la notificación de Windows
            new ToastContentBuilder()
                .AddArgument("action", "viewTicket")
                .AddArgument("ticketId", clave)
                .AddText($"Ticket resuelto")
                .AddText($"El ticket #{clave} ha sido resuelto.\n" +
                $"Fecha de resolución: {fecha}")
                .Show(); // Muestra la notificación
        }

        //Eliminar ticket
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {

                int ticClave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este ticket?\n No se podrá recuperar", "Eliminar ticket", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    EliminarTicket(ticClave);
                    RecargarTickets();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un ticket");
            }
        }

        //Metodo para eliminar el ticket
        private void EliminarTicket(int ticClave)
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("eliminarticket", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_clave", ticClave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("El ticket ha sido eliminado exitosamente");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el ticket" + ex.Message);
                }
            }
        }
    }
}
