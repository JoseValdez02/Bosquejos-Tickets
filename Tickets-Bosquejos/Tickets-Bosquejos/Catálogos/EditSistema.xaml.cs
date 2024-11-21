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
    /// Lógica de interacción para EditSistema.xaml
    /// </summary>
    public partial class EditSistema : Window
    {
        public DataRowView SistemaSelected { get; }

        private int sis_clave;
        public EditSistema(DataRowView sistemaSeleccionado)
        {
            InitializeComponent();

            if (sistemaSeleccionado != null)
            {
                SistemaSelected = sistemaSeleccionado;

                if (int.TryParse(sistemaSeleccionado["sis_clave"].ToString(), out int clave))
                {
                    this.sis_clave = clave;
                    txtSistema.Text = sistemaSeleccionado["sis_nombre"].ToString();
                    txtLenguaje.Text = sistemaSeleccionado["sis_lenguaje"].ToString();
                    txtPlataforma.Text = sistemaSeleccionado["sis_plataforma"].ToString();
                }
                else
                {
                    MessageBox.Show("El identificador del sistema no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo cargar la empresa seleccionada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void btnEditSistema_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editarsistema", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_sisNombre", txtSistema.Text);
                    cmd.Parameters.AddWithValue("v_lenguaje", txtLenguaje.Text);
                    cmd.Parameters.AddWithValue("v_plataforma", txtPlataforma.Text);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", sis_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Sistema editado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
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
