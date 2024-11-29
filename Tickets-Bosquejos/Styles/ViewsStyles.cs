using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Tickets_Bosquejos.Classes;

namespace Tickets_Bosquejos.Styles
{
    public static class ViewsStyles
    {
        public static string empEstilo { get; set; }
        public static Color Panel { get; private set; }
        public static Color Menu { get; private set; }
        public static Color Frame { get; private set; }
        public static Color Border { get; private set; }
        public static Color Button { get; private set; }
        public static Color Grid { get; private set; }

        //Tema Marino
        private static readonly Color PanelM = Color.FromRgb(139, 63, 60);
        private static readonly Color MenuM = Color.FromRgb(150, 90, 90);
        private static readonly Color FrameM = Color.FromRgb(245, 245, 235);
        private static readonly Color BorderM = Color.FromRgb(139, 63, 60);
        private static readonly Color ButtonM = Color.FromRgb(139, 63, 60);
        private static readonly Color GridM = Color.FromRgb(245, 245, 235);

        //Tema Marino de Jalisco
        private static readonly Color PanelJ = Color.FromRgb(38, 97, 156);
        private static readonly Color MenuJ = Color.FromRgb(59, 131, 189);
        private static readonly Color FrameJ = Color.FromRgb(245, 245, 235);
        private static readonly Color BorderJ = Color.FromRgb(38, 97, 156);
        private static readonly Color ButtonJ = Color.FromRgb(38, 97, 156);
        private static readonly Color GridJ = Color.FromRgb(245, 245, 235);

        //Tema SugarFoods
        private static readonly Color PanelS = Color.FromRgb(82, 183, 136);
        private static readonly Color MenuS = Color.FromRgb(149, 213, 178);
        private static readonly Color FrameS = Color.FromRgb(255, 255, 255);
        private static readonly Color BorderS = Color.FromRgb(82, 183, 136);
        private static readonly Color ButtonS = Color.FromRgb(82, 183, 136);
        private static readonly Color GridS = Color.FromRgb(255, 255, 255);

        public static void ConfigurarEstilo()
        {
            switch (empEstilo)
            {
                case "Marino":
                    Panel = PanelM;
                    Menu = MenuM;
                    Frame = FrameM;
                    Border = BorderM;
                    Button = ButtonM;
                    Grid = GridM;
                    break;

                case "MarinoJalisco":
                    Panel = PanelJ;
                    Menu = MenuJ;
                    Frame = FrameJ;
                    Border = BorderJ;
                    Button = ButtonJ;
                    Grid = GridJ;
                    break;

                case "SugarFoods":
                    Panel = PanelS;
                    Menu = MenuS;
                    Frame = FrameS;
                    Border = BorderS;
                    Button = ButtonS;
                    Grid = GridS;
                    break;
            }
        }
    }
}
