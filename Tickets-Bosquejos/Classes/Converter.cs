using MySql.Data.MySqlClient;
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
                // Check en caso de que el registro si se tenga un archivo adjunto
                return new BitmapImage(new Uri("C:\\Users\\SISTEMAS\\source\\repos\\Tickets-Bosquejos\\Tickets-Bosquejos\\Images\\check.png"));
 
            }
            else
            {
                // Cross en caso de que el registro no tenga un archivo adjunto
                return new BitmapImage(new Uri("C:\\Users\\SISTEMAS\\source\\repos\\Tickets-Bosquejos\\Tickets-Bosquejos\\Images\\close.png"));
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
