﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Lógica de interacción para AdminTicketsView.xaml
    /// </summary>
    public partial class AdminTicketsView : Page
    {
        public AdminTicketsView()
        {
            InitializeComponent();

            CargarTickets();
        }

        //Placeholder
        private void cmbFiltrarBusqueda_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiltrarBusqueda.SelectedItem != null && cmbFiltrarBusqueda.SelectedItem is ComboBoxItem selectedItem)
            {
                string filtrar = selectedItem.Content.ToString();

                if (filtrar == "Filtrar tickets")
                {

                    CargarTickets();
                }
                else
                {
                    FiltrarBusqueda(filtrar);
                }
            }
        }

        private void cmbFiltrarBusqueda_Loaded(object sender, RoutedEventArgs e)
        {
            cmbFiltrarBusqueda.SelectedIndex = 0;
        }


        //Cargar titulo de la pagina
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            Window.GetWindow(this).Title = "Administrar Tickets";
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

                    MySqlCommand cmd = new MySqlCommand("cargarticketadmin", connection);
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

        //Recargar el datagrid cuando se edite un ticket
        public void RecargarTickets()
        {
            CargarTickets();
        }


        //Filtrar busqueda por status
        private void FiltrarBusqueda(string filtrar)
        {
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("filtrarbusqueda", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_statusFiltro", filtrar);
                    cmd.Parameters.AddWithValue("p_prioridadFiltro", filtrar);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if(dt.Rows.Count > 0)
                    {
                        tableTickets.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron registros en el filtro seleccionado");
                        tableTickets.ItemsSource = null;
                    }
                   
                }

                catch (Exception ex)
                {

                    MessageBox.Show("Error al filtrar tickets: " + ex.Message);

                }
            }
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

                    MySqlCommand cmd = new MySqlCommand("buscarticketsadmin", connection);
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

                    if (dt.Rows.Count > 0)
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

        //Botón de actualizar
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();


                    MySqlCommand cmd = new MySqlCommand("actualizartablaadmin", connection);
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

        //Ir a la vista para agregar un responsable al ticket
        private void btnAgregarResponsable_Click(object sender, RoutedEventArgs e)
        {
            if (tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {
                string status = ticketSeleccionado["tic_status"].ToString();

                if (status == "Resuelto")
                {
                    MessageBox.Show("Este ticket ya se encuentra resuelto");
                    return;
                }

                AdminTicketEdit adminEditTicket = new AdminTicketEdit(ticketSeleccionado);
                this.NavigationService.Navigate(adminEditTicket);
            }
            else
            {
                MessageBox.Show("Por favor seleccione un ticket");
            }
        }

        private void btnSolucionado_Click(object sender, RoutedEventArgs e)
        {
            if (tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {
                string status = ticketSeleccionado["tic_status"].ToString();

                if(status == "Resuelto")
                {
                    MessageBox.Show("Este ticket ya se encuentra resuelto");
                    return;
                }

                int ticClave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

                MessageBoxResult result = MessageBox.Show("Este ticket se marcará como resuelto \n ¿Está seguro?", "", MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    ResueltoStatus(ticClave);
                    RecargarTickets();
                }
            }
            else
            {
                MessageBox.Show("Por favor seleccione un ticket");
            }
        }

        //Cambiar estado a resuelto
        private void ResueltoStatus(int ticClave)
        {

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("statusresuelto", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_status", "Resuelto");
                    cmd.Parameters.AddWithValue("v_fechafin", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", ticClave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Ticket resuelto");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el ticket" + ex.Message);
                }
            }
        }

        //Eliminar ticket
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (tableTickets.SelectedItem is DataRowView ticketSeleccionado)
            {

                int ticClave = Convert.ToInt32(ticketSeleccionado["tic_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Estás seguro de eliminar este ticket?\n No se podrá recuperar", "Eliminar ticket", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
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
        private void EliminarTicket(int ticClave)
        {

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
    }
}
