using Microsoft.Win32;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.IO;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para EditTicketForm.xaml
    /// </summary>
    public partial class EditTicketForm : Page
    {

        private MyTickets myTicketsPage;
        private byte[] data;
        private int tic_clave;

        public EditTicketForm(DataRowView ticketSeleccionado)
        {
            InitializeComponent();

            //this.myTicketsPage = myTicketsPage;


            this.tic_clave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

            txtIncidencia.Text = ticketSeleccionado["tic_nombre"].ToString();
            txtSistema.Text = ticketSeleccionado["sis_nombre"].ToString();
            txtPrioridad.Text = ticketSeleccionado["tic_prioridad"].ToString();
            txtObservaciones.Text = ticketSeleccionado["tic_observaciones"].ToString();
            txtPdf.Text = ticketSeleccionado["tic_pdf"].ToString();
            txtCorreo.Text = ticketSeleccionado["tic_correo"].ToString();

        }

        //Boton de volver
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MyTickets());

            MyTickets misTickets = new MyTickets();

            NavigationService.Navigate(misTickets);
        }

       

        //Obtener titulo de la pagina
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Title = "Editar Tickets";
        }


        //Subir PDF
        private void btnSubirPdf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.DefaultExt = ".pdf";
            dialog.Filter = "PDF Files (*.pdf)|*.pdf";

            dialog.Title = "Agregue un documento PDF";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {

                string filename = dialog.FileName;
                txtPdf.Text = filename;
                data = File.ReadAllBytes(filename);
            }
        }

        private void CargarTickets(int tic_clave)
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    string query = "SELECT tic_nombre, sis_nombre, tic_prioridad, tic_observaciones, tic_pdf, tic_correo" +
                        " FROM tickets_prac WHERE tic_clave = @clave";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@clave", tic_clave);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read()) { 
                    
                        txtIncidencia.Text = reader["tic_nombre"].ToString();
                        txtSistema.Text = reader["sis_nombre"].ToString();
                        txtPrioridad.Text = reader["tic_prioridad"].ToString();
                        txtObservaciones.Text = reader["tic_observaciones"].ToString();
                        txtPdf.Text = reader["tic_pdf"].ToString();
                        txtCorreo.Text = reader["tic_correo"].ToString();
                    }

                }
                catch (Exception ex) { 
                    
                    MessageBox.Show("Error al cargar el ticket:" + ex.Message);
                }
            }
        }
        private void btnEnviarEdit_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    string query = "UPDATE tickets_prac SET tic_observaciones = @observaciones, tic_pdf = @pdf WHERE tic_clave = @clave";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@observaciones", txtObservaciones.Text);
                    cmd.Parameters.AddWithValue("@pdf", data ?? new byte[0]);
                    cmd.Parameters.AddWithValue("@clave", tic_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se actualizo el ticket exitosamente");
                    MyTickets myTicketsPage = new MyTickets();
                    NavigationService.Navigate(myTicketsPage);
                    myTicketsPage.RecargarTickets();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar los tickets: " + ex.Message);

                }
            }
        }
    }
}
    

