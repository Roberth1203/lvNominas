﻿using System;
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
using System.Configuration;

namespace La_Vista_Nominas
{
    public partial class Pantalla_Principal : Form
    {
        ModificarEmpleados update;
        public String idCapturado;
        DataTable dt;
        Utilities sql;
        string tipoFiltro;
        //string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
        String dataValues = ConfigurationManager.ConnectionStrings["La_Vista_Nominas.Properties.Settings.nominaConnectionString"].ConnectionString;
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
            this.reportViewer1.RefreshReport();
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
            //btnAdd.Image = listButtonImages.Images[1];
            btnAdd.Location = new Point(52, 0);
            itemselectedBar.Location = new Point(52,52);
            itemselectedBar.Visible = true;
        }

        private void btnAdd_MouseLeave(object sender, EventArgs e)
        {
            btnAdd.Location = new Point(52, 5);
            btnAdd.Image = listButtonImages.Images[0];
            itemselectedBar.Visible = false;
        }
        private void btnDrop_MouseHover(object sender, EventArgs e)
        {
            btnDrop.Location = new Point(166, 0);
            itemselectedBar.Location = new Point(166,52);
            itemselectedBar.Visible = true;
            //btnDrop.Image = listButtonImages.Images[3];
        }

        private void btnDrop_MouseLeave(object sender, EventArgs e)
        {
            btnDrop.Location = new Point(166, 5);
            itemselectedBar.Visible = false;
            //btnDrop.Image = listButtonImages.Images[2];
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
            dt = sql.SQLdata("SELECT id AS ID,nombre AS NOMBRE,calle + ' ' + next AS DOMICILIO,ISNULL(nint, 'S/N') AS NUM_INTERIOR," +
                                       "codpost AS CODIGO_POSTAL,colonia AS COLONIA,municipio AS MUNICIPIO,estado AS ESTADO,nacimiento AS FECHA_NACIMIENTO," +
                                       "ingreso AS FECHA_INGRESO,area_laboral AS DEPARTAMENTO, puesto AS PUESTO," +
                                       "sexo AS SEXO,lugnac AS LUGAR_NACIMIENTO,curp AS CURP,rfc AS RFC,ife AS IFE,tiponomina AS NOMINA,jornada AS JORNADA," +
                                       "rolaturno AS ROLA_TURNO,forma_pago AS TIPO_PAGO,cuenta AS NUM_CUENTA,salariodiurno AS SALARIO_DIA,salarionoc AS SALARIO_NOCHE," +
                                       "salariobase AS SALARIO_BASE,nss AS NSS,licencia AS LICENCIA,ISNULL(tiplic, 'S/N') AS TIPO,ISNULL(claselic, 'S/N') AS CLASE," +
                                       "beneficiario AS BENEFICIARIO,parentezco AS PARENTESCO,telCasa AS TEL_CASA,telMovil AS MOVIL,telOtro AS TEL_OTRO," +
                                       "correo AS E_MAIL,status AS STATUS,imagen AS IMAGEN FROM personal where nombre like '%" + textBox1.Text + "%'", null, dataValues);
            dataGridView1.DataSource = dt;

        }

        private void pictureBox2_MouseHover(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(223,0);
            itemselectedBar.Location = new Point(223,52);
            itemselectedBar.Visible = true;
            //pictureBox2.Image = listButtonImages.Images[7];
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(223,5);
            itemselectedBar.Visible = false;
            //pictureBox2.Image = listButtonImages.Images[6];
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            cargarRegistros();
        }

        public void cargarRegistros()
        {
            //get data
            DataTable dt = sql.SQLdata("SELECT id AS ID,nombre AS NOMBRE,calle + ' ' + next AS DOMICILIO,ISNULL(nint, 'S/N') AS NUM_INTERIOR," +
                                       "codpost AS CODIGO_POSTAL,colonia AS COLONIA,municipio AS MUNICIPIO,estado AS ESTADO,nacimiento AS FECHA_NACIMIENTO," +
                                       "ingreso AS FECHA_INGRESO,area_laboral AS DEPARTAMENTO, puesto AS PUESTO," +
                                       "sexo AS SEXO,lugnac AS LUGAR_NACIMIENTO,curp AS CURP,rfc AS RFC,ife AS IFE,tiponomina AS NOMINA,jornada AS JORNADA," +
                                       "rolaturno AS ROLA_TURNO,forma_pago AS TIPO_PAGO,cuenta AS NUM_CUENTA,salariodiurno AS SALARIO_DIA,salarionoc AS SALARIO_NOCHE," +
                                       "salariobase AS SALARIO_BASE,nss AS NSS,licencia AS LICENCIA,ISNULL(tiplic, 'S/N') AS TIPO,ISNULL(claselic, 'S/N') AS CLASE," +
                                       "beneficiario AS BENEFICIARIO,parentezco AS PARENTESCO,telCasa AS TEL_CASA,telMovil AS MOVIL,telOtro AS TEL_OTRO," +
                                       "correo AS E_MAIL,status AS STATUS,imagen AS IMAGEN FROM personal;", null, dataValues);
            dataGridView1.DataSource = dt;

            //define width for each column
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[1].Width = 300;

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
            //btnExportar.Image = listButtonImages.Images[9];
            btnExportar.Location = new Point(280,0);
            itemselectedBar.Location = new Point(280,52);
            itemselectedBar.Visible = true;
        }

