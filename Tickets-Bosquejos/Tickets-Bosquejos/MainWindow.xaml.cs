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
            string password = txtPassword.Password; 

            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                   
                    MySqlCommand cmd = new MySqlCommand("loginuser", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("p_usuario", usuario);
                    cmd.Parameters.AddWithValue("P_password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read() && reader["usu_puesto"] != DBNull.Value)
                        {
                            string puesto = reader["usu_puesto"].ToString();
                            int empClave = Convert.ToInt32(reader["emp_clave"]);
                            string nombreUsuario = reader["usu_nombre"].ToString();
                            string nombreEmpresa = reader["emp_nombre"].ToString();

                            reader.Close();

                            if (puesto == "soporte")
                            {
                                //Si el usuario es de soporte, ingresa a la vista de administrador
                                AdminView adminView = new AdminView();
                                adminView.SetUserInfo(nombreUsuario, nombreEmpresa);
                                adminView.Show();
                            }
                            else
                            {
                                //Si el usuario es de cualquier otra area, ingresa como usuario
                                UserView userView = new UserView();
                                userView.SetUserInfo(nombreUsuario, nombreEmpresa);
                                userView.Show();
                            }
                            
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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
        
    

