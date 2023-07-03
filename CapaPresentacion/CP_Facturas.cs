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
    public partial class CP_Facturas : Form
    {
        CN_Facturas facturasCN = new CN_Facturas();
        private string IdFactura = null;
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

        public string getIdFactura() => IdFactura;

        private void CP_Facturas_Load(object sender, EventArgs e)
        {
            MostrarFacturas();
        }

        private void MostrarFacturas()
        {
            dgvFacturas.DataSource = facturasCN.MostrarFacturas();
        }

        private void btnEditarFactura_Click(object sender, EventArgs e)
        {
            CP_VerEditarFactura _VerEditarFactura = new CP_VerEditarFactura();
            _VerEditarFactura.ShowDialog();
        }

        private void btnEliminarFactura_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count > 0)
            {
                try
                {
                    IdFactura = dgvFacturas.CurrentRow.Cells[0].Value.ToString();
                    CN_Facturas factura = new CN_Facturas
                    {
                        Id = Convert.ToInt32(IdFactura)
                    };
                    factura.EliminarFactura();
                    MostrarFacturas();
                    MessageBox.Show("Factura Eliminada Correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al eliminars los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
