using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tickets_Bosquejos.Classes;
using Tickets_Bosquejos.Styles;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para EmpresaForm.xaml
    /// </summary>
    public partial class EmpresaForm : Window
    {
        //Delegado para recargar la tabla al dar de alta
        private readonly Action recargarTabla;

        private byte[] data;
        public EmpresaForm(Action recargar)
        {
            InitializeComponent();

            CargarEstilos();

            recargarTabla = recargar;
        }

        private void CargarEstilos()
        {
            // Aplicar colores dinámicamente a los controles
            Border border = (Border)FindName("BorderForm");
            if (border != null)
            {
                border.BorderBrush = new SolidColorBrush(ViewsStyles.Border);
            }

            Button botonSubirEmpresa = (Button)FindName("BtnSubirEmpresa");
            if (botonSubirEmpresa != null)
            {
                botonSubirEmpresa.Background = new SolidColorBrush(ViewsStyles.Button);
            }

            Grid mainGrid = (Grid)FindName("GridPrincipal");
            if (mainGrid != null)
            {
                mainGrid.Background = new SolidColorBrush(ViewsStyles.Grid);
            }
        }

        //Subir logotipo
        private void btnSubirLogo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.DefaultExt = ".png";
            dialog.Filter = "png Files (*.png)|*.png";

            dialog.Title = "Seleccione el logotipo de la empresa a registrar";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {

                string filename = dialog.FileName;
                TxtLogo.Text = filename;
                data = File.ReadAllBytes(filename);
            }
        }

        private void BtnSubirEmpresa_Click(object sender, RoutedEventArgs e)
        {
            string empresa = txtEmpresa.Text;

            if (string.IsNullOrEmpty(empresa))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios");
                return;
            }

            RegistrarEmpresa(empresa);
           
        }

        //Metodo para registrar una empresa
        private void RegistrarEmpresa(string empresa)
        {

            try
            {
                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("registrarcatempresas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("v_empNombre", empresa);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);

                    if (data == null || data.Length == 0)
                    {
                        //Valor nulo en caso de no añadirse un logotipo
                        cmd.Parameters.AddWithValue("v_logo", DBNull.Value);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("v_logo", data);
                    }

                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Empresa registrada con éxito.");

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
