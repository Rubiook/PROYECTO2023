using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEGOCIO.NEGOCIO
{
    public class NegocioLotesRemates
    {
        private readonly RepositorioUsuarios repositorioUsuarios;

        private MySqlConnection _connection;
        public NegocioLotesRemates()
        {
            repositorioUsuarios = new RepositorioUsuarios();
            _connection = Conexion.obtenerConexion();
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


        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Close();
                _connection.Dispose();
            }
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
                string deleteQuery = "DELETE FROM remate WHERE id = @RemateId";

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
                string query = "SELECT id, fecha FROM remate ORDER BY fecha";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int remateId = Convert.ToInt32(reader["id"]);
                            DateTime fechaRemate = Convert.ToDateTime(reader["fecha"]);

                            Remate remate = new Remate
                            {
                                Id = remateId,
                                Fecha = fechaRemate
                            };

                            remates.Add(remate);
                        }
                    }
                }
            }

            return remates;
        }



        public void MarcarLoteComoVendido(LoteVendido loteVendido)
        {
            using (MySqlConnection connection = Conexion.obtenerConexion())
            {
                try
                {
                    string insertQuery = "INSERT INTO lotesvendidos (id_remate, id_lote, fecha_venta, proveedor, comprador, precio_de_venta) VALUES (@RemateId, @LoteId, @FechaVenta, @Proveedor, @Comprador, @PrecioVenta)";

                    using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection))
                    {
                        insertCommand.Parameters.AddWithValue("@RemateId", loteVendido.IdRemate);
                        insertCommand.Parameters.AddWithValue("@LoteId", loteVendido.IdLote);
                        insertCommand.Parameters.AddWithValue("@FechaVenta", loteVendido.FechaVenta);
                        insertCommand.Parameters.AddWithValue("@Proveedor", loteVendido.Proveedor);
                        insertCommand.Parameters.AddWithValue("@Comprador", loteVendido.Comprador);
                        insertCommand.Parameters.AddWithValue("@PrecioVenta", loteVendido.PrecioDeVenta); // Agregar el precio de venta

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
                string query = "SELECT id, fecha, hora_inicio, hora_fin, rematador, tipo_de_remate FROM remate WHERE fecha >= @FechaLimite";

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


        



    }


}
