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

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para UserView.xaml
    /// </summary>
    public partial class UserView : Window
    {
        public UserView()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new TicketsForm());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new MyTickets());

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estas seguro de cerrar la sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }
    }
}
