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

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para EmpresaForm.xaml
    /// </summary>
    public partial class EmpresaForm : Window
    {
        private readonly Action recargarTabla;

        private byte[] data;
        public EmpresaForm(Action recargar)
        {
            InitializeComponent();
            recargarTabla = recargar;
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

                    MySqlCommand cmd = new MySqlCommand("registrarempresa", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("v_empNombre", empresa);
                    cmd.Parameters.AddWithValue("v_logotipo", data ?? new byte[0]);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
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
