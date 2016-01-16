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
using Microsoft.Office.Core;

namespace La_Vista_Nominas
{
    public partial class Pantalla_Principal : Form
    {
        public string idCapturado;
        DataTable dt;
        Utilities sql;
        string tipoFiltro;
        string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
        public Pantalla_Principal()
        {
            InitializeComponent();
        }

        private void Pantalla_Principal_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void Pantalla_Principal_Load(object sender, EventArgs e)
        {
            dt = new DataTable();
            sql = new Utilities();
            cargarRegistros();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAdd_MouseHover(object sender, EventArgs e)
        {
            btnAdd.Image = listButtonImages.Images[1];
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.Image = listButtonImages.Images[0];
        }
        private void btnDrop_MouseHover(object sender, EventArgs e)
        {
            btnDrop.Image = listButtonImages.Images[3];
        }

        private void btnDrop_MouseLeave(object sender, EventArgs e)
        {
            btnDrop.Image = listButtonImages.Images[2];
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Agregar_Empleado add = new Agregar_Empleado();
            add.Show();
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            txtBuscar.Image = listButtonImages.Images[5];
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            txtBuscar.Image = listButtonImages.Images[4];
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt = sql.SQLdata("select id AS ID_EMPLEADO,nombre AS NOMBRE_COMPLETO,calle + '' + next AS DOMICILIO,colonia AS COLONIA," +
                             "municipio AS MUNICIPIO,estado AS ESTADO,pais AS PAIS, sexo AS SEXO,nacimiento AS FECHA_NACIMIENTO,ingreso AS FECHA_INGRESO," +
                             "salariodiurno AS SALARIO_DIA,salarionoc AS SALARIO_NOCHE, licencia AS LICENCIA,tiplic AS TIPO,claselic AS CLASE_LICENCIA,beneficiario AS BENEFICIARIO,parentezco AS PARENTESCO," +
                             "telCasa AS TEL_CASA,telMovil AS MOVIL,telOtro AS OTRO,correo AS CORREO, status AS STATUS from personal where nombre like '%" + textBox1.Text + "%'", null, dataValues);
            dataGridView1.DataSource = dt;

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Image = listButtonImages.Images[7];
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = listButtonImages.Images[6];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        public void cargarRegistros()
        {
            DataTable dt = sql.SQLdata("select id AS ID_EMPLEADO, nombre AS NOMBRE_COMPLETO, calle + '' + next AS DOMICILIO, colonia AS COLONIA, " +
                             "municipio AS MUNICIPIO,estado AS ESTADO,pais AS PAIS, sexo AS SEXO,nacimiento AS FECHA_NACIMIENTO,ingreso AS FECHA_INGRESO," +
                             "salariodiurno AS SALARIO_DIA,salarionoc AS SALARIO_NOCHE, licencia AS LICENCIA,tiplic AS TIPO,claselic AS CLASE_LICENCIA,beneficiario AS BENEFICIARIO,parentezco AS PARENTESCO," +
                             "telCasa AS TEL_CASA,telMovil AS MOVIL,telOtro AS OTRO,correo AS CORREO, imagen as IMAGEN from personal", null, dataValues);
            dataGridView1.DataSource = dt;
        }

        private void Pantalla_Principal_Activated(object sender, EventArgs e)
        {

        }

        private void btnDrop_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Id: " + idCapturado);
            ModificarEmpleados update = new ModificarEmpleados();
            update.tipo = "baja";
            update.txtNoEmp.Text = idCapturado.ToString();
            update.Show();
        }

        private void btnExportar_MouseHover(object sender, EventArgs e)
        {
            btnExportar.Image = listButtonImages.Images[9];
        }

        private void btnExportar_MouseLeave(object sender, EventArgs e)
        {
            btnExportar.Image = listButtonImages.Images[8];
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int IDValor = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID_EMPLEADO"].Value.ToString());
            //MessageBox.Show("Valor: " + IDValor);

            //DataGridViewRow row = dataGridView1.CurrentRow;
            //int id = Convert.ToInt32(row.Cells("selectnombrecallenextcoloniamunicipioestadopaissexonacimientoingresosalariodiurnosalarionoclicenciatiplicclaselicbeneficiarioparentezcotelCasatelMoviltelOtrocorreoimagen").Value);
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Image = listButtonImages.Images[11];
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Image = listButtonImages.Images[10];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            String v = dataGridView1.Rows[e.RowIndex].Cells["ID_EMPLEADO"].Value.ToString();
            idCapturado = v;
            //MessageBox.Show("" + idCapturado);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Id: " + idCapturado);
            ModificarEmpleados update = new ModificarEmpleados();
            update.tipo = "cambio";
            update.txtNoEmp.Text = idCapturado.ToString();
            update.Show();
        }
    }
}