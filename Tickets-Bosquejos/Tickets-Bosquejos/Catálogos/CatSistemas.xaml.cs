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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Tickets_Bosquejos.Classes;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para CatSistemas.xaml
    /// </summary>
    public partial class CatSistemas : UserControl
    {
        public DataRowView sistemaSeleccionado => (DataRowView)tableSistemas.SelectedItem;
        public CatSistemas()
        {
            InitializeComponent();
            CargarSistemas();
        }

        public void CargarSistemas()
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcatsistemas", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableSistemas.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar el catálogo: " + ex.Message);

                }
            }
        }

        public void EliminarSistema()
        {
            if (sistemaSeleccionado != null)
            {

                int sisClave = Convert.ToInt32(sistemaSeleccionado["sis_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Desea eliminar este sistema?", "Eliminar sistema", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection connection = Connection.GetConnection())
                    {
                        try
                        {
                            connection.Open();

                            MySqlCommand cmd = new MySqlCommand("eliminarsistema", connection);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("v_clave", sisClave);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Sistema eliminado correctamente");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar el sistema" + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
