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
using System.Windows.Shapes;
using Tickets_Bosquejos.Classes;
using Tickets_Bosquejos.Styles;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para EditUsuario.xaml
    /// </summary>
    public partial class EditUsuario : Window
    {
        private readonly Action recargarTabla;
        public DataRowView UsuarioSelected { get; }

        private int usu_clave;
        public EditUsuario(DataRowView usuarioSeleccionado, Action recargar)
        {
            InitializeComponent();
            CargarEstilos();
            recargarTabla = recargar;

            if (usuarioSeleccionado != null)
            {
                UsuarioSelected = usuarioSeleccionado;

                if (int.TryParse(usuarioSeleccionado["usu_clave"].ToString(), out int clave))
                {
                    this.usu_clave = clave;
                    txtUsuario.Text = usuarioSeleccionado["usu_nombre"].ToString();
                    txtArea.Text = usuarioSeleccionado["usu_area"].ToString();
                    txtPuesto.Text = usuarioSeleccionado["usu_puesto"].ToString();
                    txtCorreo.Text = usuarioSeleccionado["usu_correo"].ToString();
                    txtPassword.Text = usuarioSeleccionado["usu_password"].ToString();
                }
                else
                {
                    MessageBox.Show("El identificador del usuario no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo cargar el usuario seleccionada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }

        }

        private void CargarEstilos()
        {
            // Aplicar colores dinámicamente a los controles
            Border border = (Border)FindName("BorderForm");
            if (border != null)
            {
                border.BorderBrush = new SolidColorBrush(ViewsStyles.Border);
            }

            Button botonEditarUsuario = (Button)FindName("btnEditUsuario");
            if (botonEditarUsuario != null)
            {
                botonEditarUsuario.Background = new SolidColorBrush(ViewsStyles.Button);
            }

            Grid mainGrid = (Grid)FindName("GridPrincipal");
            if (mainGrid != null)
            {
                mainGrid.Background = new SolidColorBrush(ViewsStyles.Grid);
            }
        }

        private void btnEditUsuario_Click(object sender, RoutedEventArgs e)
        {
           
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editarcatusuarios", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_usuNombre", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("v_area", txtArea.Text);
                    cmd.Parameters.AddWithValue("v_puesto", txtPuesto.Text);
                    cmd.Parameters.AddWithValue("v_correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("v_password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", usu_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario editado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    recargarTabla?.Invoke();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar al usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
