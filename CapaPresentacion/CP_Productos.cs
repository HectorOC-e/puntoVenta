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
        private string idProducto = null;
        private bool editar = false;
        public CP_Productos()
        {
            InitializeComponent();
        }

        private void CP_Productos_Load(object sender, EventArgs e)
        {
            MostrarProductos();
            CargarImpuestos();
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtDescripcion.Clear();
            txtPrecio.Clear();
            cmbImpuestos.SelectedIndex = -1;
        }

        private void CargarImpuestos()
        {
            cmbImpuestos.Items.Clear();
            List<string> impuestos = productoCN.CargarImpuestos();
            foreach (string impuesto in impuestos)
            {
                cmbImpuestos.Items.Add(impuesto);
            }


        }

        private void MostrarProductos()
        {
            dgvProductos.DataSource = productoCN.MostrarProductos();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            try
            {
                if (!string.IsNullOrEmpty(txtCodigo.Text) && !string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtPrecio.Text) && cmbImpuestos.SelectedIndex != -1)
                {
                    if (!editar)
                    {
                        CN_Productos producto = new CN_Productos
                        {
                            Codigo = txtCodigo.Text,
                            Descripcion = txtDescripcion.Text,
                            Precio = Convert.ToDecimal(txtPrecio.Text),
                            Impuesto = cmbImpuestos.SelectedItem.ToString(),
                        };
                        producto.InsertarProducto();
                        MostrarProductos();
                        LimpiarCampos();
                        MessageBox.Show("El producto se ha guardado correctamente");
                    }
                    if (editar)
                    {
                        CN_Productos producto = new CN_Productos
                        {
                            Id = Convert.ToInt32(idProducto),
                            Codigo = txtCodigo.Text,
                            Descripcion = txtDescripcion.Text,
                            Precio = Convert.ToDecimal(txtPrecio.Text),
                            Impuesto = cmbImpuestos.SelectedItem.ToString(),
                        };
                        producto.EditarProducto();
                        MostrarProductos();
                        editar = false;
                        LimpiarCampos();
                        MessageBox.Show("El producto se ha actualizado correctamente");
                    }
                }
                else
                {
                    MessageBox.Show("Debe completar todos los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ha ocurrido un error al guardar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
       
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                editar = true;
                idProducto = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                txtCodigo.Text = dgvProductos.CurrentRow.Cells[1].Value.ToString();
                txtDescripcion.Text = dgvProductos.CurrentRow.Cells[2].Value.ToString();
                txtPrecio.Text = dgvProductos.CurrentRow.Cells[3].Value.ToString();
                cmbImpuestos.SelectedItem = dgvProductos.CurrentRow.Cells[4].Value.ToString();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                try
                {
                    idProducto = dgvProductos.CurrentRow.Cells[0].Value.ToString();
                    CN_Productos producto = new CN_Productos
                    {
                        Id = Convert.ToInt32(idProducto)
                    };
                    producto.EliminarProducto();
                    MostrarProductos();
                    MessageBox.Show("Producto Eliminado Correctamente");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error al eliminar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            CP_Clientes cP_Clientes = new CP_Clientes();
            cP_Clientes.Show();
            this.Close();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            CP_Facturas cP_Facturas = new CP_Facturas();
            cP_Facturas.Show();
            this.Close();
        }
    }
}
