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
using Tickets_Bosquejos.UserClass;

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

            //Cargar página de la vista de los tickets en el frame 
            MainFrame.Navigate(new AdminTicketsView());
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

      
    }
}
