using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace La_Vista_Nominas
{
    public partial class ModificarEmpleados : Form
    {
        Pantalla_Principal main = new Pantalla_Principal();
        Utilities util = new Utilities();
        public string tipo;
        string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
        public ModificarEmpleados()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ModificarEmpleados_Load(object sender, EventArgs e)
        {
            if(tipo.Equals("baja"))
            {
                bloqCampos();
                cargarDatos();
            }
            else
                cargarDatos();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (tipo.Equals("baja"))
            {
                guardarImagen();
            }
            else
                guardarImagen();
        }

        private void bloqCampos()
        {
            txtNombre.Enabled = false;
            txtrfc.Enabled = false;
            txtcurp.Enabled = false;
            txtCalle.Enabled = false;
            txtNoExt.Enabled = false;
            txtNoInt.Enabled = false;
            txtColonia.Enabled = false;
            txtMpio.Enabled = false;
            txtEdo.Enabled = false;
            txtZipCode.Enabled = false;
            comboSexo.Enabled = false;
            txtNacimiento.Enabled = false;
            dateNacimiento.Enabled = false;
            dateIngreso.Enabled = false;
            cmbTipoNomina.Enabled = false;
            cmbJornada.Enabled = false;
            chkTurnos.Enabled = false;

            cmbPago.Enabled = false;
            txtCuenta.Enabled = false;
            txtBaseDia.Enabled = false;
            txtBaseNoche.Enabled = false;
            txtSBC.Enabled = false;
            txtSeguro.Enabled = false;
            chkLicencia.Enabled = false;

            txtTipoLicencia.Enabled = false;
            txtClaseLicencia.Enabled = false;
            txtIFE.Enabled = false;
            txtbeneficiario.Enabled = false;
            txtparentesco.Enabled = false;
            txttel1.Enabled = false;
            txttel2.Enabled = false;
            txttel3.Enabled = false;
            txtcorreo.Enabled = false;
        }

        private void cargarDatos()
        {
            MessageBox.Show("id: " + txtNoEmp.Text);
            DataTable dt = util.SQLdata("select nombre,rfc,curp,calle,next,nint,colonia," +
                             "municipio,estado,codpost,sexo,lugnac,nacimiento,ingreso," +
                             "tiponomina,jornada,forma_pago,cuenta,salariodiurno,salarionoc,csnm,nss,licencia,tiplic,claselic,beneficiario,parentezco," +
                             "telCasa,telMovil,telOtro,correo,status from personal where id = " + Convert.ToInt32(txtNoEmp.Text.ToString()) + ";",null,dataValues);

            txtNombre.Text = dt.Rows[0].ItemArray[0].ToString(); 
            txtrfc.Text = dt.Rows[0].ItemArray[1].ToString();
            txtcurp.Text = dt.Rows[0].ItemArray[2].ToString();
            txtCalle.Text = dt.Rows[0].ItemArray[3].ToString();
            txtNoExt.Text = dt.Rows[0].ItemArray[4].ToString();
            txtNoInt.Text = dt.Rows[0].ItemArray[5].ToString();
            txtColonia.Text = dt.Rows[0].ItemArray[6].ToString();
            txtMpio.Text = dt.Rows[0].ItemArray[7].ToString();
            txtEdo.Text = dt.Rows[0].ItemArray[8].ToString();
            txtZipCode.Text = dt.Rows[0].ItemArray[9].ToString();
            comboSexo.Text = dt.Rows[0].ItemArray[10].ToString();
            txtNacimiento.Text = dt.Rows[0].ItemArray[11].ToString();
            dateNacimiento.Text = dt.Rows[0].ItemArray[12].ToString();
            dateIngreso.Text = dt.Rows[0].ItemArray[13].ToString();
            cmbTipoNomina.Text = dt.Rows[0].ItemArray[14].ToString();
            cmbJornada.Text = dt.Rows[0].ItemArray[15].ToString();
            /*
            if (dt.Rows[0].ItemArray[16].ToString().Equals("1"))
                chkTurnos.Checked = true;
            else
                chkTurnos.Checked = false;
            */
            cmbPago.Text = dt.Rows[0].ItemArray[16].ToString();
            txtCuenta.Text = dt.Rows[0].ItemArray[17].ToString();
            txtBaseDia.Text = dt.Rows[0].ItemArray[18].ToString();
            txtBaseNoche.Text = dt.Rows[0].ItemArray[19].ToString();
            txtSBC.Text = dt.Rows[0].ItemArray[20].ToString();
            txtSeguro.Text = dt.Rows[0].ItemArray[21].ToString();

            if (dt.Rows[0].ItemArray[22].ToString().Equals("1"))
                chkLicencia.Checked = true;
            else
                chkLicencia.Checked = false;

            txtTipoLicencia.Text = dt.Rows[0].ItemArray[23].ToString();
            txtClaseLicencia.Text = dt.Rows[0].ItemArray[24].ToString();
            //txtIFE.Text = dt.Rows[0].ItemArray[25].ToString();
            txtbeneficiario.Text = dt.Rows[0].ItemArray[25].ToString();
            txtparentesco.Text = dt.Rows[0].ItemArray[26].ToString();
            txttel1.Text = dt.Rows[0].ItemArray[27].ToString();
            txttel2.Text = dt.Rows[0].ItemArray[28].ToString();


            DataTable dt2 = util.SQLdata("select telOtro,correo,status,ife from personal where id = " + Convert.ToInt32(txtNoEmp.Text.ToString()) + ";", null, dataValues);
            txttel3.Text = dt2.Rows[0].ItemArray[0].ToString();
            txtcorreo.Text = dt2.Rows[0].ItemArray[1].ToString();
            cmbStatus.Text = dt2.Rows[0].ItemArray[2].ToString();
            txtIFE.Text = dt2.Rows[0].ItemArray[3].ToString();
            consultarImagen();
        }

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

        public void consultarImagen()
        {
            int idEmp = Convert.ToInt32(txtNoEmp.Text);
            string cad = "select imagen from personal where id = " + idEmp;
            SqlConnection conexion = new SqlConnection(dataValues);
            SqlCommand comando = new SqlCommand(cad, conexion);
            try
            {
                conexion.Open();
                DataTable dt = util.SQLdata(cad, null, dataValues);
                string dato = dt.Rows[0].ItemArray[0].ToString();

                SqlDataReader dr;
                dr = comando.ExecuteReader();

                dr.Read();

                byte[] img = (byte[])dr[0];

                if (!(img[0] == null))
                    this.imgEmpleado.Image = Bytes2Image(img);
                else
                    MessageBox.Show("Este empleado aún no tiene una imagen asignada !!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void imgEmpleado_DoubleClick(object sender, EventArgs e)
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
                MessageBox.Show("El archivo seleccionado no es un tipo de imagen válido", "La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
