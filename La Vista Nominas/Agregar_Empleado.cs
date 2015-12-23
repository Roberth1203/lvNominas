using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace La_Vista_Nominas
{
    public partial class Agregar_Empleado : Form
    {
        Utilities sql = new Utilities();
        string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
        public Agregar_Empleado()
        {
            InitializeComponent();
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            btnGuardar.Image = imageList1.Images[1];
        }

        private void btnGuardar_MouseLeave(object sender, EventArgs e)
        {
            btnGuardar.Image = imageList1.Images[0];
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
            try
            {
                if (chkTurnos.Checked == true)
                    checkTurno = 1;
                else
                    checkTurno = 0;

                if (chkLicencia.Checked == true)
                    checkLicencia = 1;
                else
                    checkLicencia = 0;

                int tipoNom = 1; //Convert.ToInt32(cmbTipoNomina.SelectedItem.ToString().Substring(0, 1));


                string insert = "INSERT INTO PERSONAL (" +
                "nombre, rfc, curp, calle, next, nint, colonia, municipio, estado,codpost, sexo," +
                "lugnac, nacimiento, ingreso, tiponomina, jornada, rolaturno, forma_pago, cuenta, salariodiurno," + 
                "salarionoc, csnm, nss, licencia,tiplic, claselic, ife, beneficiario, parentezco," +
                "telefono1, telefono2,telefono3, correo)" +

                "VALUES ('" + txtNombre.Text + 
                "', '" + txtrfc.Text + 
                "', '" + txtcurp.Text + 
                "', '" + txtCalle.Text + 
                "', '" + txtNoExt.Text + 
                "', '" + txtNoInt.Text + 
                "', '" + txtColonia.Text +
                "', '" + txtMpio.Text + 
                "', '" + txtEdo.Text + 
                "', " + Convert.ToInt32(txtZipCode.Text) +
                ", '" + comboSexo.SelectedItem.ToString() + 
                "', '" + txtNacimiento.Text + 
                "', '" + dateNacimiento.Text + 
                "', '" + dateIngreso.Text + 
                "', " + tipoNom + 
                ", '" + cmbJornada.SelectedItem.ToString() + 
                "'," + checkTurno + 
                ", '" + cmbPago.SelectedItem.ToString() + 
                "', '" + txtCuenta.Text + 
                "', " + Convert.ToDouble(txtBaseDia.Text) + 
                ", " + Convert.ToDouble(txtBaseNoche.Text) +
                ", " + Convert.ToInt32(txtSBC.Text) + 
                ", '" + txtSeguro.Text + 
                "', " + checkLicencia + 
                ", '" + txtTipoLicencia.Text + 
                "', " + Convert.ToInt32(txtClaseLicencia.Text) + 
                ", '" + txtIFE.Text + 
                "', '" + txtbeneficiario.Text +
                "', '" + txtparentesco.Text + 
                "', " + Convert.ToInt32(txttel1.Text) + 
                ", " + Convert.ToInt32(txttel2.Text) + 
                ", " + Convert.ToInt32(txttel3.Text) + 
                ", '" + txtcorreo.Text + 
                "');";
                
                sql.SQLstatement(insert, null, dataValues);
            }
            catch (Exception error)
            {
                MessageBox.Show("Error SQL: " + error.Message,"La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void onlyLetters(KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void Agregar_Empleado_Load(object sender, EventArgs e)
        {
            int id = sql.nextId("id", "personal", null, dataValues);
            txtNoEmp.Text = id.ToString();
        }
    }
}
