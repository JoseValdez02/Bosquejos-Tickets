using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tickets_Bosquejos.Styles
{
    public static class UserTicketsFormStyles
    {
        public static Color Grid;
        public static Color Border;
        public static Color Button;

        //Tema Marino
        private static readonly Color BorderM = Color.FromRgb(139, 63, 60);
        private static readonly Color ButtonM = Color.FromRgb(139, 63, 60);
        private static readonly Color GridM = Color.FromRgb(245, 245, 235);

        //Tema Marino de Jalisco
        private static readonly Color BorderJ = Color.FromRgb(38, 97, 156);
        private static readonly Color ButtonJ = Color.FromRgb(38, 97, 156);
        private static readonly Color GridJ = Color.FromRgb(245, 245, 235);

        //Tema SugarFoods
        private static readonly Color BorderS = Color.FromRgb(82, 183, 136);
        private static readonly Color ButtonS = Color.FromRgb(82, 183, 136);
        private static readonly Color GridS = Color.FromRgb(255, 255, 255);
    }
}