        private void btnExportar_MouseLeave(object sender, EventArgs e)
        {
            //btnExportar.Image = listButtonImages.Images[8];
            btnExportar.Location = new Point(280, 5);
            itemselectedBar.Visible = false;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Modulo en desarrollo !!!");
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ModificarEmpleados obj = new ModificarEmpleados();
            obj.tipo = "baja";
            obj.txtNoEmp.Text = idCapturado.ToString();
            obj.Show();
        }

        private void pictureBox3_MouseHover(object sender, EventArgs e)
        {
            pictureBox3.Location = new Point(109,0);
            itemselectedBar.Location = new Point(109,52);
            itemselectedBar.Visible = true;
            //pictureBox3.Image = listButtonImages.Images[11];
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            pictureBox3.Location = new Point(109, 5);
            itemselectedBar.Visible = false;
            //&pictureBox3.Image = listButtonImages.Images[10];
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            update = new ModificarEmpleados();
            String v = dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString();
            update.txtNoEmp.Text = v;
            idCapturado = v;
            //MessageBox.Show("" + idCapturado);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Id: " + idCapturado);
            
            //ModificarEmpleados update = new ModificarEmpleados();
            update.tipo = "cambio";
            //update.numeroEmpleado = idCapturado;
            
            //update.txtNoEmp.Text = idCapturado.ToString();
            update.Show();
        }


        // ======================== Eventos en tabNominas ======================== 

        private void button1_Click(object sender, EventArgs e)
        {
            panelDatosDestajo.Visible = true;
            obtenerEmpleados();
        }

        private void obtenerEmpleados()
        {
            DataTable datos1 = sql.SQLdata("SELECT nombre FROM personal WHERE status = 'ALTA' and area_laboral = 'DESTAJO'",null,dataValues);
            String[] array = new String[100];

            for (int index = 0; index < datos1.Rows.Count; index ++)
            {
                array[index] = datos1.Rows[index].ItemArray[0].ToString();
            }

            listaEmpleadosDestajo1.DataSource = array;
        }

        private void listaEmpleadosDestajo1_MouseClick(object sender, MouseEventArgs e)
        {
            string cad = listaEmpleadosDestajo1.SelectedItem.ToString();

            String query = "SELECT * from datosDestajo where nomEmpleado = '" + cad + "';";
            DataTable tmp = sql.SQLdata(query, null, dataValues);

            //Llenado de Campos (Percepciones y Deducciones)
            txtLunes.Text = tmp.Rows[0].ItemArray[1].ToString();
            txtMartes.Text = tmp.Rows[0].ItemArray[2].ToString();
            txtMiercoles.Text = tmp.Rows[0].ItemArray[3].ToString();
            txtJueves.Text = tmp.Rows[0].ItemArray[4].ToString();
            txtViernes.Text = tmp.Rows[0].ItemArray[5].ToString();
            
            txtCuchillo.Text = tmp.Rows[0].ItemArray[6].ToString();
            txtEscaf.Text = tmp.Rows[0].ItemArray[7].ToString();
            txtCubreB.Text = tmp.Rows[0].ItemArray[8].ToString();
            txtBata.Text = tmp.Rows[0].ItemArray[9].ToString();
            txtCofia.Text = tmp.Rows[0].ItemArray[10].ToString();
            txtMandil.Text = tmp.Rows[0].ItemArray[11].ToString();
            txtBotas.Text = tmp.Rows[0].ItemArray[12].ToString();

            //Generar totales
            int totales1 = ( Convert.ToInt32(txtLunes.Text) + Convert.ToInt32(txtMartes.Text) + Convert.ToInt32(txtMiercoles.Text) + Convert.ToInt32(txtJueves.Text) + Convert.ToInt32(txtViernes.Text));
            labelTotalCajas.Text = Convert.ToString(totales1);

            Double totales2 = (Convert.ToDouble(txtCuchillo.Text) + Convert.ToDouble(txtEscaf.Text) + Convert.ToDouble(txtCubreB.Text) + Convert.ToDouble(txtBata.Text) + Convert.ToDouble(txtCofia.Text) + Convert.ToDouble(txtMandil.Text) + Convert.ToDouble(txtBotas.Text));
            labelTotaldesc.Text = Convert.ToString(totales2);


        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}