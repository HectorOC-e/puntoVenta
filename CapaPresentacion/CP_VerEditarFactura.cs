using CapaNegocios;
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
    public partial class CP_VerEditarFactura : Form
    {
        CN_Productos productoCN = new CN_Productos();
        CP_Facturas CP_Facturas = new CP_Facturas();
        private string IdFactura = null;

        public CP_VerEditarFactura()
        {
            InitializeComponent();
        }

        private void CP_VerEditarFactura_Load(object sender, EventArgs e)
        {
            MostrarProductos();
            IdFactura = CP_Facturas.getIdFactura();
            MessageBox.Show(IdFactura);
        }

        private void MostrarProductos()
        {
            dgvProductos.DataSource = productoCN.MostrarProductos();
        }

    }
}
