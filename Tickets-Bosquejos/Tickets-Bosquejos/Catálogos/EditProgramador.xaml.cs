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
    /// Lógica de interacción para EditProgramador.xaml
    /// </summary>
    public partial class EditProgramador : Window
    {
        private readonly Action recargarTabla;
        public DataRowView SistemaSelected { get; }

        private int pro_clave;
        public EditProgramador(DataRowView programadorSeleccionado, Action recargar)
        {
            InitializeComponent();

            recargarTabla = recargar;

            if (programadorSeleccionado != null)
            {
                SistemaSelected = programadorSeleccionado;

                if (int.TryParse(programadorSeleccionado["pro_clave"].ToString(), out int clave))
                {
                    this.pro_clave = clave;
                    txtProgramador.Text = programadorSeleccionado["pro_nombre"].ToString();
                    txtCorreo.Text = programadorSeleccionado["pro_correo"].ToString();
                   
                }
                else
                {
                    MessageBox.Show("El identificador del programador no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo cargar el programador seleccionada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void btnEditProgramador_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editarcatprogramadores", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_proNombre", txtProgramador.Text);
                    cmd.Parameters.AddWithValue("v_correo", txtCorreo.Text);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", pro_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Programador editado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    recargarTabla?.Invoke();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar al programador: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
