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
using Tickets_Bosquejos.Catálogos;


namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para AdminCatView.xaml
    /// </summary>
    public partial class AdminCatView : Page
    {
        private UserControl activeUserControl;

        public AdminCatView()
        {
            InitializeComponent();
        }

        //Abrir formularios para dar de alta usuarios, empresas, sistemas y programadores
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

        // Cargar catálogos en el ContentControl
        private void CatEmprresas_Click(object sender, RoutedEventArgs e)
        {
            //Cargar los controles de usuario de los catalogos en el ContentControl
            var empresasControl = new CatEmpresas();
            CatalogoContent.Content = empresasControl;
            activeUserControl = empresasControl;
        }

        private void CatUsuarios_Click(object sender, RoutedEventArgs e)
        {
            var usuariosControl = new CatUsuarios();
            CatalogoContent.Content = usuariosControl;
            activeUserControl = usuariosControl;
        }

        private void CatSistemas_Click(object sender, RoutedEventArgs e)
        {
            var sistemasControl = new CatSistemas();
            CatalogoContent.Content = sistemasControl;
            activeUserControl = sistemasControl;
        }

        private void CatProgramadores_Click(object sender, RoutedEventArgs e)
        {
            var programadoresControl = new CatProgramadores();
            CatalogoContent.Content = programadoresControl;
            activeUserControl = programadoresControl;
        }

      
        //Editar un registro de los catálogos
       private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           if (activeUserControl is CatEmpresas empresasControl)
            {
                // Obtener la empresa seleccionado
                if (empresasControl.empresaSeleccionada is DataRowView empresaSelected)
                {

                    EditEmpresa editEmpresaForm = new EditEmpresa(empresaSelected);
                    editEmpresaForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Selecciona una empresa");
                }
            }
           else if(activeUserControl is CatUsuarios usuariosControl)
            {

                if(usuariosControl.usuarioSeleccionado is DataRowView usuarioSelected)
                {

                    EditUsuario editUsuarioForm = new EditUsuario(usuarioSelected);
                    editUsuarioForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Selecciona un usuario");
                }
            }
            else if (activeUserControl is CatSistemas sistemasControl)
            {

                if (sistemasControl.sistemaSeleccionado is DataRowView sistemaSelected)
                {

                    EditSistema editSistemaForm = new EditSistema(sistemaSelected);
                    editSistemaForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Selecciona un sistema");
                }
            }
            else if (activeUserControl is CatProgramadores programadoresControl)
            {

                if (programadoresControl.programadorSeleccionado is DataRowView programadorSelected)
                {

                    EditProgramador editProgramadorForm = new EditProgramador(programadorSelected);
                    editProgramadorForm.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Selecciona un programador");
                }
            }
            else
            {
                MessageBox.Show("No hay un catálogo activo o no se puede editar.");
            }
        }

        // Seleccionar un registro de los catálogos para eliminar
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (activeUserControl is CatEmpresas empresasControl)
            {

                empresasControl.EliminarEmpresa();
            }
            else if (activeUserControl is CatUsuarios usuariosControl)
            {
                usuariosControl.EliminarUsuario();
            }
            else if (activeUserControl is CatSistemas sistemasControl)
            {
                sistemasControl.EliminarSistema();
            }
            else if (activeUserControl is CatProgramadores programadoresControl)
            {
                programadoresControl.EliminarProgramador();
            }
            else
            {
                MessageBox.Show("No hay un catálogo activo o se ha seleccionado un registro");
            }
        }


        //Actualizar algún catálogo
        private void btnActualizar_Click(object sender, RoutedEventArgs e)
        {
            if (activeUserControl is CatEmpresas empresasControl)
            {
                empresasControl.CargarEmpresas();
            }
            else if (activeUserControl is CatUsuarios usuariosControl)
            {
                usuariosControl.CargarUsuarios();
            }
            else if (activeUserControl is CatSistemas sistemasControl)
            {
                sistemasControl.CargarSistemas();
            }
            else if (activeUserControl is CatProgramadores programadoresControl)
            {
                programadoresControl.CargarProgramadores();
            }
            else
            {
                MessageBox.Show("No hay un catálogo activo");
            }
        }

        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
