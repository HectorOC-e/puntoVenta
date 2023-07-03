using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Productos
    {
        private CD_Conexion conexion = CD_Conexion.Instancia;

        public DataTable MostrarTodo()
        {
            try
            {
                SqlCommand comando = new SqlCommand("ObtenerProductos", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al obtener los productos.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Insertar(string codigo, string descripcion, decimal precio, string impuesto)
        {
            try
            {
                SqlCommand comando = new SqlCommand("CrearProducto", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.Parameters.AddWithValue("@Precio", precio);
                comando.Parameters.AddWithValue("@Impuesto", impuesto);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al insertar el producto.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Actualizar(int id, string codigo, string descripcion, decimal precio, string impuesto)
        {
            try
            {
                SqlCommand comando = new SqlCommand("ActualizarProducto", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id", id);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Descripcion", descripcion);
                comando.Parameters.AddWithValue("@Precio", precio);
                comando.Parameters.AddWithValue("@Impuesto", impuesto);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al actualizar el producto.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Eliminar(int id)
        {
            try
            {
                SqlCommand comando = new SqlCommand("EliminarProducto", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al eliminar el producto.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public bool ExisteProducto(string codigo)
        {
            try
            {
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Productos WHERE Codigo = @Codigo", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@Codigo", codigo);

                int count = Convert.ToInt32(comando.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al verificar si existe el producto.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public List<string> CargarImpuestos()
        {
            try
            {
                SqlCommand comando = new SqlCommand("CargarImpuestos", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = comando.ExecuteReader();
                List<string> impuestos = new List<string>();

                while (reader.Read())
                {
                    string impuesto = reader["Nombre"].ToString();
                    impuestos.Add(impuesto);
                }

                return impuestos;
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error al cargar los impuestos.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
