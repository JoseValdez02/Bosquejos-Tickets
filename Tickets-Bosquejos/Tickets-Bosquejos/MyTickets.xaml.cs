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

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
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

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarticketsusers", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

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

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("actualizartabla", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
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
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("filtrarstatus", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_statusFiltro", statusFiltro);
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