using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Lógica de interacción para AdminTicketEdit.xaml
    /// </summary>
    public partial class AdminTicketEdit : Page
    {

        private AdminTicketsView myAdminTicketsView;
        private byte[] data;
        private int tic_clave;

        public AdminTicketEdit(DataRowView ticketSeleccionado)
        {
            InitializeComponent();
            CargarCmbResponsable();

           


            this.tic_clave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

            txtIncidencia.Text = ticketSeleccionado["tic_nombre"].ToString();
            txtSistema.Text = ticketSeleccionado["sis_nombre"].ToString();
            txtPrioridad.Text = ticketSeleccionado["tic_prioridad"].ToString();
            txtObservaciones.Text = ticketSeleccionado["tic_observaciones"].ToString();
            txtPdf.Text = ticketSeleccionado["tic_pdf"].ToString();
            txtCorreo.Text = ticketSeleccionado["tic_correo"].ToString();
            txtFechaCreacion.Text = ticketSeleccionado["tic_fechacreacion"].ToString();
            
        }

        //Cargar informacion de los tickets en los textblock obteniendo la clave
        private void CargarTickets(int tic_clave)
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    string query = "SELECT tic_nombre, sis_nombre, tic_prioridad, tic_observaciones, tic_pdf, tic_correo, tic_fechacreacion" +
                        " FROM tickets_prac WHERE tic_clave = @clave";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@clave", tic_clave);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {

                        txtIncidencia.Text = reader["tic_nombre"].ToString();
                        txtSistema.Text = reader["sis_nombre"].ToString();
                        txtPrioridad.Text = reader["tic_prioridad"].ToString();
                        txtObservaciones.Text = reader["tic_observaciones"].ToString();
                        txtPdf.Text = reader["tic_pdf"].ToString();
                        txtCorreo.Text = reader["tic_correo"].ToString();
                        txtFechaCreacion.Text = reader["tic_fechacreacion"].ToString();
                    }

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar el ticket:" + ex.Message);
                }
            }
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminTicketsView());

            AdminTicketsView ticketsAdmin = new AdminTicketsView();

            NavigationService.Navigate(ticketsAdmin);
        }

        private void cmbResponsable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbResponsable.SelectedItem != null && cmbResponsable.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled)
                {

                    cmbResponsable.Text = "Selecciona a un responsable";
                }
                else
                {
                    cmbResponsable.Text = selectedItem.Content.ToString();
                }
            }
        }

        private void cmbResponsable_Loaded(object sender, RoutedEventArgs e)
        {
            cmbResponsable.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Title = "Asignar Responsable";
        }

        private void CargarCmbResponsable()
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT pro_clave, pro_nombre FROM sgtprogramadores ORDER BY pro_nombre ASC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        cmbResponsable.Items.Add(row["pro_nombre"]);

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar sistemas" + ex.Message);
                }
            }
        }

        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {

            string responsableSeleccionado = cmbResponsable.SelectedItem?.ToString();
            DateTime? fechaResolucion = txtFechaResolucion.SelectedDate;


            if (string.IsNullOrEmpty(responsableSeleccionado))
            {
                MessageBox.Show("Porfavor complete todos los campos obligatorios");
                return;
            }

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    string query = "UPDATE tickets_prac SET tic_status = @status, pro_nombre = @programador, tic_fechafin = @fechafin WHERE tic_clave = @clave";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@status", "Abierto");
                    cmd.Parameters.AddWithValue("@programador", responsableSeleccionado);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaResolucion);
                    cmd.Parameters.AddWithValue("@clave", tic_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se actualizo el ticket exitosamente");
                    AdminTicketsView myAdminTicketsView = new AdminTicketsView();
                    NavigationService.Navigate(myAdminTicketsView);
                    myAdminTicketsView.RecargarTickets();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar los tickets: " + ex.Message);

                }
            }
        }
    }
}
