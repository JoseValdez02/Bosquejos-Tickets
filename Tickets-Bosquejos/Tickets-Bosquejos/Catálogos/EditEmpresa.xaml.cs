using Microsoft.Win32;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    /// Lógica de interacción para EditEmpresa.xaml
    /// </summary>
    public partial class EditEmpresa : Window
    {

        private byte[] data;

        public DataRowView EmpresaSelected { get; }

        private int emp_clave;

        public EditEmpresa(DataRowView empresaSeleccionada)
        {
            InitializeComponent();

            if (empresaSeleccionada != null)
            {
                EmpresaSelected = empresaSeleccionada;

                if (int.TryParse(empresaSeleccionada["emp_clave"].ToString(), out int clave))
                {
                    this.emp_clave = clave;
                    txtEmpresa.Text = empresaSeleccionada["emp_nombre"].ToString();
                    txtLogo.Text = empresaSeleccionada["emp_logo"].ToString();
                }
                else
                {
                    MessageBox.Show("El identificador de la empresa no es válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No se pudo cargar la empresa seleccionada.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
            }
        }

        private void btnEditLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.DefaultExt = ".png";
            dialog.Filter = "png Files (*.png)|*.png";

            dialog.Title = "Seleccione el logotipo de la empresa a registrar";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {

                string filename = dialog.FileName;
                txtLogo.Text = filename;
                data = File.ReadAllBytes(filename);
            }
        }

        private void btnEditEmpresa_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("editarempresa", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_empNombre", txtEmpresa.Text);
                    cmd.Parameters.AddWithValue("v_logo", data ?? new byte[0]);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", emp_clave);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Empresa editada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar la empresa: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
