﻿using Microsoft.Win32;
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
using Tickets_Bosquejos.Classes;
using Microsoft.Toolkit.Uwp.Notifications;

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

            //Cargar el ticket seleccionado en el formulario
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
            Window.GetWindow(this).Title = "Editar Ticket";
        }


        //Subir PDF
        private void btnSubirPdf_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.DefaultExt = ".pdf";
            dialog.Filter = "PDF Files (*.pdf)|*.pdf";
            dialog.Title = "Seleccione un documento PDF";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {

                string filename = dialog.FileName;
                txtPdf.Text = filename;
                data = File.ReadAllBytes(filename);
            }
        }

       
        //Enviar actualizaciones del ticket
        private void btnEnviarEdit_Click(object sender, RoutedEventArgs e)
        {

            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editartickets", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_observaciones", txtObservaciones.Text);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", tic_clave);

                    if (data == null || data.Length == 0)
                    {
                        //Valor nulo en caso de añadirse un pdf al ticket
                        cmd.Parameters.AddWithValue("v_pdf", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("v_pdf", data);
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Se actualizó el ticket exitosamente");
                    NotificarActualizacion(txtIncidencia.Text);
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

        //Notificar actualización del ticket
        private void NotificarActualizacion(string incidencia)
        {

            // Generar la notificación de Windows
            new ToastContentBuilder()
                .AddArgument("action", "viewTicket")
                .AddArgument("ticketId", tic_clave)
                .AddText($"Actualización del ticket: {incidencia}")
                .AddText($"El ticket #{tic_clave} ha sido actualizado con nueva información.\n" +
                $"Revise en las observaciones o PDF")
                .Show(); // Muestra la notificación
        }
    }
}
    

