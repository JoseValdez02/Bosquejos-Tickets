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

        //Cargar el catálogo en el DataGrid
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

        // Recargar el catálogo en caso de una modificación
        public void Recargar()
        {
            CargarSistemas();
        }


        //Eliminar un sistema
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

        //Buscar un sistema
        public void Buscar(string criterio)
        {
            if (string.IsNullOrEmpty(criterio))
            {
                CargarSistemas(); // Restaura todos los registros
                return;
            }

            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscarcatsistemas", connection);
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
                        tableSistemas.ItemsSource = dt.DefaultView;
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron empresas con el criterio proporcionado");
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
