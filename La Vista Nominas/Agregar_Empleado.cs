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
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

namespace La_Vista_Nominas
{
    public partial class Agregar_Empleado : Form
    {
        Pantalla_Principal main;
        Utilities sql = new Utilities();
        //string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";        
        string dataValues = ConfigurationManager.AppSettings.Get("rutaDB");

        public Agregar_Empleado()
        {
            InitializeComponent();
            try
            {
                string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
                SqlConnection remoteConnection = new SqlConnection(dataValues);
                remoteConnection.Open();
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error SQL: " + e);
            }
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            btnGuardar.Size = new System.Drawing.Size(55, 55);
        }

        private void btnGuardar_MouseLeave(object sender, EventArgs e)
        {
            btnGuardar.Size = new System.Drawing.Size(48, 48);
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {

        }

        private void imgEmpleado_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string imagen = openFileDialog1.FileName;
                    imgEmpleado.Image = Image.FromFile(imagen);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido","La Vista Alimentos S.A. de C.V.",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int checkTurno,checkLicencia;

            String opcTurno = "", opcLicencia = "";

            if(txtNombre.Text.Equals("") || txtrfc.Text.Equals("") || txtcurp.Text.Equals(""))
            {
                MessageBox.Show("Los campos siguientes no pueden ser vacíos ... \n\n Nombre del Empleado. \n Curp del Empleado \n RFC del Empleado \n\n Favor de corregirlos !!!", "La Vista Nominas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (chkTurnos.Checked == true)
                    opcTurno = "Si";
                else
                    opcTurno = "No";

                if (chkLicencia.Checked == true)
                {
                    opcLicencia = "Si";
                    txtTipoLicencia.Enabled = true;
                    txtClaseLicencia.Enabled = true;
                }
                else
                    opcLicencia = "No";
                try
                {

                    string insert = "INSERT INTO PERSONAL (" +
                    "nombre, rfc, curp, calle, next, nint, colonia, municipio, estado, codpost, sexo," +
                    "lugnac, nacimiento, ingreso, tiponomina, jornada, rolaturno, forma_pago, cuenta, salariodiurno," +
                    "salarionoc, salariobase, nss, licencia, tiplic, claselic, ife, beneficiario, parentezco," +
                    "telCasa, telMovil, telOtro, correo, status, area_laboral)" +

                    "VALUES ('" + txtNombre.Text + "', '" + txtrfc.Text + "', '" + txtcurp.Text + "', '" + txtCalle.Text + "', '" + txtNoExt.Text +
                    "', '" + txtNoInt.Text + "', '" + txtColonia.Text + "', '" + txtMpio.Text + "', '" + txtEdo.Text + "', '" + txtZipCode.Text +
                    "', '" + comboSexo.SelectedItem.ToString() + "', '" + txtNacimiento.Text + "', '" + dateNacimiento.Text +
                    "', '" + dateIngreso.Text + "', '" + cmbTipoNomina.Text + "', '" + cmbJornada.Text + "', '" + opcTurno +
                    "', '" + cmbPago.Text + "', '" + txtCuenta.Text + "', " + Convert.ToDouble(txtBaseDia.Text) +
                    ", " + Convert.ToDouble(txtBaseNoche.Text) + ", " + Convert.ToDouble(txtSBC.Text) + ", '" + txtSeguro.Text +
                    "', '" + opcLicencia + "', '" + txtTipoLicencia.Text + "', '" + txtClaseLicencia.Text + "', '" + txtIFE.Text +
                    "', '" + txtbeneficiario.Text + "', '" + txtparentesco.Text + "', '" + txttel1.Text + "', '" + txttel2.Text +
                    "', '" + txttel3.Text + "', '" + txtcorreo.Text + "','ALTA','" + cmbDepto.Text + "');";


                    sql.SQLstatement(insert, null, dataValues);
                    MessageBox.Show("Empleado Almacenado");
                    guardarImagen();
                    this.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Error SQL: " + error.Message, "La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            }
            
        }

        private void Agregar_Empleado_Load(object sender, EventArgs e)
        {
            int id = sql.nextId("id", "personal", null, dataValues);
            txtNoEmp.Text = id.ToString();

            //Carga de catalogo de departamentos
            DataTable dt = sql.SQLdata("SELECT descripcion from cat_departamentos order by descripcion",null,dataValues);
            List<String> lstPedidos = new List<string>();
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                lstPedidos.Add(dt.Rows[i].ItemArray[0].ToString());
                i++;
            }
            cmbDepto.DataSource = lstPedidos;

            dateNacimiento.Text = "01/01/1960"; //fecha de nacimiento por default

        }

        private void chkLicencia_CheckedChanged(object sender, EventArgs e)
        {
            if(chkLicencia.Checked == true)
            {
                txtTipoLicencia.Enabled = true;
                txtClaseLicencia.Enabled = true;
            }
            else
            {
                txtTipoLicencia.Enabled = false;
                txtClaseLicencia.Enabled = false;
            }
            
        }

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyLetters(e);
        }

