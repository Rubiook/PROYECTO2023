using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using NEGOCIO;







namespace NEGOCIO
{
    public class Login
    {

        private RepositorioUsuarios repositorioUsuarios;
        //public Usuario usuarioActual; //? permite valores null
        private string rolUsuarioActual;
        private string nombreUsuarioActual;


        //private string nombreUsuarioActual;
        public Usuario UsuarioActual { get; set; } // Propiedad para el usuario actual

        public Login()
        {
            this.repositorioUsuarios = new RepositorioUsuarios();
        }


        public string ObtenerLoginUsuarioActual()
        {
            if (UsuarioActual != null)
            {
                return UsuarioActual.login;
            }
            else
            {
                return "USR"; // O cualquier valor por defecto que desees en caso de que no haya un usuario actual
            }
        }
        public string ObtenerCorreoUsuarioActual()
        {
            if (UsuarioActual != null)
            {
                return UsuarioActual.correo;
            }
            return null;
        }



        /* public int ObtenerIdUsuarioActual()
         {
             if (UsuarioActual != null)
             {
                 return UsuarioActual.id;
             }
             else
             {
                 // Retorna un valor apropiado en caso de no haber usuario actual
                 return -1; // o 0 u otro valor según tu lógica
             }
         }
         */
        public string ObtenerRolUsuarioActual()
        {
            return this.rolUsuarioActual;
        }
        public string ObtenerNombreUsuarioActual()
        {
            return this.nombreUsuarioActual;
        }



        //METODO AUTENTICAR --------------------------------------------------------------------
        public bool Autenticar(Usuario usr)
        {
            MySqlConnection conn = null;

            try
            {
                conn = Conexion.obtenerConexion();

                MySqlCommand consultaSql = new MySqlCommand();
                consultaSql.CommandText = "SELECT rol, nombre, apellido, direccion, correo, celular FROM usuarios WHERE login = @login AND pass = @pass";
                consultaSql.Parameters.AddWithValue("@login", usr.login);
                consultaSql.Parameters.AddWithValue("@pass", usr.pass);
                consultaSql.Connection = conn;

                MySqlDataReader resultado = consultaSql.ExecuteReader();

                if (resultado.Read())
                {
                    this.rolUsuarioActual = resultado.GetString("rol"); // Actualiza el rol
                    this.nombreUsuarioActual = resultado.GetString("nombre"); // Actualiza el nombre

                    // Actualiza los demás datos
                    this.UsuarioActual = new Usuario
                    {
                        login = usr.login,
                        pass = usr.pass,
                        rol = this.rolUsuarioActual,
                        nombre = resultado.GetString("nombre"),
                        apellido = resultado.GetString("apellido"),
                        direccion = resultado.GetString("direccion"),
                        correo = resultado.GetString("correo"),
                        celular = resultado.GetString("celular")
                    };

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException e)
            {
                // Manejar el error de conexión o consulta aquí
                Console.WriteLine("Error al conectar a la base de datos: " + e.Message);
                return false;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

      



        //fin-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    }
}
