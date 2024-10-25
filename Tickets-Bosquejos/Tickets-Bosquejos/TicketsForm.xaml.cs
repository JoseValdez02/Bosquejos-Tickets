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
    /// Lógica de interacción para TicketsForm.xaml
    /// </summary>
    public partial class TicketsForm : Page
    {
        public TicketsForm()
        {
            InitializeComponent();
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
