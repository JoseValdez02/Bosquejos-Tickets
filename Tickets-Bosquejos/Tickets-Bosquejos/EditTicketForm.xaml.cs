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

namespace Tickets_Bosquejos
{
    /// <summary>
    /// Lógica de interacción para EditTicketForm.xaml
    /// </summary>
    public partial class EditTicketForm : Page
    {
        public EditTicketForm()
        {
            InitializeComponent();
        }

        private void btnVolver_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MyTickets());

            MyTickets misTickets = new MyTickets();

            NavigationService.Navigate(misTickets);
        }

        private void cmbPrioridad_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbPrioridad.SelectedItem != null && cmbPrioridad.SelectedItem is ComboBoxItem selectedItem)
            {
                if (!selectedItem.IsEnabled)
                {

                    cmbPrioridad.Text = "Selecciona la prioridad";
                }
                else
                {
                    cmbPrioridad.Text = selectedItem.Content.ToString();
                }
            }
        }

            private void cmbPrioridad_Loaded(object sender, RoutedEventArgs e)
            {
                cmbPrioridad.SelectedIndex = 0;
            }
        }
    }

