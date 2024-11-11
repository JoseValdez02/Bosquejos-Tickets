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

       
        private void btnEnviarEdit_Click(object sender, RoutedEventArgs e)
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editarticket", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_observaciones", txtObservaciones.Text);
                    cmd.Parameters.AddWithValue("v_pdf", data ?? new byte[0]);
                    cmd.Parameters.AddWithValue("v_clave", tic_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se actualizó el ticket exitosamente");
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
    

