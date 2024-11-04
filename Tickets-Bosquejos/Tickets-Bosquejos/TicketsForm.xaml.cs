using Microsoft.Win32;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using MySqlX.XDevAPI.Relational;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Packaging;
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
    /// Lógica de interacción para TicketsForm.xaml
    /// </summary>
    public partial class TicketsForm : Page
    {
        private byte[] data;

        public TicketsForm()
        {
            InitializeComponent();

            CargarCmbSistemas();
        }

      


        //Poner placeholder
        private void cmbPrioridad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPrioridad.SelectedItem != null && cmbPrioridad.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled)
                {

                    cmbPrioridad.Text = "Selecciona la prioridad";
                }
                else
                {
                    cmbPrioridad.Text = selectedItem.Content.ToString();
                }
            }
        }

        private void cmbPrioridad_Loaded(object sender, RoutedEventArgs e)
        {
            cmbPrioridad.SelectedIndex = 0;
        }

        //Cambiar titulo de la ventana por el de la pagina
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Title = "Crear Ticket";
        }

        //Placeholder para el combobox para seleccionar un sistema en el formulario 
        private void cmbSistema_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbSistema.SelectedItem != null && cmbSistema.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled)
                {

                    cmbSistema.Text = "Seleccione un sistema";
                }
                else
                {
                    cmbSistema.Text = selectedItem.Content.ToString();
                }
            }
        }

        private void cmbSistema_Loaded(object sender, RoutedEventArgs e)
        {
            cmbSistema.SelectedIndex = 0;
        }

        //Cargar combobox con los sistemas de la base de datos
        private void CargarCmbSistemas()
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT sis_clave, sis_nombre FROM sgtsistemas ORDER BY sis_nombre ASC";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        cmbSistema.Items.Add(row["sis_nombre"]);

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar sistemas" + ex.Message);
                }
            }
        }

        //Subir pdf 
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

       

        //Enviar ticket
        private void btnEnviar_Click(object sender, RoutedEventArgs e)
        {
            string incidencia = txtIncidencia.Text;
            string sistema = cmbSistema.SelectedItem?.ToString();
            string prioridad = (cmbPrioridad.SelectedItem as ComboBoxItem).Content.ToString();
            string observaciones = txtObservaciones.Text;
            string correo = txtCorreo.Text;

            if (string.IsNullOrEmpty(incidencia) || string.IsNullOrEmpty(sistema) || string.IsNullOrEmpty(prioridad) || string.IsNullOrEmpty(observaciones)
                    || string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Porfavor complete todos los campos obligatorios");
                return;
            }

            GuardarTicket(incidencia, sistema, prioridad, observaciones, correo);
        }
        private void GuardarTicket(string incidencia, string sistema, string prioridad, string observaciones, string correo)
        {
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand cmd = new MySqlCommand(
                    @"INSERT INTO tickets_prac
                        (tic_nombre, sis_nombre, tic_status, tic_prioridad, tic_observaciones, tic_pdf, tic_correo, tic_fechacreacion)
                        VALUES
                        (@ticNombre, @sisNombre, @ticStatus, @ticPrioridad, @ticObservaciones, @ticPdf, @ticCorreo,
                        @ticFechaCreacion)", connection);


                    cmd.Parameters.AddWithValue("@ticNombre", incidencia);
                    cmd.Parameters.AddWithValue("@sisNombre", sistema);
                    cmd.Parameters.AddWithValue("@ticStatus", "Nuevo");
                    cmd.Parameters.AddWithValue("@ticPrioridad", prioridad);
                    cmd.Parameters.AddWithValue("@ticObservaciones", observaciones);
                    cmd.Parameters.AddWithValue("@ticPdf", data ?? new byte[0]);
                    cmd.Parameters.AddWithValue("@ticCorreo", correo);
                    cmd.Parameters.AddWithValue("@ticFechaCreacion", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Ticket enviado exitosamente.");
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ha ocurrido un error:" + ex.Message);
            }
        }
    }
}
