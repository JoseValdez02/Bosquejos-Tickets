using Microsoft.Toolkit.Uwp.Notifications;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
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
using Tickets_Bosquejos.Classes;

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

            CargarTickets();
        }

        //Botón para editar un ticket (Enviar a la pagina para editarlo)
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if(tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {
                string status = ticketSeleccionado["tic_status"].ToString();

                if (status == "Resuelto")
                {
                    MessageBox.Show("Este ticket ya se encuentra resuelto");
                    return;
                }

                EditTicketForm editTicket = new EditTicketForm(ticketSeleccionado);
                this.NavigationService.Navigate(editTicket);
            }
            else
            {
                MessageBox.Show("Por favor seleccione un ticket");
            }
        }

        //Botón para eliminar un ticket
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {

                int ticClave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Desea eliminar este ticket, podría estar en proceso?", "Eliminar ticket", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if( result == MessageBoxResult.Yes)
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
        private void EliminarTicket(int ticClave) {

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
                    NotificarEliminacion(ticClave, UserSession.usuNombre);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el ticket" + ex.Message);
                }
            }
        }

        //Notificar eliminación
        private void NotificarEliminacion(int clave, string nombre)
        {

            // Generar la notificación de Windows
            new ToastContentBuilder()
                .AddArgument("action", "viewTicket")
                .AddArgument("ticketId", clave)
                .AddText($"Ticket eliminado")
                .AddText($"El ticket #{clave} fue eliminado por el usuario {nombre}.\n" +
                $"Ya no aparecerá en la tabla de tickets")
                .Show(); // Muestra la notificación
        }

        //Placeholder para el combobox de filtrar por status y cargar el filtro de busqueda por status
        private void cmbFiltrarStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            

            if (cmbFiltrarStatus.SelectedItem != null && cmbFiltrarStatus.SelectedItem is ComboBoxItem selectedItem)
            {
                string statusFiltro = selectedItem.Content.ToString();

                if (statusFiltro == "Filtrar por Status" )
                {

                    CargarTickets();
                }
                else
                {
                    FiltrarPorStatus(statusFiltro);
                }
               
            }
            
        }

        private void cmbFiltrarStatus_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFiltrarStatus.SelectedIndex = 0;
        }

        //Cargar titulo de la pagina en la ventana
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Window.GetWindow(this).Title = "Mis Tickets";
        }

        //Cargar tabla de los tickets en el datagrid
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

        //Recargar el datagrid cuando se edite o elimine un ticket
        public void RecargarTickets()
        {
            CargarTickets();
        }

        //Botón para buscar
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarTickets(txtSearchBar.Text);
        }

        //Metodo para buscar tickets desde la barra de busqueda
        private void BuscarTickets(string criterio)
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_usuario", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("p_puesto", UserSession.usuPuesto);

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

                    if(dt.Rows.Count > 0)
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

        //Actualizar datagrid
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_usuario", UserSession.usuNombre);
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

        //Filtrar busqueda por status
        private void FiltrarPorStatus(string statusFiltro)
        {
  
            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("filtrarbusqueda", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_statusFiltro", statusFiltro);
                    cmd.Parameters.AddWithValue("p_prioridadFiltro", DBNull.Value);
                    cmd.Parameters.AddWithValue("p_usuario", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("p_puesto", UserSession.usuPuesto);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableTickets.ItemsSource = dt.DefaultView;


                    if (dt.Rows.Count > 0)
                    {
                        tableTickets.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron tickets en el filtro seleccionado");
                        tableTickets.ItemsSource = null;
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al filtrar tickets: " + ex.Message);

                }
            }
        }
    }
}