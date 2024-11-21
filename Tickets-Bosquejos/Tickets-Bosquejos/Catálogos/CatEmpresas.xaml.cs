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

                            MySqlCommand cmd = new MySqlCommand("eliminarempresa", connection);
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
    }
}
