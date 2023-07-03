using System;
using System.Data;
using System.Collections.Generic;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_DetalleFactura
    {
        private CD_DetalleFactura detalleFacturaCD = new CD_DetalleFactura();

        public int Id { get; set; }
        public int FacturaId { get; set; }
        public int ProductoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        public void InsertarDetalleFactura()
        {
            detalleFacturaCD.Insertar(FacturaId, ProductoId, Cantidad, PrecioUnitario, Total);
        }

        public void ActualizarDetalleFactura()
        {
            detalleFacturaCD.Actualizar(Id, FacturaId, ProductoId, Cantidad, PrecioUnitario, Total);
        }

        public void EliminarDetalleFactura()
        {
            detalleFacturaCD.Eliminar(Id);
        }

        public DataTable ObtenerDetallesFactura(int facturaId)
        {
            return detalleFacturaCD.ObtenerDetallesFactura(facturaId);
        }

        public List<CN_DetalleFactura> ObtenerDetallesFacturaList(int facturaId)
        {
            DataTable tabla = detalleFacturaCD.ObtenerDetallesFactura(facturaId);
            List<CN_DetalleFactura> detalles = new List<CN_DetalleFactura>();

            foreach (DataRow fila in tabla.Rows)
            {
                CN_DetalleFactura detalle = new CN_DetalleFactura();
                detalle.Id = Convert.ToInt32(fila["Id"]);
                detalle.FacturaId = Convert.ToInt32(fila["FacturaId"]);
                detalle.ProductoId = Convert.ToInt32(fila["ProductoId"]);
                detalle.Cantidad = Convert.ToInt32(fila["Cantidad"]);
                detalle.PrecioUnitario = Convert.ToDecimal(fila["PrecioUnitario"]);
                detalle.Total = Convert.ToDecimal(fila["Total"]);

                detalles.Add(detalle);
            }

            return detalles;
        }
    }
}
