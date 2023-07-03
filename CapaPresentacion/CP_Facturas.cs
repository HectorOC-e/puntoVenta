using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class CP_Facturas : Form
    {
        public CP_Facturas()
        {
            InitializeComponent();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            CP_Productos cP_Productos = new CP_Productos();
            cP_Productos.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            CP_Clientes cP_Clientes = new CP_Clientes();
            cP_Clientes.Show();
            this.Hide();
        }
    }
}
