using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Facturas
    {
        private CD_Conexion conexion = CD_Conexion.Instancia;

        public DataTable MostrarTodo()
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("ObtenerFacturas", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las facturas.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public DataTable MostrarFacturaPorId(int facturaId)
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("ObtenerFacturaPorId", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@FacturaId", facturaId);

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Insertar(int clienteId, DateTime fechaFactura, decimal total, string estado)
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("InsertarFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@ClienteId", clienteId);
                comando.Parameters.AddWithValue("@FechaFactura", fechaFactura);
                comando.Parameters.AddWithValue("@Total", total);
                comando.Parameters.AddWithValue("@Estado", estado);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar la factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Actualizar(int facturaId, int clienteId, DateTime fechaFactura, decimal total, string estado)
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("ActualizarFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@FacturaId", facturaId);
                comando.Parameters.AddWithValue("@ClienteId", clienteId);
                comando.Parameters.AddWithValue("@FechaFactura", fechaFactura);
                comando.Parameters.AddWithValue("@Total", total);
                comando.Parameters.AddWithValue("@Estado", estado);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Eliminar(int facturaId)
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("EliminarFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@FacturaId", facturaId);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
