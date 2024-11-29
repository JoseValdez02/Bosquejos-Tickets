﻿using System;
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
using Tickets_Bosquejos.Styles;

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

        //Navegación entre frames
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ViewsStyles.ConfigurarEstilo();
            MainFrame.Navigate(new TicketsForm());
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            ViewsStyles.ConfigurarEstilo();
            MainFrame.Navigate(new MyTickets());

        }

        //Cerrar sesión
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Estás seguro de cerrar la sesión?", "Cerrar Sesión", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {

                MainWindow loginWindow = new MainWindow();
                loginWindow.Show();
                this.Close();
            }
        }

        //Metodo para obtener el nombre del usuario y la empresa
        public void SetUserInfo()
        {
            txtBienvenida.Text = $"{UserSession.usuNombre} \n {UserSession.empNombre}";
        }

      
    }
}