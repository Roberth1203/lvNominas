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
            pictureBox3.BackgroundImage = imageList1.Images[4];   
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
            try
            {
                string sentencia = "";
                sql.SQLstatement(sentencia, null, dataValues);
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
