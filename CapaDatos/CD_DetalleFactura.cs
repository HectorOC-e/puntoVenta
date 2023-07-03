using System;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_DetalleFactura
    {
        private CD_Conexion conexion = CD_Conexion.Instancia;

        public void Insertar(int facturaId, int productoId, int cantidad, decimal precioUnitario, decimal total)
        {
            SqlCommand comando = null;
            try
            {
                comando = new SqlCommand("InsertarDetalleFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@FacturaId", facturaId);
                comando.Parameters.AddWithValue("@ProductoId", productoId);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@PrecioUnitario", precioUnitario);
                comando.Parameters.AddWithValue("@Total", total);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el detalle de factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Actualizar(int id, int facturaId, int productoId, int cantidad, decimal precioUnitario, decimal total)
        {
            SqlCommand comando = null;
            try
            {
                comando = new SqlCommand("ActualizarDetalleFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id", id);
                comando.Parameters.AddWithValue("@FacturaId", facturaId);
                comando.Parameters.AddWithValue("@ProductoId", productoId);
                comando.Parameters.AddWithValue("@Cantidad", cantidad);
                comando.Parameters.AddWithValue("@PrecioUnitario", precioUnitario);
                comando.Parameters.AddWithValue("@Total", total);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el detalle de factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            SqlCommand comando = null;
            try
            {
                comando = new SqlCommand("EliminarDetalleFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el detalle de factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public DataTable ObtenerDetallesFactura(int facturaId)
        {
            SqlCommand comando = null;
            try
            {
                comando = new SqlCommand("ObtenerDetallesFactura", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@FacturaId", facturaId);

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los detalles de la factura.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
