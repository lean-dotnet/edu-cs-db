using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connecting
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Vanilla way - ignoring all best pratices

            Console.WriteLine("Vanilla way - ignoring all best pratices" + Environment.NewLine);

            // Verbindungszeichenfolge Connection-String
            string connStr = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";

            Console.WriteLine("Creating connection object ...");
            SqlConnection connObj = new SqlConnection(connStr);
            Console.WriteLine("State: " + connObj.State.ToString());

            Console.WriteLine("Connecting to database ...");
            connObj.Open();
            Console.WriteLine("State: " + connObj.State.ToString());

            Console.WriteLine("And closing ...");
            connObj.Close();
            Console.WriteLine("State: " + connObj.State.ToString());
            Console.WriteLine();

            Console.WriteLine("Any key to continue ... ");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine + new string('-', 80) + Environment.NewLine);

            #endregion

            #region Legacy loading of config - but still ignoring disposable pattern 

            Console.WriteLine("Legacy loading of config - but still ignoring disposable pattern." + Environment.NewLine);

            // Alter Standard-Weg, um Konfigurationsdatei (ConnectionStrings) auszulesen
            connStr = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

            connObj = new SqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to database ...");
                connObj.Open();
                Console.WriteLine("State: " + connObj.State.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Something went wrong while opening: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("and closing ...");
                try
                {
                    connObj.Close();
                    Console.WriteLine("State: " + connObj.State.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong while closing: " + ex.Message);
                }

            }
            Console.WriteLine();

            Console.WriteLine("Any key to continue ... ");
            Console.ReadKey();
            Console.WriteLine(Environment.NewLine + new string('-', 80) + Environment.NewLine);

            #endregion

            #region Best practices: Disposable pattern and settings

            Console.WriteLine("Best practices: Disposable pattern and settings." + Environment.NewLine);

            // (Default-Value in den Settings, änderbar in der App.config)
            connStr = Properties.Settings.Default.NorthwindConnectionString;

            // Using (impliziert close-Aufruf in einer finally-Klausel)
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    Console.WriteLine("Connecting to database ...");
                    conn.Open();
                    Console.WriteLine("State: " + conn.State.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong while opening: " + ex.Message);
                }
                Console.WriteLine("and closing ...");
            }
            #endregion
        }
    }
}
