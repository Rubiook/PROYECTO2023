using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;


namespace NEGOCIO
{

  

    public class Usuario
    {
        public int id { get; set; }
        public string login { get; set; }
        public string pass { get; set; }
        public string rol { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string correo { get; set; }
        public string celular { get; set; }




        private string passwordHash;
       // private Login loginInstance; // Campo para la instancia de Login
        //private static RepositorioUsuarios repo = new RepositorioUsuarios();

        private RepositorioUsuarios repoUsuarios; // Campo para el RepositorioUsuarios

        // Constructor
        public Usuario(int id, string login, string pass, string rol, string nombre, string apellido, string direccion, string correo, string celular)
        {
            this.id = id;
            this.login = login;
            this.pass = pass;
            this.rol = rol;
            this.nombre = nombre;
            this.apellido = apellido;
            this.direccion = direccion;
            this.correo = correo;
            this.celular = celular;
        }
        public Usuario(int id, string login, string pass, string rol, RepositorioUsuarios repoUsuarios)
        {
            this.id = id;
            this.login = login;
            this.pass = pass;
            this.rol = rol;
            this.repoUsuarios = repoUsuarios; // Inicializamos el campo repoUsuarios
            //this.loginInstance = loginInstance; // Inicializamos el campo loginInstance
        }

        public Usuario(int id, string login, string pass, string rol)
        {

            this.id = id;
            this.login = login;
            this.pass = pass;
            this.rol = rol;
            

        }
        public Usuario(int id, string login, string pass)
        {
            this.id = id;
            this.login = login;
            this.pass = pass;
        }

        public Usuario(string login, string pass, string rol)
        {
            this.login = login;
            this.pass = pass;
            this.rol = rol;


        }
        
        
        public Usuario(string login, string pass)
        {
            this.login = login;
            this.pass = pass;
            


        }

        public Usuario(int id)
        {
            this.id = id;
        }

        public Usuario()
        {
        }
        public string ObtenerLoginUsuarioActual()
        {
            return this.login;
        }
        public string ObtenerNombreUsuarioActual()
        {
            return this.nombre;
        }

        /*//GUARDAR USUARIOS------------------------------------------------------------
        public void GuardarUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "INSERT INTO usuarios (login, pass, rol) VALUES (@login, @pass, @rol)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", usuario.login);
                        cmd.Parameters.AddWithValue("@pass", usuario.pass);
                        cmd.Parameters.AddWithValue("@rol", usuario.rol);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al guardar el usuario: " + ex.Message);
            }

        }
        */





        public bool passwordCorrecta(string passAvalidar)
        {
            return pass.Equals(passAvalidar) ? true : false;
            /* es lo mismo que esto
            if (pass.Equals(passAvalidar))
            {
                return true;
            } else
            {
                return false;
            }
            */
        }

    

    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Convertir la contraseña en un arreglo de bytes
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Convertir el arreglo de bytes en una cadena hexadecimal
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

        
    public ArrayList obtenerUsuariosExistentes()
        {
            ArrayList usuarios = new ArrayList();

            //cargo filas a la grilla
            foreach (Usuario usr in repoUsuarios.buscarTodos())
            {
                usuarios.Add(usr);
            }
            return usuarios;
        }
        
        //profeeeee-------------------
        public static ArrayList obtenerUsuarios()
        {
            ArrayList usuariosObtenidos = new ArrayList();

            RepositorioUsuarios repoUsr = new RepositorioUsuarios();
            foreach (Usuario usr in repoUsr.obtenerUsuarios())
            {
                usuariosObtenidos.Add(new Usuario(
                                                usr.id,
                                                usr.login,
                                                usr.pass,
                                                usr.rol));
            }

            return usuariosObtenidos;
        }
        /*
        public override string ToString()
        {
            return this.id + " " + this.login + " " + this.pass + " " + this.rol;
        }
        */
        //profeee----------------------


        public bool borrarUsuario()
        {
            return repoUsuarios.borrarUsuario(this.id);
        }



        public static Usuario ObtenerUsuarioPorId(int id)
        {
            //obtener usuario por ID ,llamar método buscarUsuarioPorId(id)
            RepositorioUsuarios repositorio = new RepositorioUsuarios();
            return repositorio.buscarUsuarioPorId(id);
        }


        /*MODIFICAR USUARIO
        public bool ModificarUsuario(string login, string contrasenia, string rol)
        {
            Usuario usuario = repo.buscarUsuarioPorId(id);

            if (usuario != null)
            {
                login = login.ToUpper();
                usuario.login = login;
                usuario.pass = contrasenia;
                usuario.rol = rol;

                // Guardar los cambios en el repositorio
               

                return true; // Indicar que la modificación fue exitosa
            }

            return false; // Indicar que no se encontró el usuario con el ID especificado
        }*/

        public bool ModificarUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE usuarios SET login = @login, pass = @pass, rol = @rol WHERE id = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@login", usuario.login);
                        cmd.Parameters.AddWithValue("@pass", usuario.pass);
                        cmd.Parameters.AddWithValue("@rol", usuario.rol);
                        cmd.Parameters.AddWithValue("@id", usuario.id);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al modificar el usuario: " + ex.Message);
            }
        }

        
        public bool EliminarUsuario()
        {
            return repoUsuarios.borrarUsuario(this.id);
        }



        //fin-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    }


}
