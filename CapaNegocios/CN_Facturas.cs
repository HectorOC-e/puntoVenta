using System;
using System.Collections.Generic;
using System.Data;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Facturas
    {
        private CD_Facturas facturaCD = new CD_Facturas();

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public DateTime FechaFactura { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; }

        public DataTable MostrarFacturas()
        {
            DataTable tabla = facturaCD.MostrarTodo();
            return tabla;
        }

        public DataTable MostrarFacturaPorId(int facturaId)
        {
            DataTable tabla = facturaCD.MostrarFacturaPorId(facturaId);
            return tabla;
        }

        public void InsertarFactura()
        {
            facturaCD.Insertar(ClienteId, FechaFactura, Total, Estado);
        }

        public void ActualizarFactura()
        {
            facturaCD.Actualizar(Id, ClienteId, FechaFactura, Total, Estado);
        }

        public void EliminarFactura()
        {
            facturaCD.Eliminar(Id);
        }
    }
}
