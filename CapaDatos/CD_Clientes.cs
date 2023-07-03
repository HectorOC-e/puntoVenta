using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Clientes
    {
        private CD_Conexion conexion = CD_Conexion.Instancia;

        public DataTable MostrarTodo()
        {
            try
            {
                SqlCommand comando = new SqlCommand("ObtenerClientes", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter adaptador = new SqlDataAdapter(comando);
                DataTable tabla = new DataTable();
                adaptador.Fill(tabla);

                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los clientes.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Insertar(string codigo, string nombre, string sexo, string rtn, string direccion)
        {
            try
            {
                SqlCommand comando = new SqlCommand("CrearCliente", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Sexo", sexo);
                comando.Parameters.AddWithValue("@RTN", rtn);
                comando.Parameters.AddWithValue("@Direccion", direccion);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el cliente.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public void Actualizar(int id, string codigo, string nombre, string sexo, string rtn, string direccion)
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("ActualizarCliente", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id", id);
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Sexo", sexo);
                comando.Parameters.AddWithValue("@RTN", rtn);
                comando.Parameters.AddWithValue("@Direccion", direccion);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el cliente.", ex);
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
                SqlCommand comando = new SqlCommand("EliminarCliente", conexion.AbrirConexion());
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.AddWithValue("@Id", id);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el cliente.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }

        public bool ExisteCliente(string codigo, string rtn)
        {
            
            try
            {
                SqlCommand comando = new SqlCommand("SELECT COUNT(*) FROM Clientes WHERE Codigo = @Codigo OR RTN = @RTN", conexion.AbrirConexion());
                comando.Parameters.AddWithValue("@Codigo", codigo);
                comando.Parameters.AddWithValue("@RTN", rtn);

                int count = Convert.ToInt32(comando.ExecuteScalar());
                return count > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar si existe el cliente.", ex);
            }
            finally
            {
                conexion.CerrarConexion();
            }
        }
    }
}
