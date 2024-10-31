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
        public AdminTicketEdit()
        {
            InitializeComponent();
            CargarCmbResponsable();
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
    }
}
