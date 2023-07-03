using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using CapaDatos;

namespace CapaNegocios
{
    public class CN_Productos
    {
        private CD_Productos productoCD = new CD_Productos();

        public DataTable MostarProductos()
        {
            DataTable tabla = new DataTable();
            tabla = productoCD.Mostar();
            return tabla;
        }
    }
}
