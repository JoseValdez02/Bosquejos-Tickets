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

        //Boton para editar un ticket (Enviar a la pagina para editarlo)
        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if(tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {

                EditTicketForm editTicket = new EditTicketForm(ticketSeleccionado);
                this.NavigationService.Navigate(editTicket);
            }
            else
            {
                MessageBox.Show("Porfavor seleccione un ticket");
            }
        }

        //Boton para eliminar un ticket
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if(tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {

                int ticClave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Estas seguro de eliminar este ticket", "Tu ticket podria estar en proceso?", MessageBoxButton.YesNo, MessageBoxImage.Warning);

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

                    string query = "DELETE FROM tickets_prac WHERE tic_clave = @clave";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@clave", ticClave);
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

        //Cargar tickets en el datagrid
        private void CargarTickets()
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    string query = "SELECT tic_clave, tic_nombre, sis_nombre, tic_status, tic_prioridad, tic_observaciones, tic_pdf, tic_correo, pro_nombre, tic_fechacreacion, tic_fechafin" +
                        " FROM tickets_prac ORDER BY tic_clave DESC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
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

        //Recargar el datagrid cuando se edite un ticket
        public void RecargarTickets()
        {
            CargarTickets();
        }

        //Barra de busqueda
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            BuscarTickets(txtSearchBar.Text);
        }

        private void BuscarTickets(string criterio)
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    string query = "SELECT tic_clave, tic_nombre, sis_nombre, tic_status, tic_prioridad, tic_observaciones, tic_pdf, tic_correo, pro_nombre, tic_fechacreacion, tic_fechafin" +
                        " FROM tickets_prac WHERE tic_clave = @criterio OR tic_nombre LIKE @criterioNombre ORDER BY tic_clave DESC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@criterio", criterio);
                    cmd.Parameters.AddWithValue("@criterioNombre", "%" + criterio + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableTickets.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No existe este producto: " + ex.Message);

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

                    string query = "SELECT tic_clave, tic_nombre, sis_nombre, tic_status, tic_prioridad, tic_observaciones, tic_pdf, tic_correo, pro_nombre, tic_fechacreacion, tic_fechafin" +
                        " FROM tickets_prac ORDER BY tic_clave DESC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
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

                    string query = "SELECT tic_clave, tic_nombre, sis_nombre, tic_status, tic_prioridad, tic_observaciones, tic_pdf, tic_correo, pro_nombre, tic_fechacreacion, tic_fechafin" +
                        " FROM tickets_prac WHERE tic_status = @statusFiltro ORDER BY tic_clave";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@statusFiltro", statusFiltro);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableTickets.ItemsSource = dt.DefaultView;
                }
                catch (Exception ex)
                {

                    MessageBox.Show("No hay tickets en ese status: " + ex.Message);

                }
            }
        }
    }
}