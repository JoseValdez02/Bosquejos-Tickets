using System;
using System.Collections.Generic;
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
using MySql.Data.MySqlClient;

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Password; // Obtener contraseña del PasswordBox

            // Cadena de conexión a la base de datos MySQL
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta para verificar si el usuario y contraseña son válidos
                    string query = "SELECT usu_puesto FROM liccatusuarios WHERE usu_identificacion = @usuario AND usu_password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    // Ejecutar la consulta
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // Si se encuentra el usuario, obtener el rol
                        string posicion = result.ToString();

                        string actualizarFecha = "UPDATE liccatusuarios SET usu_fecha = @fecha WHERE usu_identificacion = @usuario";
                        MySqlCommand updateCmd = new MySqlCommand(actualizarFecha, connection);
                        updateCmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        updateCmd.Parameters.AddWithValue("@usuario", usuario);
                        updateCmd.ExecuteNonQuery();

                        if (posicion == "soporte")
                        {
                            // Si es administrador, abrir la vista de Admin
                            AdminView adminView = new AdminView();
                            adminView.Show();
                        }
                        else
                        {
                            // Si es usuario normal, abrir la vista de Usuario
                            UserView userView = new UserView();
                            userView.Show();
                        }

                        // Cerrar la ventana de login
                        this.Close();
                    }
                    else
                    {
                        // Mostrar mensaje de error si las credenciales son incorrectas
                        MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al conectar con la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
