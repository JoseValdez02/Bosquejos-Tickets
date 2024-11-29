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
    /// Lógica de interacción para CatEmpresas.xaml
    /// </summary>
    public partial class CatEmpresas : UserControl
    {
        public DataRowView empresaSeleccionada => (DataRowView)tableEmpresas.SelectedItem;
        public CatEmpresas()
        {
            InitializeComponent();

            CargarEmpresas();
        }

        //Cargar catálogo en el DataGrid
        public void CargarEmpresas()
        {
            using (MySqlConnection connection = Connection.GetConnection())
            {
                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("cargarcatempresas", connection);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    tableEmpresas.ItemsSource = dt.DefaultView;

                }
                catch (Exception ex)
                {

                    MessageBox.Show("Error al cargar el catálogo: " + ex.Message);

                }
            }
        }

     
        //Recargar automaticamente cuando se haga una modificación
        public void Recargar()
        {
            CargarEmpresas();
        }

        //Eliminar una empresa
        public void EliminarEmpresa()
        {
            if (empresaSeleccionada != null)
            {

                int empClave = Convert.ToInt32(empresaSeleccionada["emp_clave"]);

                MessageBoxResult result = MessageBox.Show("¿Desea eliminar esta empresa?", "Eliminar empresa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (MySqlConnection connection = Connection.GetConnection())
                    {
                        try
                        {
                            connection.Open();

                            MySqlCommand cmd = new MySqlCommand("eliminarcatempresas", connection);
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("v_clave", empClave);
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

        // Buscar una empresa
        public void Buscar(string criterio)
        {
            if (string.IsNullOrEmpty(criterio))
            {
                CargarEmpresas(); // Restaura todos los registros
                return;
            }

            using (MySqlConnection connection = Connection.GetConnection())
            {

                try
                {

                    connection.Open();

                    MySqlCommand cmd = new MySqlCommand("buscarcatempresas", connection);
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
                        tableEmpresas.ItemsSource = dt.DefaultView;
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
