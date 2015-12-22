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
    public partial class Pantalla_Principal : Form
    {
        DataTable dt;
        Utilities sql;
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
            dt = sql.SQLdata("select id AS ID_EMPLEADO,nombre AS NOMBRE_COMPLETO,calle + '' + next AS DOMICILIO,colonia AS COLONIA," +
                             "municipio AS MUNICIPIO,estado AS ESTADO,pais AS PAIS,nacimiento AS FECHA_NACIMIENTO,ingreso AS FECHA_INGRESO," +
                             "salariodiurno AS SALARIO_DIA,salarionoc AS SALARIO_NOCHE,beneficiario AS BENEFICIARIO,parentezco AS PARENTESCO," +
                             "telefono1 AS TEL_CASA,telefono2 AS MOVIL,telefono3 AS OTRO,correo AS CORREO,rolaturno AS ROLA_TURNO from personal", null, dataValues);
            dataGridView1.DataSource = dt;
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
    }
}
