using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Clientes
    {
        private CD_Clientes clienteCD = new CD_Clientes();

        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Sexo { get; set; }
        public string RTN { get; set; }
        public string Direccion { get; set; }

        public DataTable MostrarClientes()
        {
            DataTable tabla = clienteCD.MostrarTodo();
            return tabla;
        }

        public void InsertarCliente()
        {
            if (clienteCD.ExisteCliente(Codigo, RTN))
            {
                throw new Exception("Ya existe un cliente con el mismo código o RTN.");
            }

            clienteCD.Insertar(Codigo, Nombre, Sexo, RTN, Direccion);
        }

        public void EditarCliente()
        {
            clienteCD.Actualizar(Id, Codigo, Nombre, Sexo, RTN, Direccion);
        }

        public void EliminarCliente()
        {
            clienteCD.Eliminar(Id);
        }
    }
}
