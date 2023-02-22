using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace EVALUACION1_completado
{
    
    public partial class Form1 : Form
    {
        string cadenaconexion = @"Server=localhost; DataBase=Comercial; Integrated Security=true";
        SqlDataAdapter adaptador;
        SqlConnection conexion;
        DataSet repositorioDatos;
        public Form1()
        {
            InitializeComponent();
           
            //2.configurar metodos del adaptador
            
            //3.creamos instancia de la conexion
            conexion = new SqlConnection(cadenaconexion);
            //1.instancia del adaptador
            adaptador = new SqlDataAdapter();
            repositorioDatos = new DataSet();
            
            adaptador.SelectCommand = new SqlCommand("select * from Cliente", conexion);
            
            adaptador.InsertCommand = new SqlCommand("INSERT INTO Cliente(Nombres,Apellidos,Tipo,LimiteCredito,Direccion,Telefono,Email) " +
                "VALUES(@nombre,@apellido,@tipo,@limite,@direccion,@telefono,@email)", conexion);
            adaptador.InsertCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 50, "Nombres");
            adaptador.InsertCommand.Parameters.Add("@apellido", SqlDbType.VarChar, 50, "Apellidos");
            adaptador.InsertCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 50, "Tipo");
            adaptador.InsertCommand.Parameters.Add("@limite", SqlDbType.Decimal, 2, "LimiteCredito");
            adaptador.InsertCommand.Parameters.Add("@direccion", SqlDbType.VarChar, 50, "Direccion");
            adaptador.InsertCommand.Parameters.Add("@telefono", SqlDbType.VarChar, 50, "Telefono");
            adaptador.InsertCommand.Parameters.Add("@email", SqlDbType.VarChar, 50, "Email");



            adaptador.UpdateCommand = new SqlCommand("UPDATE Cliente SET Nombres = @nombre, Apellidos= @apellido, Tipo= @tipo, LimiteCredito=@limite, " +
                "Direccion= @direccion, Telefono= @telefono, Email=@email " +
                "WHERE ID = @id", conexion);
            adaptador.UpdateCommand.Parameters.Add("@nombre", SqlDbType.VarChar, 50, "Nombres");
            adaptador.UpdateCommand.Parameters.Add("@apellido", SqlDbType.VarChar, 50, "Apellidos");
            adaptador.UpdateCommand.Parameters.Add("@tipo", SqlDbType.VarChar, 50, "Tipo");
            adaptador.UpdateCommand.Parameters.Add("@limite", SqlDbType.Decimal, 2, "LimiteCredito");
            adaptador.UpdateCommand.Parameters.Add("@direccion", SqlDbType.VarChar, 50, "Direccion");
            adaptador.UpdateCommand.Parameters.Add("@telefono", SqlDbType.VarChar, 50, "Telefono");
            adaptador.UpdateCommand.Parameters.Add("@email", SqlDbType.VarChar, 50, "Email");
            adaptador.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, 1, "ID");

            adaptador.DeleteCommand = new SqlCommand("DELETE from Cliente WHERE ID=@id",conexion);
            adaptador.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, 1, "ID");

        }
        //objetos necesarios para mostrar datos en el datagridview modo desconectado
        //cadena de conexion, dataset, dataAdapter
        private void CargarFormulario(object sender, EventArgs e)
        {
            mostrardatos();
        }

        private void mostrardatos()
        {
            //llena datos al dataset
            adaptador.Fill(repositorioDatos,"TipoCliente");
            //enlaza datos al datagriedview
            dgvDatos.DataSource = repositorioDatos.Tables["TipoCliente"];
        }

        private void NuevoRegistro(object sender, EventArgs e)
        {
            frmIngresoDatos frm = new frmIngresoDatos(null);
            
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var nuevaFila = repositorioDatos.Tables["TipoCliente"].NewRow();
                    nuevaFila["Nombres"] = frm.Controls["txtnombre"].Text;
                    nuevaFila["Apellidos"] = frm.Controls["txtapellido"].Text;
                    nuevaFila["Tipo"] = frm.Controls["cbotipo"].Text;
                    nuevaFila["LimiteCredito"] = frm.Controls["txtlimitecredito"].Text;
                    nuevaFila["Telefono"] = frm.Controls["txttelefono"].Text;
                    nuevaFila["Email"] = frm.Controls["txtcorreo"].Text;
                    nuevaFila["Direccion"] = frm.Controls["txtdireccion"].Text;

                    repositorioDatos.Tables["TipoCliente"].Rows.Add(nuevaFila);
                    adaptador.Update(repositorioDatos.Tables["TipoCliente"]);
                    repositorioDatos.Tables["TipoCliente"].Clear();
                    mostrardatos();

            }
        }

        private void EditarRegistro(object sender, EventArgs e)
        {
            var filaActual = dgvDatos.CurrentRow;
            if (filaActual != null)
            {
                var ID = filaActual.Cells[0].Value.ToString();
                DataRow fila = repositorioDatos.Tables["TipoCliente"].Select($"ID={ID}").FirstOrDefault();

                var frm = new frmIngresoDatos(fila);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    fila["Nombres"] = frm.Controls["txtnombre"].Text;
                    fila["Apellidos"] = frm.Controls["txtapellido"].Text;
                    fila["Tipo"] = frm.Controls["cbotipo"].Text;
                    fila["LimiteCredito"] = frm.Controls["txtlimitecredito"].Text;
                    fila["Telefono"] = frm.Controls["txttelefono"].Text;
                    fila["Email"] = frm.Controls["txtcorreo"].Text;
                    fila["Direccion"] = frm.Controls["txtdireccion"].Text;
                    
                    
                }
            }
            adaptador.Update(repositorioDatos.Tables["TipoCliente"]);
            repositorioDatos.Tables["TipoCliente"].Clear();
            mostrardatos();

        }

       

        private void Eliminar(object sender, EventArgs e)
        {
            var filaActual = dgvDatos.CurrentRow;
            var ID = filaActual.Cells[0].Value.ToString();
            
            try
            {
                DataRow fila = repositorioDatos.Tables["TipoCliente"].Select($"ID={ID}").FirstOrDefault();
                if (fila != null)
                {
                    fila.Delete();
                }
                adaptador.Update(repositorioDatos.Tables["TipoCliente"]);
            }
            catch
            {
                MessageBox.Show("no se pudo eliminar registro");
            }

           
        }

        
    }
}
