using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tickets_Bosquejos.Classes
{
    public static class UserSession
    {
        public static int empClave { get; set; }
        public static int usuClave { get; set; }
        public static string usuNombre { get; set; }
        public static string usuPuesto { get; set; }
        public static string empNombre { get; set; }

        public static void Logout()
        {
            empClave = 0;
            usuClave = 0;
            usuNombre = null;
            usuPuesto = null;
            empNombre = null;
        }
    }
}
