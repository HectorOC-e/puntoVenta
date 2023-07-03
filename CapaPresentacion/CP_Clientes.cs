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
    public partial class CP_Clientes : Form
    {
        CN_Clientes ClientesCN = new CN_Clientes();
        private string idCliente = null;
        private bool editar = false;
        public CP_Clientes()
        {
            InitializeComponent();
        }

        private void CP_Clientes_Load(object sender, EventArgs e)
        {
            MostrarClientes();
        }

        private void LimpiarCampos()
        {
            txtCodigo.Clear();
            txtRTN.Clear();
            txtNombre.Clear();
            cmbSexo.SelectedIndex = -1;
            txtDireccion.Clear();
        }

        private void MostrarClientes()
        {
            dgvClientes.DataSource = ClientesCN.MostrarClientes();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCodigo.Text) && !string.IsNullOrEmpty(txtRTN.Text) && !string.IsNullOrEmpty(txtNombre.Text) && !string.IsNullOrEmpty(txtDireccion.Text) && cmbSexo.SelectedIndex != -1)
                {
                    if (!editar)
                    {
                        CN_Clientes cliente = new CN_Clientes
                        {
                            Codigo = txtCodigo.Text,
                            RTN = txtRTN.Text,
                            Nombre = txtNombre.Text,
                            Sexo = cmbSexo.SelectedItem.ToString(),
                            Direccion = txtDireccion.Text
                            
                        };
                        cliente.InsertarCliente();
                        MostrarClientes();
                        LimpiarCampos();
                        MessageBox.Show("El Cliente se ha guardado correctamente");
                    }
                    if (editar)
                    {
                        CN_Clientes cliente = new CN_Clientes
                        {
                            Id = Convert.ToInt32(idCliente),
                            Codigo = txtCodigo.Text,
                            RTN = txtRTN.Text,
                            Nombre = txtNombre.Text,
                            Sexo = cmbSexo.SelectedItem.ToString(),
                            Direccion = txtDireccion.Text
                        };
                        cliente.EditarCliente();
                        MostrarClientes();
                        editar = false;
                        LimpiarCampos();
                        MessageBox.Show("El Cliente se ha actualizado correctamente");
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
            if (dgvClientes.SelectedRows.Count > 0)
            {
                editar = true;
                idCliente = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                txtCodigo.Text = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                txtNombre.Text = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                cmbSexo.SelectedItem = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                txtRTN.Text = dgvClientes.CurrentRow.Cells[4].Value.ToString();
                txtDireccion.Text = dgvClientes.CurrentRow.Cells[5].Value.ToString();
            }
            else
            {
                MessageBox.Show("Debe seleccionar una fila", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                try
                {
                    idCliente = dgvClientes.CurrentRow.Cells[0].Value.ToString();
                    CN_Clientes cliente = new CN_Clientes
                    {
                        Id = Convert.ToInt32(idCliente)
                    };
                    cliente.EliminarCliente();
                    MostrarClientes();
                    MessageBox.Show("Cliente Eliminado Correctamente");
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
            CP_Productos cP_Productos = new CP_Productos();
            cP_Productos.Show();
            this.Close();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            CP_Facturas cP_Facturas = new CP_Facturas();
            cP_Facturas.Show();
            this.Close();
        }
    }
}
