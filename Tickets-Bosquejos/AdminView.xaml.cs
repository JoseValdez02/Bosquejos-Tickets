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
using System.Windows.Shapes;
using Tickets_Bosquejos.Classes;
using Tickets_Bosquejos.Catálogos;
using System.Windows.Navigation;
using Tickets_Bosquejos.Styles;

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para AdminView.xaml
    /// </summary>
    public partial class AdminView : Window
    {
        public AdminView()
        {
            InitializeComponent();
            SetUserInfo();
            CargarEstilos();
        }

        private void CargarEstilos()
        {
            // Aplicar colores dinámicamente a los controles
            DockPanel panelSuperior = (DockPanel)FindName("PanelSuperior");
            if (panelSuperior != null)
            {
                panelSuperior.Background = new SolidColorBrush(ViewsStyles.Panel);
            }

            Menu menu = (Menu)FindName("Menu");
            if (menu != null)
            {
                menu.Background = new SolidColorBrush(ViewsStyles.Menu);
            }

            Frame mainFrame = (Frame)FindName("MainFrame");
            if (mainFrame != null)
            {
                mainFrame.Background = new SolidColorBrush(ViewsStyles.Frame);
            }
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("¿Estás seguro de cerrar la sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
            
                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        //Metodo para obtener el nombre del usuario y la empresa
        public void SetUserInfo ()
        {
            txtBienvenida.Text = $"{UserSession.usuNombre}\n{UserSession.empNombre}";
        }


        //Navegación entre frames
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ViewsStyles.ConfigurarEstilo();
            MainFrame.Navigate(new AdminTicketsView());
        }


        private void Empresa_Click(object sender, RoutedEventArgs e)
        {
            ViewsStyles.ConfigurarEstilo();
            CargarCatalogo(new CatEmpresas());
            
        }

        private void Usuario_Click(object sender, RoutedEventArgs e)
        {
            CargarCatalogo(new CatUsuarios());
        }

        private void Sistema_Click(object sender, RoutedEventArgs e)
        {
            CargarCatalogo(new CatSistemas());
        }

        private void Programador_Click(object sender, RoutedEventArgs e)
        {
            CargarCatalogo(new CatProgramadores());
        }

        private void CargarCatalogo(UserControl control)
        {
            AdminCatView adminCatView = new AdminCatView(control);
            MainFrame.Navigate(adminCatView);
        }
    }
}
