using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaNegocios;

namespace CapaPresentacion
{
    public partial class CP_Productos : Form
    {
        CN_Productos productoCN = new CN_Productos(); 
        public CP_Productos()
        {
            InitializeComponent();
        }

        private void CP_Productos_Load(object sender, EventArgs e)
        {
            MostrarProductos();
        }

        private void MostrarProductos()
        {
            dgvProductos.DataSource = productoCN.MostarProductos();
        }
    }
}
