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
using Tickets_Bosquejos.Styles;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para EditEmpresa.xaml
    /// </summary>
    public partial class EditEmpresa : Window
    {

        private readonly Action recargarTabla;

        private byte[] data;

        public DataRowView EmpresaSelected { get; }

        private int emp_clave;

        public EditEmpresa(DataRowView empresaSeleccionada, Action recargar)
        {
            InitializeComponent();

            CargarEstilos();

            recargarTabla = recargar;

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

        private void CargarEstilos()
        {
            // Aplicar colores dinámicamente a los controles
            Border border = (Border)FindName("BorderForm");
            if (border != null)
            {
                border.BorderBrush = new SolidColorBrush(ViewsStyles.Border);
            }

            Button botonEditarEmpresa = (Button)FindName("btnEditEmpresa");
            if (botonEditarEmpresa != null)
            {
                botonEditarEmpresa.Background = new SolidColorBrush(ViewsStyles.Button);
            }

            Grid mainGrid = (Grid)FindName("GridPrincipal");
            if (mainGrid != null)
            {
                mainGrid.Background = new SolidColorBrush(ViewsStyles.Grid);
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

                    MySqlCommand cmd = new MySqlCommand("editarcatempresas", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_empNombre", txtEmpresa.Text);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("v_clave", emp_clave);

                    if (data == null || data.Length == 0)
                    {
                        cmd.Parameters.AddWithValue("v_logo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("v_logo", data);
                    }

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Empresa editada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    recargarTabla?.Invoke();
                    this.Close();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar la empresa: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnDescargarLogo_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {
                    connection.Open();

                    // Obtener pdf por la clave del ticket
                    MySqlCommand cmd = new MySqlCommand("descargarlogocatempresas", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("v_clave", emp_clave);

                    object result = cmd.ExecuteScalar();

                    if (result != DBNull.Value && result != null)
                    {
                        byte[] Logodata = (byte[])result;

                        SaveFileDialog dialog = new SaveFileDialog();

                        dialog.Filter = "png Files (*.png)|*.png";
                        dialog.Title = "Guardar el logotipo actual de la empresa en el equipo";
                        dialog.FileName = "logotipo.png";

                        if (dialog.ShowDialog() == true)
                        {

                            string filePath = dialog.FileName;

                            //Guardar archivo en la ruta a seleccionar
                            File.WriteAllBytes(filePath, Logodata);


                            MessageBox.Show("Se guardó el logotipo correctamente");

                           
                        }
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún logotipo adjunto a esta empresa.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al descargar el logotipo: " + ex.Message);
                }
            }
        }
    }
}
