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
    /// Lógica de interacción para CatProgramadores.xaml
    /// </summary>
    public partial class CatProgramadores : UserControl
    {
        public DataRowView programadorSeleccionado => (DataRowView)tableProgramadores.SelectedItem;
        public CatProgramadores()
        {
            InitializeComponent();
            CargarProgramadores();
        }

        // Cargar catálogo en el DataGrid
        public void CargarProgramadores()
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcatprogramadores", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableProgramadores.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar el catálogo: " + ex.Message);

                }
            }
        }

        // Recargar si se hace una modificación
        public void Recargar()
        {
            CargarProgramadores();
        }

        // Eliminar un programador
        public void EliminarProgramador()
        {
            if (programadorSeleccionado != null)
            {

                int proClave = Convert.ToInt32(programadorSeleccionado["pro_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Desea eliminar a este programador?", "Eliminar programador", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection connection = Connection.GetConnection())
                    {
                        try
                        {
                            connection.Open();

                            MySqlCommand cmd = new MySqlCommand("eliminarprogramador", connection);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("v_clave", proClave);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Empresa eliminada correctamente");

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error al eliminar la empresa" + ex.Message);
                        }
                    }
                }
            }
        }

        // Buscar a un programador
        public void Buscar(string criterio)
        {
            if (string.IsNullOrEmpty(criterio))
            {
                CargarProgramadores(); // Restaura todos los registros
                return;
            }

            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscarcatprogramadores", connection);
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
                        tableProgramadores.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron programadores con el criterio proporcionado");
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
