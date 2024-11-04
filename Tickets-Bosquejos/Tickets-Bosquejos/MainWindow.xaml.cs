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

            // Cadena de conexión a la base de datos MySQL
            string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {

                    connection.Open();

                    string query = @"SELECT u.usu_puesto, u.emp_clave, u.usu_nombre, e.emp_nombre 
                             FROM liccatusuarios u 
                             INNER JOIN liccatempresas e ON u.emp_clave = e.emp_clave 
                             WHERE u.usu_identificacion = @usuario AND u.usu_password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string puesto = reader["usu_puesto"].ToString();
                            int empClave = Convert.ToInt32(reader["emp_clave"]);
                            string nombreUsuario = reader["usu_nombre"].ToString();
                            string nombreEmpresa = reader["emp_nombre"].ToString();

                            reader.Close();

                            //Actualizar fecha de inicio de sesión de los usuarios
                            string actualizarFechaUsuarios = "UPDATE liccatusuarios SET usu_fecha = NOW() WHERE usu_identificacion = @usuario";
                            MySqlCommand updateUserCmd = new MySqlCommand(actualizarFechaUsuarios, connection);
                            updateUserCmd.Parameters.AddWithValue("@usuario", usuario);
                            updateUserCmd.ExecuteNonQuery();

                            //Actualizar fecha de inicio de sesión de las empresas
                            string actualizarFechaEmpresas = "UPDATE liccatempresas SET usu_fecha = NOW() WHERE emp_clave = @empClave";
                            MySqlCommand updateEmpresaCmd = new MySqlCommand(actualizarFechaEmpresas, connection);
                            updateEmpresaCmd.Parameters.AddWithValue("@empClave", empClave);
                            updateEmpresaCmd.ExecuteNonQuery();

                            //Actualizar quien fue el ultimo usuario de la empresa en ingresar a la empresa
                            string actualizarEmpresaUsuario = "UPDATE liccatempresas SET usu_identificacion = @usuario WHERE emp_clave = @empClave";
                            MySqlCommand updateEmpresaUsuarioCmd = new MySqlCommand(actualizarEmpresaUsuario, connection);
                            updateEmpresaUsuarioCmd.Parameters.AddWithValue("@usuario", usuario);
                            updateEmpresaUsuarioCmd.Parameters.AddWithValue("@empClave", empClave);
                            updateEmpresaUsuarioCmd.ExecuteNonQuery();


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
        
    

