using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;
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
    /// Lógica de interacción para TicketsForm.xaml
    /// </summary>
    public partial class TicketsForm : Page
    {
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Title = "Crear Ticket";
        }

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
                catch (Exception ex) {

                    MessageBox.Show("Error al cargar sistemas" + ex.Message);
                }
            }
        }
    }
}

