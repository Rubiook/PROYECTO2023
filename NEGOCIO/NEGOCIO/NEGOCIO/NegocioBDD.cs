using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class NegocioBDD
    {
        private readonly RepositorioUsuarios repositorioUsuarios;

        private MySqlConnection _connection;
        public NegocioBDD()
        {
            repositorioUsuarios = new RepositorioUsuarios();
            _connection = Conexion.obtenerConexion();
        }

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        /* public List<Lote> ObtenerLotesAsignados(int remateId)
         {
             List<Lote> lotesAsignados = new List<Lote>();

             using (MySqlConnection conn = Conexion.obtenerConexion())
             {
                 try
                 {
                     string query = "SELECT * FROM Lote WHERE remate_id = @RemateId";
                     using (MySqlCommand cmd = new MySqlCommand(query, conn))
                     {
                         cmd.Parameters.AddWithValue("@RemateId", remateId);

                         conn.Open();
                         using (MySqlDataReader reader = cmd.ExecuteReader())
                         {
                             while (reader.Read())
                             {
                                 Lote lote = new Lote
                                 {
                                     id = Convert.ToInt32(reader["id"]),
                                     proveedor_lote = reader["proveedor_lote"].ToString(),
                                     precio_base = Convert.ToInt32(reader["precio_base"]),
                                     tipo_animal = reader["tipo_animal"].ToString(),
                                     cantidad_animales = Convert.ToInt32(reader["cantidad_animales"])
                                 };
                                 lotesAsignados.Add(lote);
                             }
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     // Manejo de excepciones
                 }
             }

             return lotesAsignados;
         }*/
        //------------------------------------------------------------------
        /*
        public void AsignarLoteARemate(int idRemate, int idLote)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    // Insertar en la tabla lotesAsignados
                    string insertQuery = "INSERT INTO lotesasignados (id_remate, id_lote) VALUES (@RemateId, @LoteId)";

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@RemateId", idRemate);
                        insertCommand.Parameters.AddWithValue("@LoteId", idLote);

                        insertCommand.ExecuteNonQuery();
                    }

                    // Actualizar el campo RemateId en la tabla lotes
                    string updateQuery = "UPDATE lote SET id_remate = @RemateId WHERE id = @LoteId";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@RemateId", idRemate);
                        updateCommand.Parameters.AddWithValue("@LoteId", idLote);

                        updateCommand.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Puedes manejar el error según tu lógica de negocio, por ejemplo, registrar en un log.
                    throw new Exception("Error al asignar lote: " + ex.Message);
                }
            }
        }

        public void DesasignarLoteDeRemate(int loteId)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Primero, elimina el registro de lotesasignados
                    string deleteQuery = "DELETE FROM lotesasignados WHERE id_lote = @LoteId";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@LoteId", loteId);
                        int rowsDeleted = deleteCommand.ExecuteNonQuery();

                        if (rowsDeleted > 0)
                        {
                            // Si se eliminó el registro en lotesasignados, actualiza la columna id_remate en la tabla lote
                            string updateQuery = "UPDATE lote SET id_remate = NULL WHERE id = @LoteId";

                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@LoteId", loteId);
                                int rowsUpdated = updateCommand.ExecuteNonQuery();

                                if (rowsUpdated > 0)
                                {
                                    Console.WriteLine($"El lote {loteId} ha sido desasignado del remate y la columna id_remate en la tabla lote ha sido actualizada.");
                                }
                            }
                        }
                        else
                        {
                            // Si no se eliminó ningún registro, significa que el lote no estaba asignado a ningún remate.
                            Console.WriteLine($"El lote {loteId} no estaba asignado a ningún remate.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desasignar el lote: " + ex.Message);
            }
        }
        */




        public void AsignarLoteARemate(int idRemate, int idLote)
        {
            try
            {
                // Insertar en la tabla lotesAsignados
                string insertQuery = "INSERT INTO lotesasignados (id_remate, id_lote) VALUES (@RemateId, @LoteId)";

                using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, _connection))
                {
                    insertCommand.Parameters.AddWithValue("@RemateId", idRemate);
                    insertCommand.Parameters.AddWithValue("@LoteId", idLote);

                    insertCommand.ExecuteNonQuery();
                }

                // Actualizar el campo RemateId en la tabla lotes
                string updateQuery = "UPDATE lote SET id_remate = @RemateId WHERE id = @LoteId";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, _connection))
                {
                    updateCommand.Parameters.AddWithValue("@RemateId", idRemate);
                    updateCommand.Parameters.AddWithValue("@LoteId", idLote);

                    updateCommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                // Puedes manejar el error según tu lógica de negocio, por ejemplo, registrar en un log.
                throw new Exception("Error al asignar lote: " + ex.Message);
            }
        }

        public void DesasignarLoteDeRemate(int loteId)
        {
            try
            {
                // Primero, elimina el registro de lotesasignados
                string deleteQuery = "DELETE FROM lotesasignados WHERE id_lote = @LoteId";

                using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, _connection))
                {
                    deleteCommand.Parameters.AddWithValue("@LoteId", loteId);
                    int rowsDeleted = deleteCommand.ExecuteNonQuery();

                    if (rowsDeleted > 0)
                    {
                        // Si se eliminó el registro en lotesasignados, actualiza la columna id_remate en la tabla lote
                        string updateQuery = "UPDATE lote SET id_remate = NULL WHERE id = @LoteId";

                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, _connection))
                        {
                            updateCommand.Parameters.AddWithValue("@LoteId", loteId);
                            int rowsUpdated = updateCommand.ExecuteNonQuery();

                            if (rowsUpdated > 0)
                            {
                                Console.WriteLine($"El lote {loteId} ha sido desasignado del remate y la columna id_remate en la tabla lote ha sido actualizada.");
                            }
                        }
                    }
                    else
                    {
                        // Si no se eliminó ningún registro, significa que el lote no estaba asignado a ningún remate.
                        Console.WriteLine($"El lote {loteId} no estaba asignado a ningún remate.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al desasignar el lote: " + ex.Message);
            }
        }



        public List<Lote> ObtenerLotesAsignadosPorRemate(int remateId)
        {
            List<Lote> lotesAsignados = new List<Lote>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT l.id, l.proveedor_lote, l.precio_base, l.tipo_de_lote, l.cantidad_en_lote, l.descripcion, l.imagen " +
                               "FROM lote l " +
                               "INNER JOIN lotesasignados la ON l.id = la.id_lote " +
                               "WHERE la.id_remate = @RemateId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RemateId", remateId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int loteId = Convert.ToInt32(reader["id"]);
                            string proveedor = Convert.ToString(reader["proveedor_lote"]);
                            int precioBase = (int)reader["precio_base"];
                            string tipoLote = Convert.ToString(reader["tipo_de_lote"]);
                            int cantidadEnLote = Convert.ToInt32(reader["cantidad_en_lote"]);
                            string descripcionLote = Convert.ToString(reader["descripcion"]);
                            byte[] imagenData = reader["imagen"] as byte[]; // Obtener los datos de la imagen
                           
                            Console.WriteLine($"DescripcionLote: {descripcionLote}");

                            // Crear un objeto Lote usando propiedades públicas o un constructor adecuado
                            Lote lote = new Lote
                            {
                                id = loteId,
                                proveedor_lote = proveedor,
                                precio_base = precioBase,
                                tipo_de_lote = tipoLote,
                                cantidad_en_lote = cantidadEnLote,
                                descripcion = descripcionLote,
                                imagen = imagenData // Asignar los datos de la imagen al objeto Lote
                            };

                            lotesAsignados.Add(lote);
                        }
                    }
                }
            }

            return lotesAsignados;
        }


        

        public int ObtenerCantidadLotesAsignados(int remateId)
        {
            int cantidadLotesAsignados = 0;

            try
            {
                using (MySqlConnection conexion = Conexion.obtenerConexion())
                {
                    string consulta = "SELECT COUNT(*) FROM lotesasignados WHERE id_remate = @remateId";
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@remateId", remateId);
                        //conexion.Open();
                        cantidadLotesAsignados = Convert.ToInt32(comando.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                // Aquí puedes manejar la excepción según tus necesidades.
                // Por ejemplo, puedes lanzar una excepción personalizada.
                throw new Exception("Error al obtener la cantidad de lotes asignados.", ex);
            }

            return cantidadLotesAsignados;
        }

        public void EliminarLotesAsignados(int remateId)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    // Eliminar los lotes asignados al remate
                    string deleteQuery = "DELETE FROM lotesasignados WHERE id_remate = @RemateId";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@RemateId", remateId);

                        deleteCommand.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Puedes manejar el error según tu lógica de negocio, por ejemplo, registrar en un log.
                    throw new Exception("Error al eliminar lotes asignados: " + ex.Message);
                }
            }
        }

        

        public List<Lote> ObtenerLotesPorFecha(DateTime fecha)
        {
            List<Lote> lotesPorFecha = new List<Lote>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT l.id, l.proveedor_lote, l.precio_base, l.tipo_de_lote, l.cantidad_en_lote, l.descripcion, l.imagen " +
                               "FROM lote l " +
                               "INNER JOIN lotesasignados la ON l.id = la.id_lote " +
                               "INNER JOIN remate r ON la.id_remate = r.id " +
                               "WHERE DATE(r.fecha) = @Fecha";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Fecha", fecha.Date);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int loteId = Convert.ToInt32(reader["id"]);
                            string proveedor = Convert.ToString(reader["proveedor_lote"]);
                            int precioBase = (int)reader["precio_base"];
                            string tipoLote = Convert.ToString(reader["tipo_de_lote"]);
                            int cantidadEnLote = Convert.ToInt32(reader["cantidad_en_lote"]);
                            string descripcionLote = Convert.ToString(reader["descripcion"]);
                            byte[] imagenData = reader["imagen"] as byte[]; // Obtener los datos de la imagen

                            // Crear un objeto Lote usando propiedades públicas o un constructor adecuado
                            Lote lote = new Lote
                            {
                                id = loteId,
                                proveedor_lote = proveedor,
                                precio_base = precioBase,
                                tipo_de_lote = tipoLote,
                                cantidad_en_lote = cantidadEnLote,
                                descripcion = descripcionLote,
                                imagen = imagenData // Asignar los datos de la imagen al objeto Lote
                            };

                            lotesPorFecha.Add(lote);
                        }
                    }
                }
            }

            return lotesPorFecha;
        }

        public Lote ObtenerLotePorId(int loteId)
        {
            Lote lote = null;

            try
            {
                // No necesitas usar "using" aquí ya que el método ya está manejando la conexión.
                // Además, no necesitas abrir la conexión manualmente ya que está siendo manejada en los métodos que llaman a este.
                // using (MySqlConnection connection = Conexion.obtenerConexion())
                // {
                string query = "SELECT * FROM lote WHERE id = @LoteId";
                MySqlCommand command = new MySqlCommand(query, _connection); // Usar la instancia de conexión existente
                command.Parameters.AddWithValue("@LoteId", loteId);

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
                            cantidad_en_lote = Convert.ToInt32(reader["cantidad_en_lote"]),
                            id_remate = Convert.ToInt32(reader["id_remate"]),
                            descripcion = reader["descripcion"].ToString(),
                            imagen = reader["imagen"] as byte[]
                        };
                    }
                }
                // } No necesitas cerrar la conexión manualmente aquí, ya que está siendo manejada en los métodos que llaman a este.
            }
            catch (Exception ex)
            {
                // Manejo de excepciones si es necesario
                Console.WriteLine("Error al obtener el lote: " + ex.Message);
            }

            return lote;
        }



        public bool VerificarLoteAsignado(int loteId)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM lotesasignados WHERE id_lote = @LoteId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoteId", loteId);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0; // Retorna true si el lote está asignado, false si no lo está
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Error al verificar lote asignado: " + ex.Message);
                }
            }
        }
        public bool VerificarLoteVendido(int loteId)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM lotesvendidos WHERE id_lote = @LoteId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoteId", loteId);
                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0; // Retorna true si el lote está asignado, false si no lo está
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Error al verificar lote asignado: " + ex.Message);
                }
            }
        }


        public LoteVendido ObtenerVentaPorId(int ventaId)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    string query = "SELECT lv.id, lv.id_remate, lv.id_lote, lv.fecha_venta, lv.id_preventa, pv.precio_de_venta " +
                                   "FROM lotesvendidos lv " +
                                   "INNER JOIN preeventas pv ON lv.id_preventa = pv.id " +
                                   "WHERE lv.id = @VentaId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@VentaId", ventaId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                LoteVendido venta = new LoteVendido
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    IdRemate = Convert.ToInt32(reader["id_remate"]),
                                    IdLote = Convert.ToInt32(reader["id_lote"]),
                                    FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                                    id_preventa = Convert.ToInt32(reader["id_preventa"]),
                                    PrecioDeVenta = Convert.ToInt32(reader["precio_de_venta"])
                                    // Agrega otros atributos según tu estructura de datos
                                };

                                return venta;
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener detalles de la venta: " + ex.Message);
            }

            return null; // Retorna null si no se encuentra la venta con el ID especificado
        }







        /*
                public List<LoteVendido> ObtenerLotesVendidos()
                {
                    List<LoteVendido> lotesVendidos = new List<LoteVendido>();

                    using (MySqlConnection connection = Conexion.obtenerConexion())
                    {
                        string query = "SELECT lv.id, lv.id_remate, lv.id_lote, lv.fecha_venta, lv.proveedor, lv.comprador, lv.precio_de_venta, " +
                                       "l.proveedor_lote, l.precio_base, l.tipo_de_lote, l.cantidad_en_lote, l.descripcion, " +
                                       "lv.id_usuario_comprador " + 
                                       "FROM lotesvendidos lv " +
                                       "INNER JOIN lote l ON lv.id_lote = l.id";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    LoteVendido loteVendido = new LoteVendido
                                    {
                                        Id = Convert.ToInt32(reader["id"]),
                                        IdRemate = Convert.ToInt32(reader["id_remate"]),
                                        IdLote = Convert.ToInt32(reader["id_lote"]),
                                        FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                                        Proveedor = reader["proveedor"].ToString(),
                                        Comprador = reader["comprador"].ToString(),
                                        PrecioDeVenta = Convert.ToInt32(reader["precio_de_venta"]),
                                        ProveedorLote = reader["proveedor_lote"].ToString(),
                                        PrecioBase = Convert.ToInt32(reader["precio_base"]),
                                        TipoDeLote = reader["tipo_de_lote"].ToString(),
                                        CantidadEnLote = Convert.ToInt32(reader["cantidad_en_lote"]),
                                        Descripcion = reader["descripcion"].ToString(),
                                        IdUsuarioComprador = Convert.ToInt32(reader["id_usuario_comprador"]) 
                                    };

                                    lotesVendidos.Add(loteVendido);
                                }
                            }
                        }
                    }

                    return lotesVendidos;
                }*/

        public List<LoteVendido> ObtenerLotesVendidos()
        {
            List<LoteVendido> lotesVendidos = new List<LoteVendido>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = @"
            SELECT lv.id, lv.id_remate, lv.id_lote, lv.fecha_venta, p.id_usuario, p.precio_de_venta,
                   l.proveedor_lote, l.precio_base, l.tipo_de_lote, l.cantidad_en_lote, l.descripcion
            FROM lotesvendidos lv
            INNER JOIN preeventas p ON lv.id_preventa = p.id
            INNER JOIN lote l ON lv.id_lote = l.id";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            LoteVendido loteVendido = new LoteVendido
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                IdRemate = Convert.ToInt32(reader["id_remate"]),
                                IdLote = Convert.ToInt32(reader["id_lote"]),
                                FechaVenta = Convert.ToDateTime(reader["fecha_venta"]),
                                IdUsuarioComprador = Convert.ToInt32(reader["id_usuario"]),
                                PrecioDeVenta = Convert.ToInt32(reader["precio_de_venta"]),
                                ProveedorLote = reader["proveedor_lote"].ToString(),
                                PrecioBase = Convert.ToInt32(reader["precio_base"]),
                                TipoDeLote = reader["tipo_de_lote"].ToString(),
                                CantidadEnLote = Convert.ToInt32(reader["cantidad_en_lote"]),
                                Descripcion = reader["descripcion"].ToString()
                            };

                            lotesVendidos.Add(loteVendido);
                        }
                    }
                }
            }

            return lotesVendidos;
        }







        public int ObtenerRemateIdPorLote(int loteId)
        {
            int remateId = -1; // Valor por defecto si no se encuentra el remate

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT id_remate FROM lotesasignados WHERE id_lote = @LoteId";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LoteId", loteId);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        remateId = Convert.ToInt32(result);
                    }
                }
            }

            return remateId;
        }

       

    public void EliminarRemate(int remateId)
    {
        using (MySqlConnection connection = Conexion.obtenerConexion())
        {
            try
            {
                string deleteQuery = "UPDATE remate SET activo = 0 WHERE id = @RemateId";

                using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                {
                    deleteCommand.Parameters.AddWithValue("@RemateId", remateId);
                    deleteCommand.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el remate: " + ex.Message);
            }
        }
    }


        public bool LoteYaAsignado(int loteId)
        {
            return VerificarLoteAsignado(loteId);
        }

        public List<Remate> ObtenerRematesOrdenadosPorFecha()
        {
            List<Remate> remates = new List<Remate>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT id, fecha, tipo_de_remate FROM remate WHERE activo = 1 ORDER BY fecha ";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int remateId = Convert.ToInt32(reader["id"]);
                            DateTime fechaRemate = Convert.ToDateTime(reader["fecha"]);
                            string tipoRemate = Convert.ToString(reader["tipo_de_remate"]);

                            Remate remate = new Remate
                            {
                                Id = remateId,
                                Fecha = fechaRemate,
                                TipoDeRemate = tipoRemate
                            };

                            remates.Add(remate);
                        }
                    }
                }
            }

            return remates;
        }

        public int ObtenerIdLotePrevendidoPorIdLote(int idLote)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    string obtenerIdLoteVendidoQuery = @"SELECT id FROM lotesvendidos WHERE id_lote = @IdLote";

                    using (MySqlCommand command = new MySqlCommand(obtenerIdLoteVendidoQuery, connection))
                    {
                        command.Parameters.AddWithValue("@IdLote", idLote);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Convert.ToInt32(result);
                        }
                        else
                        {
                            // Si no se encuentra un registro correspondiente, puedes manejarlo como desees.
                            // Podrías devolver un valor predeterminado, lanzar una excepción, etc.
                            return -1; // Por ejemplo, devolviendo -1 para indicar que no se encontró el registro.
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener el ID de lote vendido: " + ex.Message);
            }
        }


        /*  public void MarcarLoteComoVendido(LoteVendido loteVendido)
          {
              using (MySqlConnection connection = Conexion.obtenerConexion())
              {
                  try
                  {
                      string insertQuery = "INSERT INTO lotesvendidos (id_remate, id_lote, fecha_venta, proveedor, comprador, precio_de_venta, id_usuario_comprador) VALUES (@RemateId, @LoteId, @FechaVenta, @Proveedor, @Comprador, @PrecioVenta, @IdUsuarioComprador)";

                      using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                      {
                          insertCommand.Parameters.AddWithValue("@RemateId", loteVendido.IdRemate);
                          insertCommand.Parameters.AddWithValue("@LoteId", loteVendido.IdLote);
                          insertCommand.Parameters.AddWithValue("@FechaVenta", loteVendido.FechaVenta);
                          insertCommand.Parameters.AddWithValue("@Proveedor", loteVendido.Proveedor);
                          insertCommand.Parameters.AddWithValue("@Comprador", loteVendido.Comprador);
                          insertCommand.Parameters.AddWithValue("@PrecioVenta", loteVendido.PrecioDeVenta);
                          insertCommand.Parameters.AddWithValue("@IdUsuarioComprador", loteVendido.IdUsuarioComprador);


                          insertCommand.ExecuteNonQuery();
                      }

                      // Actualizar el campo RemateId en la tabla lotes (opcional si lo deseas hacer)
                      string updateQuery = "UPDATE lote SET id_remate = @RemateId WHERE id = @LoteId";

                      using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                      {
                          updateCommand.Parameters.AddWithValue("@RemateId", loteVendido.IdRemate);
                          updateCommand.Parameters.AddWithValue("@LoteId", loteVendido.IdLote);

                          updateCommand.ExecuteNonQuery();
                      }
                  }
                  catch (MySqlException ex)
                  {
                      throw new Exception("Error al marcar lote como vendido: " + ex.Message);
                  }
              }
          }*/

        public void MarcarLoteComoVendido(int idLote, int idRemate, int idPreventa)
        {
            try
            {
                
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Insertar en la tabla lotesvendidos usando el id_preventa y id_lote obtenidos
                    string insertarLoteVendidoQuery = @"INSERT INTO lotesvendidos (id_preventa, id_lote, id_remate, fecha_venta)
                                                 VALUES (@IdPreventa, @IdLote, @IdRemate, CURRENT_DATE)";

                    using (MySqlCommand command = new MySqlCommand(insertarLoteVendidoQuery, connection))
                    {
                        command.Parameters.AddWithValue("@IdPreventa", idPreventa);
                        command.Parameters.AddWithValue("@IdLote", idLote);
                        command.Parameters.AddWithValue("@IdRemate", idRemate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al marcar el lote como vendido: " + ex.Message);
            }
        }



        public void QuitarLoteVendido(int idRemate, int idLote)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    // Eliminar la entrada de la tabla lotesVendidos
                    string deleteQuery = "DELETE FROM lotesvendidos WHERE id_remate = @RemateId AND id_lote = @LoteId";

                    using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection))
                    {
                        deleteCommand.Parameters.AddWithValue("@RemateId", idRemate);
                        deleteCommand.Parameters.AddWithValue("@LoteId", idLote);

                        deleteCommand.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Puedes manejar el error según tu lógica de negocio, por ejemplo, registrar en un log.
                    throw new Exception("Error al quitar lote de la lista de vendidos: " + ex.Message);
                }
            }
        }

        public bool ExisteLoteVendido(int loteId)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    string query = "SELECT COUNT(*) FROM lotesvendidos WHERE id_lote = @LoteId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoteId", loteId);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        return count > 0;
                    }
                }
                catch (MySqlException ex)
                {
                    // Puedes manejar el error según tu lógica de negocio, por ejemplo, registrar en un log.
                    throw new Exception("Error al verificar si el lote ha sido vendido: " + ex.Message);
                }
            }
        }


        public void DesasignarLotesNoVendidosEnRematesAntiguos(DateTime fechaLimite)
        {
            List<int> rematesAntiguos = ObtenerRematesAntiguos(fechaLimite);

            foreach (int remateId in rematesAntiguos)
            {
                DesasignarLotesNoVendidosPorRemate(remateId);
            }
        }

        private List<int> ObtenerRematesAntiguos(DateTime fechaLimite)
        {
            List<int> rematesAntiguos = new List<int>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT id FROM remate WHERE fecha <= @FechaLimite";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FechaLimite", fechaLimite);

                    //connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int remateId = reader.GetInt32("id");
                            rematesAntiguos.Add(remateId);
                        }
                    }
                }
            }

            return rematesAntiguos;
        }
        public List<Remate> ObtenerRematesPosteriores(DateTime fechaLimite)
        {
            List<Remate> rematesPosteriores = new List<Remate>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT id, fecha, hora_inicio, hora_fin, rematador, tipo_de_remate FROM remate WHERE fecha >= @FechaLimite AND activo = 1 ORDER BY fecha";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FechaLimite", fechaLimite);

                    //connection.Open();
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Remate remate = new Remate
                            {
                                Id = reader.GetInt32("id"),
                                Fecha = reader.GetDateTime("fecha"),
                                HoraInicio = reader.GetTimeSpan("hora_inicio"),
                                HoraFin = reader.GetTimeSpan("hora_fin"),
                                Rematador = reader.GetString("rematador"),
                                TipoDeRemate = reader.GetString("tipo_de_remate")
                            };

                            rematesPosteriores.Add(remate);
                        }
                    }
                }
            }

            return rematesPosteriores;
        }


        private void DesasignarLotesNoVendidosPorRemate(int remateId)
        {
            List<int> lotesNoVendidos = ObtenerLotesNoVendidosPorRemate(remateId);

            foreach (int loteId in lotesNoVendidos)
            {
                DesasignarLoteDeRemate(loteId);
            }
        }

        private List<int> ObtenerLotesNoVendidosPorRemate(int remateId)
        {
            List<int> lotesNoVendidos = new List<int>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT id FROM lote WHERE id_remate = @RemateId AND id NOT IN (SELECT id_lote FROM lotesvendidos)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@RemateId", remateId);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int loteId = reader.GetInt32("id");
                            lotesNoVendidos.Add(loteId);
                        }
                    }
                }
            }

            return lotesNoVendidos;
        }



        public string ObtenerTipoRemate(int remateId)
        {
            try
            {
                using (MySqlConnection conn = Conexion.obtenerConexion())
                {
                    string query = "SELECT tipo_de_remate FROM remate WHERE id = @RemateId";
                    using (MySqlCommand command = new MySqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@RemateId", remateId);
                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            return result.ToString();
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener el tipo de remate: " + ex.Message);
            }

            return ""; // Retornar un valor predeterminado si no se encuentra el tipo de remate
        }

        public byte[] ObtenerFotoDelLote(int loteId)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    string query = "SELECT imagen FROM lote WHERE id = @LoteId";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@LoteId", loteId);

                        //connection.Open();
                        object result = command.ExecuteScalar();

                        if (result != null && result != DBNull.Value)
                        {
                            return (byte[])result; // Convierte el resultado a un array de bytes (imagen)
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    throw new Exception("Error al obtener la imagen del lote: " + ex.Message);
                }
            }

            return null; // Si no hay imagen o no existe, retorna null
        }


        public bool EsEmpleadoRegistrado(int idUsuario)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM empleados WHERE id_usuario = @idUsuario AND activo = 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    // Si count es mayor que 0, significa que el empleado ya está registrado
                    return count > 0;
                }
            }
        }






        public void AgregarEmpleado(int idUsuario, TimeSpan horaEntrada, TimeSpan horaSalida, decimal sueldoMensual, string tareas)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "INSERT INTO empleados (id_usuario, hora_entrada, hora_salida, sueldo_mensual, tareas) VALUES (@idUsuario, @horaEntrada, @horaSalida, @sueldoMensual, @tareas)";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuario", idUsuario);
                    command.Parameters.AddWithValue("@horaEntrada", horaEntrada);
                    command.Parameters.AddWithValue("@horaSalida", horaSalida);
                    command.Parameters.AddWithValue("@sueldoMensual", sueldoMensual);
                    command.Parameters.AddWithValue("@tareas", tareas);

                    command.ExecuteNonQuery();
                }
            }
        }



        public void AgregarOperador(int idUsuario, TimeSpan horaEntrada, TimeSpan horaSalida, decimal sueldoMensual, string tareas)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                // Insertar en la tabla empleados
                string empleadosQuery = "INSERT INTO empleados (id_usuario, hora_entrada, hora_salida, sueldo_mensual) VALUES (@idUsuario, @horaEntrada, @horaSalida, @sueldoMensual)";
                using (MySqlCommand empleadosCommand = new MySqlCommand(empleadosQuery, connection))
                {
                    empleadosCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    empleadosCommand.Parameters.AddWithValue("@horaEntrada", horaEntrada);
                    empleadosCommand.Parameters.AddWithValue("@horaSalida", horaSalida);
                    empleadosCommand.Parameters.AddWithValue("@sueldoMensual", sueldoMensual);
                    empleadosCommand.ExecuteNonQuery();
                }

                // Obtener el id_empleado del empleado recién insertado
                int idEmpleado = ObtenerIdEmpleadoRecienInsertado(connection); // Implementa este método según tu lógica

                // Insertar en la tabla operadores
                string operadoresQuery = "INSERT INTO operadores (id_empleado, tareas) VALUES (@idEmpleado, @tareas)";
                using (MySqlCommand operadoresCommand = new MySqlCommand(operadoresQuery, connection))
                {
                    operadoresCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    operadoresCommand.Parameters.AddWithValue("@tareas", tareas);
                    operadoresCommand.ExecuteNonQuery();
                }
            }
        }

        public void AgregarRematador(int idUsuario, TimeSpan horaEntrada, TimeSpan horaSalida, decimal sueldoMensual, string numeroRematador, decimal comision)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                // Insertar en la tabla empleados
                string empleadosQuery = "INSERT INTO empleados (id_usuario, hora_entrada, hora_salida, sueldo_mensual) VALUES (@idUsuario, @horaEntrada, @horaSalida, @sueldoMensual)";
                using (MySqlCommand empleadosCommand = new MySqlCommand(empleadosQuery, connection))
                {
                    empleadosCommand.Parameters.AddWithValue("@idUsuario", idUsuario);
                    empleadosCommand.Parameters.AddWithValue("@horaEntrada", horaEntrada);
                    empleadosCommand.Parameters.AddWithValue("@horaSalida", horaSalida);
                    empleadosCommand.Parameters.AddWithValue("@sueldoMensual", sueldoMensual);
                    empleadosCommand.ExecuteNonQuery();
                }

                // Obtener el id_empleado del empleado recién insertado
                int idEmpleado = ObtenerIdEmpleadoRecienInsertado(connection); // Implementa este método según tu lógica

                // Insertar en la tabla rematadores
                string rematadoresQuery = "INSERT INTO rematadores (id_empleado, n_rematador, comision) VALUES (@idEmpleado, @numeroRematador, @Comision)";
                using (MySqlCommand rematadoresCommand = new MySqlCommand(rematadoresQuery, connection))
                {
                    rematadoresCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);
                    rematadoresCommand.Parameters.AddWithValue("@numeroRematador", numeroRematador);
                    rematadoresCommand.Parameters.AddWithValue("@Comision", comision);
                    rematadoresCommand.ExecuteNonQuery();
                }
            }
        }



        private int ObtenerIdEmpleadoRecienInsertado(MySqlConnection connection)
        {
            string query = "SELECT LAST_INSERT_ID()";
            using (MySqlCommand command = new MySqlCommand(query, connection))
            {
                object result = command.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    throw new Exception("No se pudo obtener el id_empleado recién insertado.");
                }
            }
        }





        /*public void EliminarEmpleado(int idEmpleado)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    string query = "DELETE FROM empleados WHERE id = @IdEmpleado";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el empleado: " + ex.Message);
            }
        }*/
        public void DarDeBajaEmpleado(int idEmpleado)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Actualizar el campo activo en la tabla empleados
                    string darDeBajaQuery = "UPDATE empleados SET activo = 0 WHERE id = @IdEmpleado";
                    using (MySqlCommand darDeBajaCommand = new MySqlCommand(darDeBajaQuery, connection))
                    {
                        darDeBajaCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        darDeBajaCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al dar de baja al empleado: " + ex.Message);
            }
        }

        public void EliminarEmpleado(int idEmpleado)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Verificar si el empleado es un rematador
                    string verificarRematadorQuery = "SELECT COUNT(*) FROM rematadores WHERE id_empleado = @IdEmpleado";
                    using (MySqlCommand verificarRematadorCommand = new MySqlCommand(verificarRematadorQuery, connection))
                    {
                        verificarRematadorCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        int countRematadores = Convert.ToInt32(verificarRematadorCommand.ExecuteScalar());

                        // Si el empleado es un rematador, eliminar el registro correspondiente en la tabla rematadores
                        if (countRematadores > 0)
                        {
                            string eliminarRematadorQuery = "DELETE FROM rematadores WHERE id_empleado = @IdEmpleado";
                            using (MySqlCommand eliminarRematadorCommand = new MySqlCommand(eliminarRematadorQuery, connection))
                            {
                                eliminarRematadorCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                                eliminarRematadorCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // Verificar si el empleado es un operador
                    string verificarOperadorQuery = "SELECT COUNT(*) FROM operadores WHERE id_empleado = @IdEmpleado";
                    using (MySqlCommand verificarOperadorCommand = new MySqlCommand(verificarOperadorQuery, connection))
                    {
                        verificarOperadorCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        int countOperadores = Convert.ToInt32(verificarOperadorCommand.ExecuteScalar());

                        // Si el empleado es un operador, eliminar el registro correspondiente en la tabla operadores
                        if (countOperadores > 0)
                        {
                            string eliminarOperadorQuery = "DELETE FROM operadores WHERE id_empleado = @IdEmpleado";
                            using (MySqlCommand eliminarOperadorCommand = new MySqlCommand(eliminarOperadorQuery, connection))
                            {
                                eliminarOperadorCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                                eliminarOperadorCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // Finalmente, eliminar el empleado de la tabla empleados
                    string eliminarEmpleadoQuery = "DELETE FROM empleados WHERE id = @IdEmpleado";
                    using (MySqlCommand eliminarEmpleadoCommand = new MySqlCommand(eliminarEmpleadoQuery, connection))
                    {
                        eliminarEmpleadoCommand.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                        eliminarEmpleadoCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al eliminar el empleado: " + ex.Message);
            }
        }


        /*
        public void ModificarEmpleado(int idEmpleado, string loginUsuario, TimeSpan horaEntrada, TimeSpan horaSalida, decimal sueldoMensual, string tareas)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Obtener el usuario a partir del nombre de login
                    Usuario usuario = repositorioUsuarios.ObtenerUsuarioPorLogin(loginUsuario);

                    if (usuario != null)
                    {
                        int idUsuario = usuario.id; // Obtener el ID del usuario

                        string query = "UPDATE empleados SET id_usuario = @IdUsuario, hora_entrada = @HoraEntrada, hora_salida = @HoraSalida, sueldo_mensual = @SueldoMensual, tareas = @Tareas WHERE id = @IdEmpleado";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                            command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            command.Parameters.AddWithValue("@HoraEntrada", horaEntrada);
                            command.Parameters.AddWithValue("@HoraSalida", horaSalida);
                            command.Parameters.AddWithValue("@SueldoMensual", sueldoMensual);
                            command.Parameters.AddWithValue("@Tareas", tareas);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el usuario.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al modificar el empleado: " + ex.Message);
            }
        }*/

        public void ModificarRematador(int idEmpleado, string loginUsuario, TimeSpan horaEntrada, TimeSpan horaSalida, decimal sueldoMensual, string numeroRematador, decimal comision)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Obtener el usuario a partir del nombre de login
                    Usuario usuario = repositorioUsuarios.ObtenerUsuarioPorLogin(loginUsuario);

                    if (usuario != null)
                    {
                        int idUsuario = usuario.id; // Obtener el ID del usuario

                        string query = "UPDATE empleados SET id_usuario = @IdUsuario, hora_entrada = @HoraEntrada, hora_salida = @HoraSalida, sueldo_mensual = @SueldoMensual WHERE id = @IdEmpleado";
                        string queryRematador = "UPDATE rematadores SET n_rematador = @NumeroRematador, comision = @Comision WHERE id_empleado = @IdEmpleado";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                            command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            command.Parameters.AddWithValue("@HoraEntrada", horaEntrada);
                            command.Parameters.AddWithValue("@HoraSalida", horaSalida);
                            command.Parameters.AddWithValue("@SueldoMensual", sueldoMensual);

                            command.ExecuteNonQuery();
                        }

                        using (MySqlCommand command = new MySqlCommand(queryRematador, connection))
                        {
                            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                            command.Parameters.AddWithValue("@NumeroRematador", string.IsNullOrEmpty(numeroRematador) ? DBNull.Value : (object)numeroRematador);
                            command.Parameters.AddWithValue("@Comision", comision);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el usuario.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al modificar el rematador: " + ex.Message);
            }
        }

        public void ModificarOperador(int idEmpleado, string loginUsuario, TimeSpan horaEntrada, TimeSpan horaSalida, decimal sueldoMensual, string tareas)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    // Obtener el usuario a partir del nombre de login
                    Usuario usuario = repositorioUsuarios.ObtenerUsuarioPorLogin(loginUsuario);

                    if (usuario != null)
                    {
                        int idUsuario = usuario.id; // Obtener el ID del usuario

                        string query = "UPDATE empleados SET id_usuario = @IdUsuario, hora_entrada = @HoraEntrada, hora_salida = @HoraSalida, sueldo_mensual = @SueldoMensual WHERE id = @IdEmpleado";
                        string queryOperador = "UPDATE operadores SET tareas = @Tareas WHERE id_empleado = @IdEmpleado";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                            command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                            command.Parameters.AddWithValue("@HoraEntrada", horaEntrada);
                            command.Parameters.AddWithValue("@HoraSalida", horaSalida);
                            command.Parameters.AddWithValue("@SueldoMensual", sueldoMensual);

                            command.ExecuteNonQuery();
                        }

                        using (MySqlCommand command = new MySqlCommand(queryOperador, connection))
                        {
                            command.Parameters.AddWithValue("@IdEmpleado", idEmpleado);
                            command.Parameters.AddWithValue("@Tareas", string.IsNullOrEmpty(tareas) ? DBNull.Value : (object)tareas);

                            command.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new Exception("No se pudo obtener el usuario.");
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al modificar el operador: " + ex.Message);
            }
        }



        public string ObtenerNombreComprador(string idUsuarioComprador)
        {
            string nombreCompleto = "Nombre no disponible"; // Valor predeterminado en caso de que no se pueda obtener el nombre

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT CONCAT(nombre, ' ', apellido) AS nombreCompleto FROM usuarios WHERE id = @idUsuarioComprador";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idUsuarioComprador", idUsuarioComprador);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Se encontró el nombre y apellido del comprador en la base de datos
                            nombreCompleto = reader["nombreCompleto"].ToString();
                        }
                    }
                }
            }

            return nombreCompleto;
        }





        public List<string> ObtenerUsuariosOperadores()
        {
            List<string> usuariosOperadores = new List<string>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT login FROM usuarios WHERE rol = 'OPERADOR'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string login = reader["login"].ToString();
                            usuariosOperadores.Add(login);
                        }
                    }
                }
            }

            return usuariosOperadores;
        }

        public List<string> ObtenerUsuariosRematadores()
        {
            List<string> usuariosRematadores = new List<string>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT login FROM usuarios WHERE rol = 'REMATADOR'";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string login = reader["login"].ToString();
                            usuariosRematadores.Add(login);
                        }
                    }
                }
            }

            return usuariosRematadores;
        }
        public List<string> ObtenerUsuariosOperadoresActivos()
        {
            List<string> usuariosOperadoresActivos = new List<string>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT login FROM usuarios WHERE rol = 'OPERADOR' AND activo = 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string login = reader["login"].ToString();
                            usuariosOperadoresActivos.Add(login);
                        }
                    }
                }
            }

            return usuariosOperadoresActivos;
        }

        public List<string> ObtenerUsuariosRematadoresActivos()
        {
            List<string> usuariosRematadoresActivos = new List<string>();

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT login FROM usuarios WHERE rol = 'REMATADOR' AND activo = 1";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string login = reader["login"].ToString();
                            usuariosRematadoresActivos.Add(login);
                        }
                    }
                }
            }

            return usuariosRematadoresActivos;
        }




        public List<Empleado> ObtenerEmpleados()
        {
            List<Empleado> empleados = new List<Empleado>();

            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    string query = @"
        SELECT e.*, u.login, u.nombre, u.apellido, o.tareas, r.n_rematador, r.comision
        FROM empleados e
        INNER JOIN usuarios u ON e.id_usuario = u.id
        LEFT JOIN operadores o ON e.id = o.id_empleado
        LEFT JOIN rematadores r ON e.id = r.id_empleado
        WHERE e.activo = 1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Empleado empleado = null; // Inicializar la variable aquí

                                int id = Convert.ToInt32(reader["id"]);
                                int idUsuario = Convert.ToInt32(reader["id_usuario"]);
                                string login = reader["login"].ToString();
                                string nombre = reader["nombre"].ToString();
                                string apellido = reader["apellido"].ToString();
                                TimeSpan horaEntrada = TimeSpan.Parse(reader["hora_entrada"].ToString());
                                TimeSpan horaSalida = TimeSpan.Parse(reader["hora_salida"].ToString());
                                decimal sueldoMensual = reader["sueldo_mensual"] != DBNull.Value ? Convert.ToDecimal(reader["sueldo_mensual"]) : 0;
                                string tareas = reader["tareas"] != DBNull.Value ? reader["tareas"].ToString() : string.Empty;
                                string numeroRematador = reader["n_rematador"] != DBNull.Value ? reader["n_rematador"].ToString() : null; // Convierte a string
                                decimal comision = reader["comision"] != DBNull.Value ? Convert.ToDecimal(reader["comision"]) : 0; // Obtener la comisión
                                // Crear el objeto Empleado, Rematador u Operador según el caso
                                if (numeroRematador != null)
                                {
                                    empleado = new Rematador
                                    {
                                        Id = id,
                                        IdUsuario = idUsuario,
                                        Login = login,
                                        Nombre = nombre,
                                        Apellido = apellido,
                                        HoraEntrada = horaEntrada,
                                        HoraSalida = horaSalida,
                                        SueldoMensual = sueldoMensual,
                                        NRematador = numeroRematador,
                                        Comision = comision
                                    };
                                }
                                else
                                {
                                    // Verifica si es operador y asigna el valor correspondiente a Tareas
                                    //string tareasOperador = (empleado is Operador) ? tareas : null;

                                    empleado = new Operador
                                    {
                                        Id = id,
                                        IdUsuario = idUsuario,
                                        Login = login,
                                        Nombre = nombre,
                                        Apellido = apellido,
                                        HoraEntrada = horaEntrada,
                                        HoraSalida = horaSalida,
                                        SueldoMensual = sueldoMensual,
                                        Tareas = tareas
                                    };
                                }

                                empleados.Add(empleado);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener los empleados: " + ex.Message);
            }

            return empleados;
        }





        //OBTENER PREVENTAS ----------------------------------------------------------------- TODOS LOS DATOS DE LA TABLA
        public List<Preeventa> ObtenerPreeventas()
        {
            List<Preeventa> preeventas = new List<Preeventa>();

            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    //string query = @"SELECT * FROM preeventas";

                    string query = @"
                      SELECT p.id, p.id_lote, p.id_usuario, p.precio_de_venta,
                             u.nombre, u.apellido, u.correo, u.celular,
                            l.proveedor_lote, l.precio_base, l.tipo_de_lote, l.cantidad_en_lote,
                             l.descripcion, p.id_remate, p.fecha_creacion
                     FROM preeventas p
                     INNER JOIN lote l ON p.id_lote = l.id
                        INNER JOIN usuarios u ON p.id_usuario = u.id;";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                int idLote = Convert.ToInt32(reader["id_lote"]);
                                int idUsuario = Convert.ToInt32(reader["id_usuario"]);
                                decimal precioVenta = Convert.ToDecimal(reader["precio_de_venta"]);
                                string nombreUsuario = reader["nombre"].ToString();
                                string apellidoUsuario = reader["apellido"].ToString();
                                string correoUsuario = reader["correo"].ToString();
                                string celularUsuario = reader["celular"].ToString();
                                string proveedorLote = reader["proveedor_lote"].ToString();
                                int precioBaseLote = Convert.ToInt32(reader["precio_base"]);
                                string tipoLote = reader["tipo_de_lote"].ToString();
                                int cantidadEnLote = Convert.ToInt32(reader["cantidad_en_lote"]);
                                int idRemate = Convert.ToInt32(reader["id_remate"]);
                                string descripcionLote = reader["descripcion"].ToString();
                                DateTime fechaCreacion = Convert.ToDateTime(reader["fecha_creacion"]);

                                Preeventa preventa = new Preeventa
                                {
                                    id = id,
                                    id_lote = idLote,
                                    id_usuario = idUsuario,
                                    precio_de_venta = precioVenta,
                                    nombre_usuario = nombreUsuario,
                                    apellido_usuario = apellidoUsuario,
                                    correo_usuario = correoUsuario,
                                    celular_usuario = celularUsuario,
                                    proveedor_lote = proveedorLote,
                                    precio_base_lote = precioBaseLote,
                                    tipo_lote = tipoLote,
                                    cantidad_en_lote = cantidadEnLote,
                                    id_remate = idRemate,
                                    descripcion_lote = descripcionLote,
                                    fecha_creacion = fechaCreacion
                                };

                                preeventas.Add(preventa);
                            }
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al obtener las preeventas: " + ex.Message);
            }

            return preeventas;
        }




        public void AgregarPreeventa(int idLote, int idUsuario, decimal precioVenta, int idRemate)
        {
            try
            {
                using (MySqlConnection connection = Conexion.obtenerConexion())
                {
                    string query = "INSERT INTO preeventas (id_lote, id_usuario, precio_de_venta, id_remate) " +
                                   "VALUES (@IdLote, @IdUsuario, @PrecioVenta, @IdRemate)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@IdLote", idLote);
                        command.Parameters.AddWithValue("@IdUsuario", idUsuario);
                        command.Parameters.AddWithValue("@PrecioVenta", precioVenta);
                        command.Parameters.AddWithValue("@IdRemate", idRemate);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw new Exception("Error al agregar la preeventa: " + ex.Message);
            }
        }



        public bool LoteYaPrevendido(int idLote)
        {
            // Realizar consulta para verificar si el lote ya está prevendido
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                string query = "SELECT COUNT(*) FROM preeventas WHERE id_lote = @IdLote";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IdLote", idLote);
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    return count > 0;
                }
            }
        }

        public bool HayLotesEnElProximoRemate()
        {
            try
            {
                // Obtener el próximo remate (sustituye ObtenerProximoRemate() con la lógica real para obtener el próximo remate)
                Remate proximoRemate = ObtenerProximoRemate();

                if (proximoRemate != null)
                {
                    // Verificar si hay lotes asignados a este remate (sustituye ObtenerLotesAsignadosPorRemate con la lógica real)
                    List<Lote> lotes = ObtenerLotesAsignadosPorRemate(proximoRemate.Id);

                    return lotes != null && lotes.Count > 0;
                }

                return false;
            }
            catch (Exception ex)
            {
                // Manejar la excepción según tus necesidades
                Console.WriteLine("Error al verificar si hay lotes en el próximo remate: " + ex.Message);
                return false;
            }
        }

        public Remate ObtenerProximoRemate()
        {
            List<Remate> remates = ObtenerRematesOrdenadosPorFecha();
            DateTime fechaActual = DateTime.Now;

            foreach (Remate remate in remates)
            {
                if (remate.Fecha > fechaActual)
                {
                    return remate;
                }
            }

            return null;
        }



        public Usuario ObtenerLoginUsuarioPorId(int id)
        {
            Usuario usuario = null;

            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                //connection.Open();

                string query = "SELECT login FROM usuarios WHERE id = @Id";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            usuario = new Usuario
                            {
                                login = Convert.ToString(reader["login"]),
                            };
                        }
                    }
                }
            }

            return usuario;
        }





    }


}
