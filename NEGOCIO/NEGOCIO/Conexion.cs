using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace NEGOCIO
{
    public class Conexion
    {



        public static MySqlConnection obtenerConexion()
        {

            try
            {
                
                MySqlConnection conn;
                string myConnectionString;
                myConnectionString = "server=127.0.0.1;uid=usuarioApp;pwd=usuarioApp1234;database=proyect";
                
                
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                return conn;
            }
            catch (MySqlException ex)
            {
                
                throw new Exception("Error al conectarse a la base: " + ex.Message);
            }

        }





    }
}
