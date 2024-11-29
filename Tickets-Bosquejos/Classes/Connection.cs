using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Tickets_Bosquejos.Classes
{
    public static class Connection
    {
        private static readonly string connectionString = "server=127.0.0.1;port=3307;database=tickets;user=root;password=marino;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
