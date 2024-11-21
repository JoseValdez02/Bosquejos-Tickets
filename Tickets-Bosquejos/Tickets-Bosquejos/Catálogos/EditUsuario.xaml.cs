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

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para EditUsuario.xaml
    /// </summary>
    public partial class EditUsuario : Window
    {

        public DataRowView UsuarioSelected { get; }

        private int usu_clave;
        public EditUsuario(DataRowView usuarioSeleccionado)
        {
            InitializeComponent();
            CargarCmbEmpresas();

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

        private void CargarCmbEmpresas()
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcmbempresas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {

                        cmbEmpresa.Items.Add(new KeyValuePair<int, string>((int)row["emp_clave"], row["emp_nombre"].ToString()));

                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar empresas" + ex.Message);
                }
            }
        }

        private void cmbEmpresa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbEmpresa.SelectedItem != null && cmbEmpresa.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled)
                {

                    cmbEmpresa.Text = "Seleccione una empresa";
                }
                else
                {
                    cmbEmpresa.Text = selectedItem.Content.ToString();
                }
            }
        }

        private void cmbEmpresa_Loaded(object sender, RoutedEventArgs e)
        {
            cmbEmpresa.SelectedIndex = 0;
        }

        private void btnEditUsuario_Click(object sender, RoutedEventArgs e)
        {
            int? empClave = (cmbEmpresa.SelectedValue as int?);
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editarusuario", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_usuNombre", txtUsuario.Text);
                    cmd.Parameters.AddWithValue("v_empClave", empClave);
                    cmd.Parameters.AddWithValue("v_area", txtArea.Text);
                    cmd.Parameters.AddWithValue("v_puesto", txtPuesto.Text);
                    cmd.Parameters.AddWithValue("v_correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("v_password", txtPassword.Text);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", usu_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Usuario editado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar al usuario: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
