using Microsoft.Toolkit.Uwp.Notifications;
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
using Tickets_Bosquejos.Classes;

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

        //Cargar combobox con la tabla de los sistemas
        private void CargarCmbSistemas()
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcmbsistemas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        cmbSistema.Items.Add(new KeyValuePair<int, string>((int)row["sis_clave"], row["sis_nombre"].ToString()));

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
            int? sisClave = (cmbSistema.SelectedValue as int?);
            string sistema = cmbSistema.SelectedItem?.ToString();
            string prioridad = (cmbPrioridad.SelectedItem as ComboBoxItem).Content.ToString();
            string observaciones = txtObservaciones.Text;
            string correo = txtCorreo.Text;

            if (string.IsNullOrEmpty(incidencia) || string.IsNullOrEmpty(sistema) || string.IsNullOrEmpty(prioridad) || string.IsNullOrEmpty(observaciones)
                    || string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios");
                return;
            }

            GuardarTicket(incidencia, sisClave, sistema, prioridad, observaciones, correo);
        }

        //Metodo para guardar un ticket
        private void GuardarTicket(string incidencia, int? sisClave, string sistema, string prioridad, string observaciones, string correo)
        {
            
            try
            {
                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("enviarticket", connection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("v_empClave", UserSession.empClave);
                    cmd.Parameters.AddWithValue("v_usuClave", UserSession.usuClave);
                    cmd.Parameters.AddWithValue("v_sisClave", sisClave);
                    cmd.Parameters.AddWithValue("v_incidencia", incidencia);
                    cmd.Parameters.AddWithValue("v_ticUsuario", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_sisNombre", sistema);
                    cmd.Parameters.AddWithValue("v_empNombre", UserSession.empNombre);
                    cmd.Parameters.AddWithValue("v_status", "Nuevo");
                    cmd.Parameters.AddWithValue("v_prioridad", prioridad);
                    cmd.Parameters.AddWithValue("v_observaciones", observaciones);
                    cmd.Parameters.AddWithValue("v_pdf", data ?? new byte[0]);
                    cmd.Parameters.AddWithValue("v_correo", correo);
                    cmd.Parameters.AddWithValue("v_fechacreacion", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Ticket enviado exitosamente.");
                NotificarTicket(txtIncidencia.Text, UserSession.usuNombre, DateTime.Now);
                TicketsForm ticketsFormPage = new TicketsForm();
                NavigationService.Navigate(ticketsFormPage);
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Ha ocurrido un error:" + ex.Message);
            }
        }

        //Notificar ticket creado
        private void NotificarTicket( string incidencia, string usuario, DateTime fecha)
        {

            // Generar la notificación de Windows
            new ToastContentBuilder()
                .AddArgument("action", "viewTicket")
                .AddText($"Nuevo ticket creado: {incidencia}")
                .AddText($"Usuario: {usuario}")
                .AddText($"Fecha de creación: {fecha}")
                .Show(); // Muestra la notificación
        }
    }
}
