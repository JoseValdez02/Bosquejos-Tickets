using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Tickets_Bosquejos.Classes
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Verifica si el valor es un arreglo de bytes (indicativo de que hay un PDF)
            if (value != null && value is byte[])
            {
                // Imagen en caso de que el ticket si contenga un pdf
                return new BitmapImage(new Uri("C:\\Users\\SISTEMAS\\source\\repos\\Tickets-Bosquejos\\Tickets-Bosquejos\\Images\\check.png"));
            }
            else
            {
                // Imagen en caso de que el ticket no contenga un pdf
                return new BitmapImage(new Uri("C:\\Users\\SISTEMAS\\source\\repos\\Tickets-Bosquejos\\Tickets-Bosquejos\\Images\\close.png"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
