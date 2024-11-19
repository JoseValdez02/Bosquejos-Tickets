using System;
using System.Collections.Generic;
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
using Tickets_Bosquejos.Catálogos;

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para AdminCatView.xaml
    /// </summary>
    public partial class AdminCatView : Page
    {
        public AdminCatView()
        {
            InitializeComponent();
        }

        private void Empresa_Click(object sender, RoutedEventArgs e)
        {
            var empresa = new EmpresaForm();
            empresa.ShowDialog();
        }

        private void Usuario_Click(object sender, RoutedEventArgs e)
        {
            var usuario = new UsuarioForm();
            usuario.ShowDialog();
        }

        private void Sistema_Click(object sender, RoutedEventArgs e)
        {
            var sistema = new SistemaForm();
            sistema.ShowDialog();
        }

        private void Programador_Click(object sender, RoutedEventArgs e)
        {
            var programador = new ProgramadorForm();
            programador.ShowDialog();
        }

        private void CatEmprresas_Click(object sender, RoutedEventArgs e)
        {
            CatalogoContent.Content = new Catálogos.CatEmpresas();
        }

        private void CatUsuarios_Click(object sender, RoutedEventArgs e)
        {
            CatalogoContent.Content = new Catálogos.CatUsuarios();
        }

        private void CatSistemas_Click(object sender, RoutedEventArgs e)
        {
            CatalogoContent.Content = new Catálogos.CatSistemas();
        }

        private void CatProgramadores_Click(object sender, RoutedEventArgs e)
        {
            CatalogoContent.Content = new Catálogos.CatProgramadores();
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
