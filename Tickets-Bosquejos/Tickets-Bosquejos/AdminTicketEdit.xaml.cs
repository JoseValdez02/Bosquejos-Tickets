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
using Tickets_Bosquejos.Classes;
using Microsoft.Toolkit.Uwp.Notifications;
using System.Transactions;

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

            using (MySqlConnection connection = Connection.GetConnection())
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

                        cmbResponsable.Items.Add(new KeyValuePair<int, string>((int)row["pro_clave"], row["pro_nombre"].ToString()));

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar lista" + ex.Message);
                }
            }
        }

        //Asignar responsable
        private void btnAsignar_Click(object sender, RoutedEventArgs e)
        {

            int? proClave = (cmbResponsable.SelectedValue as int?);
            string responsableSeleccionado = cmbResponsable.SelectedItem?.ToString();

            if (proClave == null)
            {
                MessageBox.Show("No se selecciono un programador válido");
                return;
            }

            if (string.IsNullOrEmpty(responsableSeleccionado))
            {
                MessageBox.Show("Por favor asigne a un programador este ticket");
                return;
            }

            AsignacionesSistemas(proClave);
            AsignarResponsable(proClave, responsableSeleccionado);

        }

        private void AsignacionesSistemas(int? proClave)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand asiCmd = new MySqlCommand("asignacionessistemas");
                    asiCmd.CommandType = System.Data.CommandType.StoredProcedure;
                    asiCmd.Parameters.AddWithValue("p_empClave", UserSession.empClave);
                    asiCmd.Parameters.AddWithValue("p_proClave", proClave);
                    asiCmd.Parameters.AddWithValue("p_usuClave", UserSession.usuClave);
                    asiCmd.Parameters.AddWithValue("p_usuIdentificacion", UserSession.usuNombre);
                    asiCmd.Parameters.AddWithValue("p_usuFecha", DateTime.Now);
                    asiCmd.ExecuteNonQuery();


                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar los tickets: " + ex.Message);

                }
                finally
                {
                    connection?.Close();
                }
            }
        }

        private void AsignarResponsable(int? proClave, string responsableSeleccionado) { 

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("asignarresponsable", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_status", "Abierto");
                    cmd.Parameters.AddWithValue("v_proClave", proClave);
                    cmd.Parameters.AddWithValue("v_proNombre", responsableSeleccionado);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", tic_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se agrego al responsable exitosamente");

                    NotificarAsignacion(responsableSeleccionado, txtIncidencia.Text, txtPrioridad.Text);
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

      

        //Notificar asignación al responsable
        private void NotificarAsignacion(string responsable, string incidencia, string prioridad)
        {

            // Generar la notificación de Windows
            new ToastContentBuilder()
                .AddArgument("action", "viewTicket")
                .AddArgument("ticketId", tic_clave)
                .AddText($"Nuevo ticket asignado: {incidencia}")
                .AddText($"Prioridad: {prioridad}")
                .AddText($"El ticket con ID: {tic_clave} ha sido asignado a {responsable}.\n" +
                $"Atienda el ticket y asigne una fecha de resolución")
                .Show(); // Muestra la notificación
        }

        private void btnDescargarPdf_Click(object sender, RoutedEventArgs e)
        {
          
            using (MySqlConnection connection = Connection.GetConnection())
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

        //Asignar fecha de resolución
        private void btnAgregarFecha_Click(object sender, RoutedEventArgs e)
        {
           
            DateTime? fechaResolucion = txtFechaResolucion.SelectedDate;



            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("asignarfecharesolucion", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_status", "Abierto");
                    cmd.Parameters.AddWithValue("v_fechaFin", fechaResolucion);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", tic_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se actualizó el ticket exitosamente");
                    NotificarFecha(txtIncidencia.Text, txtFechaResolucion.SelectedDate);
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

        //Notificar fecha de resolución
        private void NotificarFecha(string incidencia, DateTime? fecha)
        {

            // Generar la notificación de Windows
            new ToastContentBuilder()
                .AddArgument("action", "viewTicket")
                .AddArgument("ticketId", tic_clave)
                .AddText($"Fecha estimada: {fecha}")
                .AddText($"Su ticket #{tic_clave} tiene como fecha de resolución {fecha}")
                .Show(); // Muestra la notificación
        }
    }
}
