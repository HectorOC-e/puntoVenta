using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Productos
    {
        private CD_Productos productoCD = new CD_Productos();

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public string Impuesto { get; set; }

        public DataTable MostrarProductos()
        {
            try
            {
                DataTable tabla = productoCD.MostrarTodo();
                return tabla;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los productos.", ex);
            }
        }

        public void InsertarProducto()
        {
            try
            {
                if (productoCD.ExisteProducto(Codigo))
                {
                    throw new Exception("Ya existe un producto con el mismo código.");
                }

                productoCD.Insertar(Codigo, Descripcion, Precio, Impuesto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el producto.", ex);
            }
        }

        public void EditarProducto()
        {
            try
            {
                productoCD.Actualizar(Id, Codigo, Descripcion, Precio, Impuesto);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al editar el producto.", ex);
            }
        }

        public void EliminarProducto()
        {
            try
            {
                productoCD.Eliminar(Id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto.", ex);
            }
        }

        public List<string> CargarImpuestos()
        {
            try
            {
                List<string> impuestos = productoCD.CargarImpuestos();
                return impuestos;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los impuestos.", ex);
            }
        }
    }
}
