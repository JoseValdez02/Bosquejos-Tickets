using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Tickets_Bosquejos.Styles
{
    public static class UserTicketsStyles
    {

        public static Color Grid;
        public static Color BotonEditar;
        public static Color BotonEliminar;

        //Tema Marino
        private static readonly Color BotonEditarM = Color.FromRgb(139, 63, 60);
        private static readonly Color BotonEliminarM = Color.FromRgb(139, 63, 60);
        private static readonly Color GridM = Color.FromRgb(245, 245, 235);

        //Tema Marino de Jalisco
        private static readonly Color BotonEditarJ = Color.FromRgb(38, 97, 156);
        private static readonly Color BotonEliminarJ = Color.FromRgb(38, 97, 156);
        private static readonly Color GridJ = Color.FromRgb(245, 245, 235);

        //Tema SugarFoods
        private static readonly Color BotonEditarS = Color.FromRgb(82, 183, 136);
        private static readonly Color BotonEliminarS = Color.FromRgb(82, 183, 136);
        private static readonly Color GridS = Color.FromRgb(255, 255, 255);
    }
}
