using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tickets_Bosquejos.UserClass;

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para AdminTicketEdit.xaml
    /// </summary>
    public partial class AdminTicketEdit : Page
    {


        private byte[] data;
        private int tic_clave;

        public AdminTicketEdit(DataRowView ticketSeleccionado)
        {
            InitializeComponent();
            CargarCmbResponsable();


            //Cargar la información del ticket seleccionado en los textbox
            this.tic_clave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

            txtIncidencia.Text = ticketSeleccionado["tic_nombre"].ToString();
            txtUsuario.Text = ticketSeleccionado["tic_usuario"].ToString();
            txtSistema.Text = ticketSeleccionado["sis_nombre"].ToString();
            txtEmpresa.Text = ticketSeleccionado["emp_nombre"].ToString();
            txtPrioridad.Text = ticketSeleccionado["tic_prioridad"].ToString();
            txtObservaciones.Text = ticketSeleccionado["tic_observaciones"].ToString();
            txtPdf.Text = ticketSeleccionado["tic_pdf"].ToString();
            txtCorreo.Text = ticketSeleccionado["tic_correo"].ToString();
            txtFechaCreacion.Text = ticketSeleccionado["tic_fechacreacion"].ToString();
            //Cargar estos datos en caso de que ya existan registros en la tabla de tickets
            
            if (ticketSeleccionado["pro_nombre"] != DBNull.Value)
            {
                txtResponsable.Text = ticketSeleccionado["pro_nombre"].ToString();
            }
            if (ticketSeleccionado["tic_fechafin"] != DBNull.Value) {
                txtFechaResolucion.SelectedDate = Convert.ToDateTime(ticketSeleccionado["tic_fechafin"]);
            }

        }



        //Volver
        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AdminTicketsView());

            AdminTicketsView ticketsAdmin = new AdminTicketsView();

            NavigationService.Navigate(ticketsAdmin);
        }

        //Cargar placeholder
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

        //Cargar título
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Title = "Asignar Responsable";
        }

        //Cargar tabla de programadores en el combobox
        private void CargarCmbResponsable()
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcmbprogramadores", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
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

                    MessageBox.Show("Error al cargar lista" + ex.Message);
                }
            }
        }

        //Asignar responsable y fecha
        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {

            string responsableSeleccionado = cmbResponsable.SelectedItem?.ToString();
            DateTime? fechaResolucion = txtFechaResolucion.SelectedDate;


            if (string.IsNullOrEmpty(responsableSeleccionado))
            {
                MessageBox.Show("Por favor asigne a un programador este ticket");
                return;
            }

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("asignarresponsable", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_status", "Abierto");
                    cmd.Parameters.AddWithValue("v_programador", responsableSeleccionado);
                    cmd.Parameters.AddWithValue("v_fechafin", fechaResolucion);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuIdentificacion);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", tic_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se actualizó el ticket exitosamente");
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

        private void btnDescargarPdf_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Obtener pdf por la clave del ticket
                    MySqlCommand cmd = new MySqlCommand("descargarpdf", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_clave", tic_clave);

                    byte[] pdfData = (byte[])cmd.ExecuteScalar();

                    if (pdfData != null)
                    {
                        SaveFileDialog dialog = new SaveFileDialog();

                        dialog.Filter = "PDF Files (*.pdf)|*.pdf";
                        dialog.Title = "Guardar archivo PDF";
                        dialog.FileName = "ticket.pdf";

                        if (dialog.ShowDialog() == true)
                        {

                            string filePath = dialog.FileName;

                            //Guardar archivo en la ruta a seleccionar
                            File.WriteAllBytes(filePath, pdfData);


                            MessageBox.Show("Se guardó el archivo PDF correctamente");

                            // Abrir el archivo descargado automáticamente
                            System.Diagnostics.Process.Start(filePath);
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún archivo PDF adjunto para este ticket.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descargar el archivo PDF: " + ex.Message);
                }
            }
        }
    }
}
