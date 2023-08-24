using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


namespace PERSISTENCIA.PERSISTENCIA
{
    internal class Conexion
    {

        public static MySqlConnection obtenerConexion()
        {

            try
            {
                MySqlConnection conn;
                string myConnectionString;

                myConnectionString = "server=127.0.0.1;uid=usuarioApp;pwd=root;database=misegundadatabase";
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al conextarce a la base");
                return null;
            }

        }





    }
}