        public void onlyLetters(KeyPressEventArgs ex)
        {
            if (!(char.IsLetter(ex.KeyChar)) && (ex.KeyChar != (char)Keys.Back) && (ex.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ex.Handled = true;
                return;
            }
        }

        public void onlyLettersAndNumbers(KeyPressEventArgs ex)
        {
            if (!(char.IsLetter(ex.KeyChar)) && !(char.IsNumber(ex.KeyChar)) && (ex.KeyChar != (char)Keys.Back) && (ex.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("No se permiten símbolos especiales !!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ex.Handled = true;
                return;
            }
        }

        public void onlynumbers(KeyPressEventArgs ex)
        {
            if (!(char.IsNumber(ex.KeyChar)) && (ex.KeyChar != (char)Keys.Back) && (ex.KeyChar != (char)Keys.Space))
            {
                MessageBox.Show("Solo se permiten números !!", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                ex.Handled = true;
                return;
            }
        }

        public void guardarImagen()
        {
            try
            {
                int idEmp = Convert.ToInt32(txtNoEmp.Text);
                System.Data.SqlClient.SqlConnection conn = new System.Data.SqlClient.SqlConnection(dataValues);
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                // Estableciento propiedades
                cmd.Connection = conn;
                cmd.CommandText = "update personal set imagen = @foto where id = " + idEmp;
                
                // Creando los parámetros necesarios
                cmd.Parameters.Add("@foto", System.Data.SqlDbType.Image);
                
                // Asignando el valor de la imagen

                // Stream usado como buffer
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                // Se guarda la imagen en el buffer
                imgEmpleado.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                // Se extraen los bytes del buffer para asignarlos como valor para el
                // parámetro.
                cmd.Parameters["@foto"].Value = ms.GetBuffer();

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Registro Guardado Correctamente");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            guardarImagen();
        }

        /*
        // crea un arreglo de bytes a partir de la imagen que se va a almacenar.
        private byte[] Image2Bytes(Image img)
        {
            string sTemp = Path.GetTempFileName();
            FileStream fs = new FileStream(sTemp, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            img.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
            fs.Position = 0;

            int imgLength = Convert.ToInt32(fs.Length);
            byte[] bytes = new byte[imgLength];
            fs.Read(bytes, 0, imgLength);
            fs.Close();
            return bytes;
        }
        */

        // Recupera la imagen a partir del conjunto de bits guardados en la DB
        private Image Bytes2Image(byte[] bytes)
        {
            if (bytes == null) return null;

            MemoryStream ms = new MemoryStream(bytes);
            Bitmap bm = null;

            try
            {
                bm = new Bitmap(ms);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return bm;
        }

        
        public void consultarImagen()
        {
            int idEmp = Convert.ToInt32(txtNoEmp.Text);
            string cad ="select imagen from personal where id = " + idEmp;
            SqlConnection conexion = new SqlConnection(dataValues);
            SqlCommand comando = new SqlCommand(cad, conexion);
            try
            {
                conexion.Open();
                DataTable dt = sql.SQLdata(cad, null, dataValues);
                string dato = dt.Rows[0].ItemArray[0].ToString();

                SqlDataReader dr;
                dr = comando.ExecuteReader();

                dr.Read();

                byte[] img = (byte[])dr[0] ;

                this.imgEmpleado.Image = Bytes2Image(img);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void imgEmpleado_MouseHover(object sender, EventArgs e)
        {
            imgEmpleado.Image = imageList1.Images[6];
            imgEmpleado.Cursor = Cursors.Hand;
        }

        private void imgEmpleado_MouseLeave(object sender, EventArgs e)
        {
            imgEmpleado.Image = imageList1.Images[7];
            imgEmpleado.Cursor = Cursors.Default;
        }

        private void btnCancelar_MouseHover(object sender, EventArgs e)
        {
            btnCancelar.Size = new System.Drawing.Size(55, 55);
        }

        private void btnCancelar_MouseLeave(object sender, EventArgs e)
        {
            btnCancelar.Size = new System.Drawing.Size(48, 48);
        }

        private void txtrfc_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyLettersAndNumbers(e);
        }

        private void txtcurp_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyLettersAndNumbers(e);
        }

        private void txtMpio_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyLettersAndNumbers(e);
        }

        private void txtbeneficiario_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyLettersAndNumbers(e);
        }

        private void txtparentesco_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlyLetters(e);
        }

        private void txtCuenta_KeyPress(object sender, KeyPressEventArgs e)
        {
            onlynumbers(e);
        }
    }
}