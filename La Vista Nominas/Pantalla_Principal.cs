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
using System.Configuration;
using System.Globalization;
using Microsoft.Reporting.WinForms;

namespace La_Vista_Nominas
{
    public partial class Pantalla_Principal : Form
    {

        //Datos a insertar en reporte
        public string nombre { get; set; }
        public string NSS { get; set; }
        public string curp { get; set; }
        public string depto { get; set; }

        
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
            obtenerEmpleados();
            //this.reportViewer1.RefreshReport();
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
        private void superTabControlPanel3_Click(object sender, EventArgs e)
        {
            
        }

        private void obtenerEmpleados()
        {
            DataTable datos1 = sql.SQLdata("SELECT nombre FROM personal WHERE status = 'ALTA' and area_laboral = 'DESTAJO'",null,dataValues);
            DataTable empDia = sql.SQLdata("select nombre from personal where status = 'ALTA'",null,dataValues);
            
            List<string> DestajoList = new List<string>();
            List<string> EmployeeList = new List<string>();
            int i = 0,j = 0;
            
            foreach (DataRow row in empDia.Rows)
            {
                EmployeeList.Add(empDia.Rows[i].ItemArray[0].ToString());  //(string.Format("{0}?{1}", row["campo1"], row["campo2"]));
                i++;
            }

            foreach (DataRow row in datos1.Rows)
            {
                DestajoList.Add(datos1.Rows[j].ItemArray[0].ToString());
                j++;
            }
            
            listaEmpleadosDestajo1.DataSource = DestajoList;
            listaEmpleadosDia.DataSource = EmployeeList;
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
            txtSabado.Text = tmp.Rows[0].ItemArray[6].ToString();
            
            txtCuchillo.Text = tmp.Rows[0].ItemArray[7].ToString();
            txtEscaf.Text = tmp.Rows[0].ItemArray[8].ToString();
            txtCubreB.Text = tmp.Rows[0].ItemArray[9].ToString();
            txtBata.Text = tmp.Rows[0].ItemArray[10].ToString();
            txtCofia.Text = tmp.Rows[0].ItemArray[11].ToString();
            txtMandil.Text = tmp.Rows[0].ItemArray[12].ToString();
            txtBotas.Text = tmp.Rows[0].ItemArray[13].ToString();

            calculoTotales();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnreporte_Click(object sender, EventArgs e)
        {
            
        }
        
        private void saveChanges()
        {
            try
            {
                String updateQuery = "UPDATE datosDestajo SET dia1 = " + txtLunes.Text + ",dia2 = " + txtMartes.Text + ",dia3 = " + txtMiercoles.Text +
                             ",dia4 = " + txtJueves.Text + ",dia5 = " + txtViernes.Text + ",dia6 = " + txtSabado.Text +
                             ",cuchillo = " + Convert.ToDouble(txtCuchillo.Text) + ",escafandra =" + Convert.ToDouble(txtEscaf.Text) +
                             ",cubrebocas = " + Convert.ToDouble(txtCubreB.Text) + ",bata = " + Convert.ToDouble(txtBata.Text) +
                             ",cofia = " + Convert.ToDouble(txtCofia.Text) + ",mandil = " + Convert.ToDouble(txtMandil.Text) +
                             ",botas = " + Convert.ToDouble(txtBotas.Text) + " WHERE nomEmpleado = '" + listaEmpleadosDestajo1.SelectedItem.ToString() + "';";

                sql.SQLstatement(updateQuery, null, dataValues);

                calculoTotales();
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
        }

        private void calculoTotales()
        {
            //Generar totales
            int totales1 = (Convert.ToInt32(txtLunes.Text) + Convert.ToInt32(txtMartes.Text) + Convert.ToInt32(txtMiercoles.Text) + Convert.ToInt32(txtJueves.Text) + Convert.ToInt32(txtViernes.Text) + Convert.ToInt32(txtSabado.Text));
            labelTotalCajas.Text = Convert.ToString(totales1);

            Double totales2 = (Convert.ToDouble(txtCuchillo.Text) + Convert.ToDouble(txtEscaf.Text) + Convert.ToDouble(txtCubreB.Text) + Convert.ToDouble(txtBata.Text) + Convert.ToDouble(txtCofia.Text) + Convert.ToDouble(txtMandil.Text) + Convert.ToDouble(txtBotas.Text));
            labelTotaldesc.Text = "$ " + Convert.ToString(totales2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Nomina_Individual showReport = new Nomina_Individual();
            saveChanges();
        }


        /* ======================================= Métodos Para Obtener datos para Reportes ======================================= */

        private void dataOnNominareport()
        {
            DataTable dt = sql.SQLdata("Select nombre,nss,rfc,curp,area_laboral,puesto from personal where nombre like '%" + listaEmpleadosDestajo1.SelectedItem.ToString() + "%';", null, dataValues);
            Classes.Datos_Empleados employeeData = new Classes.Datos_Empleados();
            Classes.Movimientos_Destajo employeeMovs = new Classes.Movimientos_Destajo();

            //Carga de datos a cada miembro de las clases
            employeeData.nombre = dt.Rows[0].ItemArray[0].ToString();
            employeeData.nss = dt.Rows[0].ItemArray[1].ToString();
            employeeData.rfc = dt.Rows[0].ItemArray[2].ToString();
            employeeData.curp = dt.Rows[0].ItemArray[3].ToString();
            employeeData.nCajas = (Convert.ToInt32(txtLunes.Text) + Convert.ToInt32(txtMartes.Text) + Convert.ToInt32(txtMiercoles.Text) + Convert.ToInt32(txtJueves.Text) + Convert.ToInt32(txtViernes.Text) + Convert.ToInt32(txtSabado.Text));
            employeeData.depto = dt.Rows[0].ItemArray[4].ToString();
            employeeData.puesto = dt.Rows[0].ItemArray[5].ToString();
            employeeData.iniPeriodo = DateTime.Now.AddDays(-6.0); 
            employeeData.finPeriodo = DateTime.Now;
            employeeData.diasLab = 6;

            employeeMovs.sueldo = (Convert.ToDouble(labelTotalCajas.Text) * Convert.ToDouble(txtCostoCaja.Text));
            employeeMovs.aguinaldo = Convert.ToDouble(txtAguinaldoD.Text);
            employeeMovs.vacaciones = Convert.ToDouble(txtVacacionesD.Text);
            employeeMovs.prDominical = Convert.ToDouble(txtDominicalD.Text);
            employeeMovs.prVacacional = Convert.ToDouble(txtVacacionalD.Text);
            employeeMovs.montoPercep = (employeeMovs.sueldo + employeeMovs.aguinaldo + employeeMovs.vacaciones + employeeMovs.prDominical + employeeMovs.prVacacional);
            
            //Hago una instancia del formulario Nomina_Individual y agrego los miembros de las clases a las list del nuevo form.
            Nomina_Individual frm = new Nomina_Individual();
            frm.obj.Add(employeeData);
            frm.objMovimientos.Add(employeeMovs);

            frm.Show();
        }

        private void getDataForListaRaya()
        {
            String text = "select id_empleado,nomEmpleado,sum(dia1 + dia2 + dia3 + dia4 + dia5 + dia6) as CAJAS_TOTALES from datosDestajo where nomEmpleado not in ('null') group by id_empleado,nomEmpleado;";
            DataTable dt = sql.SQLdata(text, null, dataValues);

            Classes.datosListaRaya obj = new Classes.datosListaRaya();
            obj.idEmp = Convert.ToInt32(dt.Rows[0].ItemArray[0].ToString());
            obj.nomEmp = dt.Rows[0].ItemArray[1].ToString();
            obj.cajas = Convert.ToInt32(dt.Rows[0].ItemArray[2].ToString());
            

            if (this.pnlReportForms.Controls.Count > 0)
                this.pnlReportForms.Controls.RemoveAt(0);

            Form_Lista_Raya form = new Form_Lista_Raya();
            form.listado.Add(obj);
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.pnlReportForms.Controls.Add(form);
            this.pnlReportForms.Tag = form;
            form.Show();
        }


    // Carga los campos solicitados del empleado para la cabecera del reporte de nomina
       /* private void loadEmployeeData()
        {
            DataTable headerData = new DataTable();
            DataTable periodData = new DataTable();
            DataTable datos = new DataTable();
            
            List<Classes.Movimientos_Destajo> movs = new List<Classes.Movimientos_Destajo>();
            
            headerData = sql.SQLdata("Select nombre,nss,rfc,curp,area_laboral,puesto,id from personal where nombre like '%" + listaEmpleadosDestajo1.SelectedItem.ToString() + "%';", null, dataValues);
            //periodData = sql.SQLdata("Select area_laboral,puesto from personal where nombre like '%" + listaEmpleadosDestajo1.SelectedItem.ToString() + "%';", null, dataValues);

            reportViewer1.LocalReport.DataSources.Clear();
            // utilizo el nombre del dataset asignado al reporte y el nombre de la instancia de clase intermediaria 
            //reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("datosEmpleado",headerData));
            //reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("infoPeriodo", periodData));
            reportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", headerData));
            reportViewer1.RefreshReport();


            //Instancia de ambos DataSet para reporte de nomina.
            Classes.Datos_Empleados origin = new Classes.Datos_Empleados();
            Classes.Movimientos_Destajo originMov = new Classes.Movimientos_Destajo();

            origin.nombre = headerData.Rows[0].ItemArray[0].ToString();
            origin.nss = headerData.Rows[0].ItemArray[1].ToString();
            origin.rfc = headerData.Rows[0].ItemArray[2].ToString();
            origin.curp = headerData.Rows[0].ItemArray[3].ToString();
            origin.depto = headerData.Rows[0].ItemArray[4].ToString();
            origin.puesto = headerData.Rows[0].ItemArray[5].ToString();
            origin.nCajas = Convert.ToInt32(headerData.Rows[0].ItemArray[6].ToString());
            origin.iniPeriodo = DateTime.Parse(DateTime.Today.ToShortDateString());

            originMov.sueldo = (Convert.ToDouble(labelTotalCajas.Text) * 6.20 );
            originMov.aguinaldo = Convert.ToDouble(txtAguinaldoD.Text);
            originMov.vacaciones = Convert.ToDouble(txtVacacionesD.Text);
            originMov.prDominical = Convert.ToDouble(txtDominicalD.Text);
            originMov.prVacacional = Convert.ToDouble(txtDominicalD.Text);

            tabReportes.Focus();
        }
        */

        private void btnMostrarRecibo_Click(object sender, EventArgs e)
        {
            String cad = DateTime.Today.ToShortDateString();
            MessageBox.Show("Fecha del sistema: " + cad);
            dataOnNominareport();
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("Fuck the Police");
        }

        private void btnExpand_Click(object sender, EventArgs e)
        {
            grBoxDestajo.Size = new System.Drawing.Size(656, 147);
            btnExpand.Visible = false;
            btnContract.Visible = true;
        }

        private void btnContract_Click(object sender, EventArgs e)
        {
            grBoxDestajo.Size = new System.Drawing.Size(656, 0);
            btnContract.Visible = false;
            btnExpand.Visible = true;
        }

        private void btnListaRaya_Click(object sender, EventArgs e)
        {
            getDataForListaRaya();
            /*
            if (this.pnlReportForms.Controls.Count > 0)
                this.pnlReportForms.Controls.RemoveAt(0);

            Form_Lista_Raya form = new Form_Lista_Raya();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            this.pnlReportForms.Controls.Add(form);
            this.pnlReportForms.Tag = form;
            form.Show();
            */
        }
        
    }
}