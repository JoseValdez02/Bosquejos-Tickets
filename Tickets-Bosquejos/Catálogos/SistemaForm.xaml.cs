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
using Tickets_Bosquejos.Styles;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para SistemaForm.xaml
    /// </summary>
    public partial class SistemaForm : Window
    {
        private readonly Action recargarTabla;
        public SistemaForm(Action recargar)
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

            Button botonSubirSistema = (Button)FindName("btnSubirSistema");
            if (botonSubirSistema != null)
            {
                botonSubirSistema.Background = new SolidColorBrush(ViewsStyles.Button);
            }

            Grid mainGrid = (Grid)FindName("GridPrincipal");
            if (mainGrid != null)
            {
                mainGrid.Background = new SolidColorBrush(ViewsStyles.Grid);
            }
        }

        private void btnSubirSistema_Click(object sender, RoutedEventArgs e)
        {

            string sistema = txtSistema.Text;
            string lenguaje = txtLenguaje.Text;
            string plataforma = txtPlataforma.Text;

            if (string.IsNullOrEmpty(sistema) || string.IsNullOrEmpty(lenguaje) || string.IsNullOrEmpty(plataforma))
            {
                MessageBox.Show("Por favor complete todos los campos obligatorios");
                return;
            }

            RegistrarSistema(sistema, lenguaje, plataforma);
        }

        //Metodo para registrar una empresa
        private void RegistrarSistema(string sistema, string lenguaje, string plataforma)
        {

            try
            {
                using (MySqlConnection connection = Connection.GetConnection())
                {
                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("registrarsistemas", connection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("v_sisNombre", sistema);
                    cmd.Parameters.AddWithValue("v_lenguaje", lenguaje);
                    cmd.Parameters.AddWithValue("v_plataforma", plataforma);
                    cmd.Parameters.AddWithValue("v_usuIdentificacion", UserSession.usuNombre);
                    cmd.Parameters.AddWithValue("v_usuFecha", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
                MessageBox.Show("Sistema registrado con éxito.");

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
