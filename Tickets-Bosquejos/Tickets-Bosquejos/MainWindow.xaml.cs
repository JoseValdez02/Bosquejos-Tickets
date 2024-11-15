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
using Tickets_Bosquejos.Classes;

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

        //Botón de inicio de sesión
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            string password = txtPassword.Password; 

            using (MySqlConnection connection = Connection.GetConnection())
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
                            UserSession.usuPuesto = reader["usu_puesto"].ToString();
                            UserSession.empClave = Convert.ToInt32(reader["emp_clave"]);
                            UserSession.usuNombre = reader["usu_nombre"].ToString();
                            UserSession.empNombre = reader["emp_nombre"].ToString();
                            UserSession.usuClave = Convert.ToInt32(reader["usu_clave"]);

                            reader.Close();

                            MySqlCommand logCmd = new MySqlCommand("registrologin", connection);
                            logCmd.CommandType = System.Data.CommandType.StoredProcedure;
                            logCmd.Parameters.AddWithValue("p_empClave", UserSession.empClave);
                            logCmd.Parameters.AddWithValue("p_usuClave", UserSession.usuClave);
                            logCmd.Parameters.AddWithValue("p_empNombre", UserSession.empNombre);
                            logCmd.Parameters.AddWithValue("p_usuIdentificacion", UserSession.usuNombre);
                            logCmd.Parameters.AddWithValue("p_usuFecha", DateTime.Now);
                            logCmd.ExecuteNonQuery();

                            if (UserSession.usuPuesto == "soporte")
                            {
                                //Si el usuario es de soporte, ingresa a la vista de administrador
                                AdminView adminView = new AdminView();
                                adminView.Show();
                            }
                            else
                            {
                                //Si el usuario es de cualquier otra area, ingresa como usuario
                                UserView userView = new UserView();
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
        
    

