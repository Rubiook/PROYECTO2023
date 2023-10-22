using MySql.Data.MySqlClient;
using NEGOCIO.NEGOCIO;
using System.Collections;

namespace NEGOCIO
{
    public class RepositorioUsuarios
    {

        public ArrayList usuarios;

        public RepositorioUsuarios()
        {
            usuarios = new ArrayList();
            
        }

       

        //BUSCAR USUARIO-----------------------------------------
        public Usuario buscarUsuario(string login)
        {
            foreach (Usuario usr in usuarios)
            {
                if (usr.login.Equals(login))
                {
                    //es una buena practica logear a la consola
                    Console.WriteLine("Usuario existe " + login);
                    return usr;
                }
            }
            //esto lo vamos a mejorar en futuras versiones
            return null;
        }

        //DEVUELVE LOS USUARIOS -----------------------------------------------
        public ArrayList buscarTodos()
        {
            return usuarios;
        }


       


        public Usuario buscarUsuarioPorId(int id)
        {
            foreach (Usuario usr in usuarios)
            {
                if (usr.id == id)
                {

                    return usr;
                }
            }
            //esto lo vamos a mejorar en futuras versiones
            return null;
        }


        public Usuario ObtenerUsuarioPorId(int id)
        {
            Usuario usuario = null;

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                //connection.Open();

                string query = "SELECT id, nombre, apellido, correo, celular FROM usuarios WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                id = Convert.ToInt32(reader["id"]),
                                nombre = Convert.ToString(reader["nombre"]),
                                apellido = Convert.ToString(reader["apellido"]),
                                correo = Convert.ToString(reader["correo"]),
                                celular = Convert.ToString(reader["celular"])
                            };
                        }
                    }
                }
            }

            return usuario;
        }




        /*
        public bool borrarUsuario(int idUsuario)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "DELETE FROM usuarios WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }*/

        public bool borrarUsuario(int idUsuario)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE usuarios SET activo = 0 WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", idUsuario);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                throw new Exception("Error al marcar usuario como inactivo: " + ex.Message);
            }
        }






        private void ReasignarIndices()
        {
            int newId = 1;
            ArrayList nuevosUsuarios = new ArrayList();

            foreach (Usuario usuario in usuarios)
            {
                usuario.id = newId;
                nuevosUsuarios.Add(usuario);
                newId++;
            }

            usuarios = nuevosUsuarios;
        }


        public ArrayList obtenerUsuarios()
        {
            ArrayList usuarios = new ArrayList();

            MySqlConnection conn = null;
            try
            {
                conn = Conexion.obtenerConexion();

                MySqlCommand consultaSql = new MySqlCommand();
                MySqlDataReader resultado;

                consultaSql.CommandText = "SELECT * FROM usuarios WHERE activo = 1;";
                consultaSql.Connection = conn;
                resultado = consultaSql.ExecuteReader();
                while (resultado.Read())
                {
                    int id = resultado.GetInt32(0);
                    string login = resultado.GetString(1);
                    string pass = resultado.GetString(2);
                    string rol = resultado.GetString(3);
                    string nombre = resultado.GetString(4); // Nuevo atributo
                    string apellido = resultado.GetString(5); // Nuevo atributo
                    string direccion = resultado.GetString(6); // Nuevo atributo
                    string correo = resultado.GetString(7); // Nuevo atributo
                    string celular = resultado.GetString(8); // Nuevo atributo

                    usuarios.Add(new Usuario(id, login, pass, rol, nombre, apellido, direccion, correo, celular));
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error al conectarse a la base");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            Console.WriteLine("Fin ejecución obtener usuario");
            return usuarios;
        }

        public bool ExisteUsuario(string login)
        {
            MySqlConnection conn = null;
            bool existe = false;

            try
            {
                conn = Conexion.obtenerConexion();

                MySqlCommand consultaSql = new MySqlCommand();
                consultaSql.CommandText = "SELECT COUNT(*) FROM usuarios WHERE login = @login AND activo = 1";
                consultaSql.Connection = conn;

                consultaSql.Parameters.AddWithValue("@login", login);

                int count = Convert.ToInt32(consultaSql.ExecuteScalar());
                existe = count > 0;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al verificar si el usuario existe: " + ex.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return existe;
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE usuarios SET login = @login, pass = @pass, rol = @rol, nombre = @nombre, apellido = @apellido, direccion = @direccion, correo = @correo, celular = @celular WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", usuario.login);
                    cmd.Parameters.AddWithValue("@pass", usuario.pass);
                    cmd.Parameters.AddWithValue("@rol", usuario.rol);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@direccion", usuario.direccion);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@celular", usuario.celular);
                    cmd.Parameters.AddWithValue("@id", usuario.id);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario: " + ex.Message);
            }
        }

        public bool ModificarContraseña(string nombreUsuario, string nuevaContraseña)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE usuarios SET pass = @pass WHERE login = @login";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@pass", nuevaContraseña);
                    cmd.Parameters.AddWithValue("@login", nombreUsuario);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar contraseña: " + ex.Message);
            }
        }





        public Usuario ObtenerUsuarioPorLogin(string login)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "SELECT * FROM usuarios WHERE login = @login";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@login", login);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Usuario usuario = new Usuario();
                            usuario.id = Convert.ToInt32(reader["id"]);
                            usuario.login = reader["login"].ToString();
                            // Asignar otros campos si es necesario

                            return usuario;
                        }
                        else
                        {
                            return null; // Usuario no encontrado
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener usuario por login: " + ex.Message);
            }
        }







        public int ObtenerUltimoId()
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "SELECT MAX(id) FROM usuarios";
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        return 0; // Si la tabla está vacía, devolver 0
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener el último ID: " + ex.Message);
            }
        }

        public bool AgregarUsuario(Usuario usuario)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    int ultimoId = ObtenerUltimoId();
                    int nuevoId = ultimoId + 1;

                    string query = "INSERT INTO usuarios (id, login, pass, rol, nombre, apellido, direccion, correo, celular) VALUES (@id, @login, @pass, @rol, @nombre, @apellido, @direccion, @correo, @celular)";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@id", nuevoId);
                    cmd.Parameters.AddWithValue("@login", usuario.login);
                    cmd.Parameters.AddWithValue("@pass", usuario.pass);
                    cmd.Parameters.AddWithValue("@rol", usuario.rol);
                    cmd.Parameters.AddWithValue("@nombre", usuario.nombre);
                    cmd.Parameters.AddWithValue("@apellido", usuario.apellido);
                    cmd.Parameters.AddWithValue("@direccion", usuario.direccion);
                    cmd.Parameters.AddWithValue("@correo", usuario.correo);
                    cmd.Parameters.AddWithValue("@celular", usuario.celular);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el usuario: " + ex.Message);
            }
        }


        // VERIFICAR USABILIDAD --*******************************************************
        public Lote ObtenerLotePorId(int loteId)
        {
            Lote lote = null;

            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    connection.Open();

                    string query = "SELECT * FROM tu_tabla_de_lotes WHERE id = @loteId";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@loteId", loteId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lote = new Lote
                            {
                                id = Convert.ToInt32(reader["id"]),
                                proveedor_lote = reader["proveedor_lote"].ToString(),
                                precio_base = Convert.ToInt32(reader["precio_base"]),
                                tipo_de_lote = reader["tipo_de_lote"].ToString(),
                                cantidad_en_lote = Convert.ToInt32(reader["cantidad_en_lote"])
                                // Agrega aquí el resto de las propiedades del lote
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si es necesario
                Console.WriteLine("Error al obtener el lote: " + ex.Message);
            }

            return lote;
        }

        public string ObtenerRolUsuarioPorId(int idUsuario)
        {
            string rolUsuario = string.Empty;

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    string query = "SELECT rol FROM usuarios WHERE id = @IdUsuario";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        //connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            rolUsuario = Convert.ToString(result);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Error al obtener el rol del usuario: " + ex.Message);
                }
            }

            return rolUsuario;
        }



        public void InsertarRemate(Remate remate)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "INSERT INTO remate (fecha, hora_inicio, hora_fin, rematador, tipo_de_remate) " +
                                   "VALUES (@Fecha, @HoraInicio, @HoraFin, @Rematador, @TipoDeRemate)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Fecha", remate.Fecha);
                        command.Parameters.AddWithValue("@HoraInicio", remate.HoraInicio);
                        command.Parameters.AddWithValue("@HoraFin", remate.HoraFin);
                        command.Parameters.AddWithValue("@Rematador", remate.Rematador);
                        command.Parameters.AddWithValue("@TipoDeRemate", remate.TipoDeRemate);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            throw new Exception("No se pudo registrar el remate.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el remate: " + ex.Message);
            }
        }




        public bool ExisteRematador(string rematador)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "SELECT COUNT(*) FROM usuarios WHERE UPPER(rol) = 'REMATADOR' AND UPPER(login) = @Login"; // Cambio aquí
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Login", rematador.ToUpper()); // Cambio aquí

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al verificar el rematador: " + ex.Message);
            }
        }

        public List<Remate> ObtenerRemates(MySqlConnection conn)
        {
            List<Remate> remates = new List<Remate>();

            try
            {
                string query = "SELECT id, fecha, hora_inicio, hora_fin, rematador, tipo_de_remate FROM remate WHERE activo = 1";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Remate remate = new Remate
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Fecha = Convert.ToDateTime(reader["fecha"]),
                                HoraInicio = TimeSpan.Parse(reader["hora_inicio"].ToString()),
                                HoraFin = TimeSpan.Parse(reader["hora_fin"].ToString()),
                                Rematador = reader["rematador"].ToString(),
                                TipoDeRemate = reader["tipo_de_remate"].ToString(), 
                                                                                    
                            };

                            remates.Add(remate);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener los remates: " + ex.Message);
            }

            return remates;
        }

        public bool EliminarRematePorId(int remateId)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "DELETE FROM remate WHERE id = @RemateId";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@RemateId", remateId);
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el remate: " + ex.Message);
            }
        }


        public bool EliminarRematePorFecha(DateTime fechaRemate, MySqlConnection conn, MySqlTransaction transaction)
        {
            try
            {
                string query = "DELETE FROM remate WHERE fecha = @FechaRemate";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Transaction = transaction; // Asignar la transacción al comando
                    command.Parameters.AddWithValue("@FechaRemate", fechaRemate);
                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el remate por fecha: " + ex.Message);
            }
        }
        public bool ModificarRemate(int remateId, string nuevoRematador, DateTime nuevaFecha, TimeSpan nuevaHoraInicio, TimeSpan nuevaHoraFin, string nuevoTipoDeRemate)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE remate SET rematador = @Rematador, fecha = @Fecha, hora_inicio = @HoraInicio, hora_fin = @HoraFin, tipo_de_remate = @TipoDeRemate WHERE id = @RemateId";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Rematador", nuevoRematador);
                        command.Parameters.AddWithValue("@Fecha", nuevaFecha);
                        command.Parameters.AddWithValue("@HoraInicio", nuevaHoraInicio);
                        command.Parameters.AddWithValue("@HoraFin", nuevaHoraFin);
                        command.Parameters.AddWithValue("@TipoDeRemate", nuevoTipoDeRemate);
                        command.Parameters.AddWithValue("@RemateId", remateId);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al modificar el remate: " + ex.Message);
            }
        }



        public void InsertarLote(Lote lote)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "INSERT INTO lote (proveedor_lote, precio_base, tipo_de_lote, cantidad_en_lote, descripcion, imagen) " +
                                   "VALUES (@ProveedorLote, @PrecioBase, @TipoDeLote, @CantidadEnLote, @Descripcion, @Imagen)";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ProveedorLote", lote.proveedor_lote);
                        command.Parameters.AddWithValue("@PrecioBase", lote.precio_base);
                        command.Parameters.AddWithValue("@TipoDeLote", lote.tipo_de_lote);
                        command.Parameters.AddWithValue("@CantidadEnLote", lote.cantidad_en_lote);
                        command.Parameters.AddWithValue("@Descripcion", lote.descripcion);
                        command.Parameters.AddWithValue("@Imagen", lote.imagen);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected <= 0)
                        {
                            throw new Exception("No se pudo registrar el lote.");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar el lote: " + ex.Message);
            }
        }



        public List<Lote> ObtenerLotes()
        {
            List<Lote> lotes = new List<Lote>();

            MySqlConnection conn = null;
            try
            {
                conn = Conexion.obtenerConexion();

                MySqlCommand consultaSql = new MySqlCommand();
                MySqlDataReader resultado;

                consultaSql.CommandText = "SELECT * FROM lote WHERE activo = 1;";
                consultaSql.Connection = conn;
                resultado = consultaSql.ExecuteReader();
                while (resultado.Read())
                {
                    int id = resultado.GetInt32("id");
                    string proveedorLote = resultado.GetString("proveedor_lote");
                    int precioBase = resultado.GetInt32("precio_base");
                    string tipoLote = resultado.GetString("tipo_de_lote");
                    int cantidadEnLote = resultado.GetInt32("cantidad_en_lote");
                    string descripcion = resultado.GetString("descripcion");
                    // No necesitamos recuperar la columna de imagen en este punto

                    lotes.Add(new Lote
                    {
                        id = id,
                        proveedor_lote = proveedorLote,
                        precio_base = precioBase,
                        tipo_de_lote = tipoLote,
                        cantidad_en_lote = cantidadEnLote,
                        descripcion = descripcion
                        // No necesitamos agregar la imagen en este punto
                    });
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error al conectarse a la base");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            Console.WriteLine("Fin ejecución obtener lotes");
            return lotes;
        }

        public byte[] ObtenerImagenLote(int loteId)
        {
            byte[] imagenBytes = null;

            MySqlConnection conn = null;
            try
            {
                conn = Conexion.obtenerConexion();

                MySqlCommand consultaSql = new MySqlCommand();
                consultaSql.CommandText = "SELECT imagen FROM lote WHERE id = @loteId;";
                consultaSql.Parameters.AddWithValue("@loteId", loteId);
                consultaSql.Connection = conn;

                object resultado = consultaSql.ExecuteScalar();
                if (resultado != null && resultado != DBNull.Value)
                {
                    imagenBytes = (byte[])resultado;
                }
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Error al conectarse a la base");
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return imagenBytes;
        }


        public bool EliminarLotePorId(int loteId)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE lote SET activo = 0 WHERE id = @LoteId";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@LoteId", loteId);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el lote: " + ex.Message);
            }
        }

        public bool ExisteProveedorVendedor(string proveedor)
        {
            using (MySqlConnection conn = Conexion.obtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM usuarios WHERE UPPER(login) = @Proveedor AND rol = 'VENDEDOR'";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Proveedor", proveedor.ToUpper()); // Convertir a mayúsculas

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }
        public bool ExisteComprador(string comprador)
        {
            using (MySqlConnection conn = Conexion.obtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM usuarios WHERE UPPER(login) = @Comprador AND rol = 'COMPRADOR'";

                using (MySqlCommand command = new MySqlCommand(query, conn))
                {
                    command.Parameters.AddWithValue("@Comprador", comprador.ToUpper()); // Convertir a mayúsculas

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }


        public bool ModificarLote(Lote loteModificado)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "UPDATE lote SET proveedor_lote = @NuevoProveedor, precio_base = @NuevoPrecioBase, tipo_de_lote = @NuevoTipoDeLote, cantidad_en_lote = @NuevaCantidadEnLote, descripcion = @NuevaDescripcion WHERE id = @Id";

                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@NuevoProveedor", loteModificado.proveedor_lote);
                        command.Parameters.AddWithValue("@NuevoPrecioBase", loteModificado.precio_base);
                        command.Parameters.AddWithValue("@NuevoTipoDeLote", loteModificado.tipo_de_lote);
                        command.Parameters.AddWithValue("@NuevaCantidadEnLote", loteModificado.cantidad_en_lote);
                        command.Parameters.AddWithValue("@NuevaDescripcion", loteModificado.descripcion);
                        command.Parameters.AddWithValue("@Id", loteModificado.id);

                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected > 0; // Devuelve true si al menos una fila fue modificada
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al modificar el lote: " + ex.Message);
            }
        }


        public bool ExisteRemateEnFecha(DateTime fecha)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM remate WHERE fecha = @fecha AND activo = 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@fecha", fecha);

                    //connection.Open();

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    return count > 0;
                }
            }
        }




        //fin-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    }

} 
