using Microsoft.Win32;
using System;
using MySql.Data.MySqlClient;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tickets_Bosquejos.Catálogos
{
    /// <summary>
    /// Lógica de interacción para EditEmpresa.xaml
    /// </summary>
    public partial class EditEmpresa : Window
    {

        private byte[] data;

       public DataRowView EmpresaSelected { get; }

       private int emp_clave;

        public EditEmpresa(CatEmpresas empresaSelected)
        {
            InitializeComponent();

           this.emp_clave = Convert.ToInt32(empresaSeleccionada["emp_clave"]);
            txtEmpresa.Text = empresaSeleccionada["emp_nombre"].ToString();
            txtLogotipo.Text = empresaSeleccionada["tic_pdf"].ToString();
        }

      public EditEmpresa(DataRowView empresaSelected)
        {
            EmpresaSelected = empresaSelected;
        }

        private void btnEditLogo_Click(object sender, RoutedEventArgs e)
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

        private void btnEditEmpresa_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
