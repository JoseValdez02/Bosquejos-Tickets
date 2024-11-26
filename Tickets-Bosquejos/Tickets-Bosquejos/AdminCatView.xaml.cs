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

        public AdminCatView(UserControl control)
        {
            InitializeComponent();
            activeUserControl = control;
            CatalogoContent.Content = activeUserControl;
           
        }

        //Editar un registro de los catálogos
       private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
           if (activeUserControl is CatEmpresas empresasControl)
            {
                // Obtener el registro seleccionado
                if (empresasControl.empresaSeleccionada is DataRowView empresaSelected)
                {
                    //Enviar datos de los registros al formulario correspondiente
                    EditEmpresa editEmpresaForm = new EditEmpresa(empresaSelected, empresasControl.Recargar);
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

                    EditUsuario editUsuarioForm = new EditUsuario(usuarioSelected, usuariosControl.Recargar);
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

                    EditSistema editSistemaForm = new EditSistema(sistemaSelected, sistemasControl.Recargar);
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

                    EditProgramador editProgramadorForm = new EditProgramador(programadorSelected, programadoresControl.Recargar);
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
                empresasControl.Recargar();
            }
            else if (activeUserControl is CatUsuarios usuariosControl)
            {
                usuariosControl.EliminarUsuario();
                usuariosControl.Recargar();
            }
            else if (activeUserControl is CatSistemas sistemasControl)
            {
                sistemasControl.EliminarSistema();
                sistemasControl.Recargar();
            }
            else if (activeUserControl is CatProgramadores programadoresControl)
            {
                programadoresControl.EliminarProgramador();
                programadoresControl.Recargar();
            }
            else
            {
                MessageBox.Show("No hay un catálogo activo o no se ha seleccionado un registro");
            }
        }


        //Actualizar algún catálogo activo
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

        //Buscar un registro de los catálogos
        private void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
           string criterio = txtSearchBar.Text.Trim();

            if (activeUserControl is CatEmpresas empresasControl)
            {
                empresasControl.Buscar(criterio);
            }
            else if (activeUserControl is CatUsuarios usuariosControl)
            {
                usuariosControl.Buscar(criterio);
            }
            else if (activeUserControl is CatSistemas sistemasControl)
            {
                sistemasControl.Buscar(criterio);
            }
            else if (activeUserControl is CatProgramadores programadoresControl)
            {
                programadoresControl.Buscar(criterio);
            }
            else
            {
                MessageBox.Show("No hay un catálogo activo");    
            }
        }

        //Enviar a los formularios para añadir nuevo registro
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            if (activeUserControl is CatEmpresas empresasControl)
            {

                EmpresaForm empresa = new EmpresaForm(empresasControl.Recargar);
                empresa.ShowDialog();
            }
            else if (activeUserControl is CatUsuarios usuariosControl)
            {

               UsuarioForm usuario = new UsuarioForm(usuariosControl.Recargar);
               usuario.ShowDialog();
            }
            else if (activeUserControl is CatSistemas sistemasControl)
            {

               SistemaForm sistema = new SistemaForm(sistemasControl.Recargar);
               sistema.ShowDialog();
            }
            else if (activeUserControl is CatProgramadores programadoresControl)
            {

               ProgramadorForm programador = new ProgramadorForm(programadoresControl.Recargar);
               programador.ShowDialog();
            }
            else
            {
                MessageBox.Show("Ocurrio un error.");
            }
        }
    }
}
