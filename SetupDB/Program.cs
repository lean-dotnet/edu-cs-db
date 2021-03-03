using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace SetupDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Setting up Northwind Database in LocalDB Instance ...");

            using(SqlConnection conn = new SqlConnection(Properties.Settings.Default.LocalDB))
            {
                string script = File.ReadAllText("instnwnd.sql");

                ServerConnection serverConn = new ServerConnection(conn);
                Server server = new Server(serverConn);
                server.ConnectionContext.ExecuteNonQuery(script);
            }

            Console.WriteLine("Done.");
            SqlConnectionStringBuilder csb = new SqlConnectionStringBuilder(Properties.Settings.Default.LocalDB);
            csb.InitialCatalog = "Northwind";
            Console.WriteLine("Connect to the Northwind DB with the following connection string:");
            Console.WriteLine(csb.ConnectionString);
        }
    }
}
