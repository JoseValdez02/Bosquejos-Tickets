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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tickets_Bosquejos.Classes;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para UsuarioForm.xaml
    /// </summary>
    public partial class UsuarioForm : Window
    {
        private readonly Action recargarTabla;
        public UsuarioForm(Action recargar)
        {
            InitializeComponent();

            CargarCmbEmpresas();

            recargarTabla = recargar;
        }

        //Cargar lista de empresas en el combobox
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

        //Placeholder para el combobox de empresa
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



        private void btnSubirUsuario_Click(object sender, RoutedEventArgs e)
        {
            string usuario = txtUsuario.Text;
            int? empClave = (cmbEmpresa.SelectedValue as int?);
            string area = txtArea.Text;
            string puesto = txtPuesto.Text;
            string correo = txtCorreo.Text;
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(area) || string.IsNullOrEmpty(puesto) || string.IsNullOrEmpty(correo) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios");
                return;
            }

            RegistrarUsuario(usuario, empClave, area, puesto, correo, password);
        }

        //Metodo para registrar una empresa
        private void RegistrarUsuario(string usuario, int? empClave, string area, string puesto, string correo, string password)
        {

            try
            {
                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("registrarusuario", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("v_usuNombre", usuario);
                    cmd.Parameters.AddWithValue("v_empClave", empClave);
                    cmd.Parameters.AddWithValue("v_area", area);
                    cmd.Parameters.AddWithValue("v_puesto", puesto);
                    cmd.Parameters.AddWithValue("v_correo", correo);
                    cmd.Parameters.AddWithValue("v_password", password);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Usuario registrado con éxito.");

                recargarTabla?.Invoke();
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error:" + ex.Message);
            }
        }
    }
}
