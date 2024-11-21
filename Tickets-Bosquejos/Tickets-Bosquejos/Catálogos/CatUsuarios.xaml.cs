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
using MySql.Data.MySqlClient;
using Tickets_Bosquejos.Classes;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para CatUsuarios.xaml
    /// </summary>
    public partial class CatUsuarios : UserControl
    {
        public DataRowView usuarioSeleccionado => (DataRowView)tableUsuarios.SelectedItem;
        public CatUsuarios()
        {
            InitializeComponent();

            CargarUsuarios();
        }

        public void CargarUsuarios()
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcatusuarios", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableUsuarios.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar el catálogo: " + ex.Message);

                }
            }
         }

        public void EliminarUsuario()
        {
            if (usuarioSeleccionado != null)
            {

                int usuClave = Convert.ToInt32(usuarioSeleccionado["usu_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Desea eliminar este usuario?", "Eliminar usuario", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection connection = Connection.GetConnection())
                    {
                        try
                        {
                            connection.Open();

                            MySqlCommand cmd = new MySqlCommand("eliminarusuario", connection);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("v_clave", usuClave);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Usuario eliminado correctamente");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar al usuario" + ex.Message);
                        }
                    }
                }
            }
        }
    }
}
