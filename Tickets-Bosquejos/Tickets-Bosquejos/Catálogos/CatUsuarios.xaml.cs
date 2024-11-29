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

        //Cargar el catálogo en el DataGrid
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

        //Recargar en caso de una modificación
        public void Recargar()
        {
            CargarUsuarios();
        }

        //Eliminar un usuario
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

                            MySqlCommand cmd = new MySqlCommand("eliminarcatusuarios", connection);
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

        //Buscar un usuario
        public void Buscar(string criterio)
        {
            if (string.IsNullOrEmpty(criterio))
            {
                CargarUsuarios(); // Restaura todos los registros
                return;
            }

            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscarcatusuarios", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    if (int.TryParse(criterio, out int criterioInt))
                    {
                        cmd.Parameters.AddWithValue("p_criterio", criterioInt);
                        cmd.Parameters.AddWithValue("p_criterioNombre", " ");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("p_criterio", -1);
                        cmd.Parameters.AddWithValue("p_criterioNombre", criterio);
                    }

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        tableUsuarios.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron usuarios con el criterio proporcionado");
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show("Ocurrio un error: " + ex.Message);

                }
            }
        }
    }
}
