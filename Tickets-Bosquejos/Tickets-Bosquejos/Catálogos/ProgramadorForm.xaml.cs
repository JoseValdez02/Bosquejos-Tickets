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
    /// Lógica de interacción para ProgramadorForm.xaml
    /// </summary>
    public partial class ProgramadorForm : Window
    {
        public ProgramadorForm()
        {
            InitializeComponent();
        }

        private void btnSubirProgramador_Click(object sender, RoutedEventArgs e)
        {
            string programador = txtProgramador.Text;
            string correo = txtCorreo.Text;
           

            if (string.IsNullOrEmpty(programador) || string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios");
                return;
            }

            RegistrarProgramador(programador, correo);
        }

        //Metodo para registrar una empresa
        private void RegistrarProgramador(string programador, string correo)
        {

            try
            {
                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("registrarprogramador", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("v_proNombre", programador);
                    cmd.Parameters.AddWithValue("v_correo", correo);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Sistema registrado con éxito.");
                this.Close();


            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error:" + ex.Message);
            }
        }
    }

}
