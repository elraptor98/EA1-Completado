using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EVALUACION1_completado
{
    public partial class frmIngresoDatos : Form
    {
        DataRow fila;
        public frmIngresoDatos(DataRow filaEditar=null)
        {
            InitializeComponent();
            if (filaEditar != null)
            {
                this.fila = filaEditar;
                this.Text = "Editar registro";
                mostrarDatos();
            }
        }

        private void mostrarDatos()
        {
            txtnombre.Text = this.fila["Nombres"].ToString();
            txtapellido.Text = this.fila["Apellidos"].ToString();
            cbotipo.Text = this.fila["Tipo"].ToString();
            txtlimitecredito.Text = this.fila["LimiteCredito"].ToString();
            txtdireccion.Text = this.fila["Direccion"].ToString();
            txtcorreo.Text = this.fila["Email"].ToString();
            txttelefono.Text = this.fila["Telefono"].ToString();
        }

        private void AceptarCambios(object sender, EventArgs e)
        {
            validacion();
        }

        private void validacion()
        {
            double cantidad;
            string tipo;
            tipo = cbotipo.Text;
            cantidad = double.Parse(txtlimitecredito.Text);

            if (string.IsNullOrEmpty(txtnombre.Text) || string.IsNullOrEmpty(txtapellido.Text) || string.IsNullOrEmpty(txtlimitecredito.Text) ||
                string.IsNullOrEmpty(txtdireccion.Text) ||
                string.IsNullOrEmpty(txttelefono.Text) || string.IsNullOrEmpty(txtcorreo.Text) || string.IsNullOrEmpty(cbotipo.Text))
            {
                MessageBox.Show("Debe llenar todos los campos", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (tipo == "VIP" && (cantidad > 2500 || cantidad < 0))
            {
                MessageBox.Show("Limite de Credito: 2500", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            else if (tipo == "PLATINUM" && (cantidad > 3500 || cantidad < 0))
            {
                MessageBox.Show("Limite de Credito: 3500", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            else if (tipo == "GOLDEN" && (cantidad > 5000 || cantidad < 0))
            {
                MessageBox.Show("Limite de Credito: 5000", "AVISO", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

      
    }
}
