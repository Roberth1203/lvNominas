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
using System.Globalization;
using Microsoft.Reporting.WinForms;
using data = System.Data;
using excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;

namespace La_Vista_Nominas
{
    public partial class Pantalla_Principal : Form
    {
        Classes.Parametros_Reporte class_Parametros = new Classes.Parametros_Reporte();

        //Datos a insertar en reporte
        public string nombre { get; set; }
        public string NSS { get; set; }
        public string curp { get; set; }
        public string depto { get; set; }


        ModificarEmpleados update;
        public String idCapturado;
        DataTable dt;
        List<string> conteoEmpleados = new List<string>();
        Utilities sql;
        public string sentenciaSQL;
        int indexR = 0;
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
            loadSettings();
            cargarRegistros();
            obtenerEmpleados();
            //this.reportViewer1.RefreshReport();
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
            itemselectedBar.Location = new Point(52, 52);
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
            itemselectedBar.Location = new Point(166, 52);
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
            pictureBox2.Location = new Point(223, 0);
            itemselectedBar.Location = new Point(223, 52);
            itemselectedBar.Visible = true;
            //pictureBox2.Image = listButtonImages.Images[7];
        }

        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Location = new Point(223, 5);
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
                                       "rolaturno AS ROLA_TURNO,calculo AS CALCULO,forma_pago AS TIPO_PAGO,cuenta AS NUM_CUENTA,salariodiurno AS SALARIO_DIA,salarionoc AS SALARIO_NOCHE," +
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
            btnExportar.Location = new Point(280, 0);
            itemselectedBar.Location = new Point(280, 52);
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
            pictureBox3.Location = new Point(109, 0);
            itemselectedBar.Location = new Point(109, 52);
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
            DataTable datos1 = sql.SQLdata("SELECT nombre FROM personal WHERE calculo = 'DESTAJO' AND status = 'ALTA' ORDER BY nombre", null, dataValues); //Cargar lista empleados destajo
            DataTable empDia = sql.SQLdata("SELECT nombre FROM personal WHERE calculo = 'HORAS' AND status = 'ALTA' ORDER BY nombre", null, dataValues); // carga lista de empleados por horas
            DataTable mixtos = sql.SQLdata("SELECT nombre FROM personal WHERE calculo = 'MIXTO' AND status = 'ALTA' ORDER BY nombre", null, dataValues); // Carga lista de empleados mixta
            List<string> DestajoList = new List<string>();
            List<string> EmployeeList = new List<string>();
            List<string> MixList = new List<string>();
            int i = 0, j = 0, k  = 0;

            foreach (DataRow row in empDia.Rows)
            {
                EmployeeList.Add(empDia.Rows[i].ItemArray[0].ToString());  //(string.Format("{0}?{1}", row["campo1"], row["campo2"]));
                i++;
            }

            foreach (DataRow row in datos1.Rows)
            {
                DestajoList.Add(datos1.Rows[j].ItemArray[0].ToString());
                conteoEmpleados.Add(datos1.Rows[j].ItemArray[0].ToString());
                j++;
            }

            foreach ( DataRow row in mixtos.Rows)
            {
                MixList.Add(mixtos.Rows[k].ItemArray[0].ToString());
                k++;
            }

            listaEmpleadosDestajo1.DataSource = DestajoList;
            listaEmpleadosDia.DataSource = EmployeeList;
            listEmpleadoMixto.DataSource = MixList;
        }

        private void listaEmpleadosDestajo1_MouseClick(object sender, MouseEventArgs e)
        {
            btnMostrarRecibo.Enabled = true;
            btnMostrarRecibo.BackColor = Color.White;
            btnNominaMasiva.Enabled = true;
            btnNominaMasiva.BackColor = Color.White;

            string cad = listaEmpleadosDestajo1.SelectedItem.ToString();

            String query = "select dia1,dia2,dia3,dia4,dia5,dia6,cuchillo,escafandra,cubrebocas,bata,cofia,mandil,botas,guantes,comedor,prestamo from datosDestajo where nomEmpleado = '" + cad + "';";
            DataTable tmp = sql.SQLdata(query, null, dataValues);

            //Llenado de Campos (Percepciones y Deducciones)
            txtLunes.Text = tmp.Rows[0].ItemArray[0].ToString();
            txtMartes.Text = tmp.Rows[0].ItemArray[1].ToString();
            txtMiercoles.Text = tmp.Rows[0].ItemArray[2].ToString();
            txtJueves.Text = tmp.Rows[0].ItemArray[3].ToString();
            txtViernes.Text = tmp.Rows[0].ItemArray[4].ToString();
            txtSabado.Text = tmp.Rows[0].ItemArray[5].ToString();

            txtCuchillo.Text = tmp.Rows[0].ItemArray[6].ToString();
            txtEscaf.Text = tmp.Rows[0].ItemArray[7].ToString();
            txtCubreB.Text = tmp.Rows[0].ItemArray[8].ToString();
            txtBata.Text = tmp.Rows[0].ItemArray[9].ToString();
            txtCofia.Text = tmp.Rows[0].ItemArray[10].ToString();
            txtMandil.Text = tmp.Rows[0].ItemArray[11].ToString();
            txtBotas.Text = tmp.Rows[0].ItemArray[12].ToString();
            txtGuantes.Text = tmp.Rows[0].ItemArray[13].ToString();
            txtComedor.Text = tmp.Rows[0].ItemArray[14].ToString();
            txtPrestamo.Text = tmp.Rows[0].ItemArray[15].ToString();

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
                             ",botas = " + Convert.ToDouble(txtBotas.Text) + ",guantes = " + Convert.ToDouble(txtGuantes.Text) +
                             ",comedor = " + Convert.ToDouble(txtComedor.Text) + ",prestamo = " + Convert.ToDouble(txtPrestamo.Text) +
                             " WHERE nomEmpleado = '" + listaEmpleadosDestajo1.SelectedItem.ToString() + "';";

                sql.SQLstatement(updateQuery, null, dataValues);

                calculoTotales();
            }
            catch (Exception e)
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
        private void showAllPaySheet()
        {
            string msg = ConfigurationManager.AppSettings.Get("keyCajas");
            MessageBox.Show("elemento del App.Config: " + msg);

            conteoEmpleados.ForEach(delegate (String empleado)
            {

                string[] array = { "Cuchilllo", "Cofia", "Escafandra", "Mandil", "Cubrebocas", "Botas", "Bata", "Guantes", "Descuento Comedor", "Prestamo Empresa" };
                List<String> discountsList = new List<string>();
                DataTable dt = sql.SQLdata("Select nombre,nss,rfc,curp,area_laboral,puesto from personal where nombre like '%" + empleado + "%';", null, dataValues);
                DataTable caj = sql.SQLdata("select sum(dia1+dia2+dia3+dia4+dia5+dia6) from datosDestajo where nomEmpleado like '%" + empleado + "%'",null,dataValues);

                MessageBox.Show(caj.Rows[0].ItemArray[0].ToString());
                DataTable ded = sql.SQLdata("select cuchillo,cofia,escafandra,mandil,cubrebocas,botas,bata,guantes,comedor,prestamo from datosDestajo where nomEmpleado like '%" + empleado + "%';", null, dataValues);
                Classes.Datos_Empleados employeeData = new Classes.Datos_Empleados();
                Classes.Movimientos_Destajo employeeMovs = new Classes.Movimientos_Destajo();

                //Carga de datos a cada miembro de las clases
                employeeData.nombre = dt.Rows[0].ItemArray[0].ToString();
                employeeData.nss = dt.Rows[0].ItemArray[1].ToString();
                employeeData.rfc = dt.Rows[0].ItemArray[2].ToString();
                employeeData.curp = dt.Rows[0].ItemArray[3].ToString();
                employeeData.nCajas = Convert.ToInt32(caj.Rows[0].ItemArray[0].ToString());
                employeeData.depto = dt.Rows[0].ItemArray[4].ToString();
                employeeData.puesto = dt.Rows[0].ItemArray[5].ToString();
                employeeData.iniPeriodo = DateTime.Now.AddDays(-6.0);
                employeeData.finPeriodo = DateTime.Now;
                employeeData.diasLab = 6;

                employeeMovs.sueldo = (Convert.ToDouble(caj.Rows[0].ItemArray[0].ToString()) * Convert.ToDouble(txtCostoCaja.Text));
                employeeMovs.aguinaldo = Convert.ToDouble(txtAguinaldoD.Text);
                employeeMovs.vacaciones = Convert.ToDouble(txtVacacionesD.Text);
                employeeMovs.prDominical = Convert.ToDouble(txtDominicalD.Text);
                employeeMovs.prVacacional = Convert.ToDouble(txtVacacionalD.Text);
                employeeMovs.montoPercep = (employeeMovs.sueldo + employeeMovs.aguinaldo + employeeMovs.vacaciones + employeeMovs.prDominical + employeeMovs.prVacacional);

                int w = 0;


                foreach (DataColumn col in ded.Columns)
                {
                    if (Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString()) > 0)
                    {
                        if (employeeMovs.desc1 <= 0)
                        {
                           // MessageBox.Show("desc1 está vacío ...");
                            employeeMovs.nom1 = array[w];
                            employeeMovs.desc1 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                        }
                        else
                        {
                            if (employeeMovs.desc2 <= 0)
                            {
                                //MessageBox.Show("desc2 está vacío ...");
                                employeeMovs.nom2 = array[w];
                                employeeMovs.desc2 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                            {
                                if (employeeMovs.desc3 <= 0)
                                {
                                    //MessageBox.Show("desc3 está vacío ...");
                                    employeeMovs.nom3 = array[w];
                                    employeeMovs.desc3 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    if (employeeMovs.desc4 <= 0)
                                {
                                    //MessageBox.Show("desc4 está vacío ...");
                                    employeeMovs.nom4 = array[w];
                                    employeeMovs.desc4 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    if (employeeMovs.desc5 <= 0)
                                {
                                    //MessageBox.Show("desc5 está vacío ...");
                                    employeeMovs.nom5 = array[w];
                                    employeeMovs.desc5 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    if (employeeMovs.desc6 <= 0)
                                {
                                    //MessageBox.Show("desc6 está vacío ...");
                                    employeeMovs.nom6 = array[w];
                                    employeeMovs.desc6 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    if (employeeMovs.desc7 <= 0)
                                {
                                    //MessageBox.Show("desc7 está vacío ...");
                                    employeeMovs.nom7 = array[w];
                                    employeeMovs.desc7 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    if (employeeMovs.desc8 <= 0)
                                {
                                    //MessageBox.Show("desc8 está vacío ...");
                                    employeeMovs.nom8 = array[w];
                                    employeeMovs.desc8 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    if (employeeMovs.desc9 <= 0)
                                {
                                    //MessageBox.Show("desc9 está vacío ...");
                                    employeeMovs.nom9 = array[w];
                                    employeeMovs.desc9 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                                }
                                else
                                    MessageBox.Show("Lista de deducciones llena ...");
                            }
                        }
                    }
                    w++;
                }

                employeeMovs.montoDeducc = (employeeMovs.desc1 + employeeMovs.desc2 + employeeMovs.desc3 + employeeMovs.desc4 + employeeMovs.desc5 + employeeMovs.desc6 + employeeMovs.desc7 + employeeMovs.desc8 + employeeMovs.desc9);

                //MessageBox.Show("Numero de elementos en lista: " + discountsList.Count());
                //employeeMovs.nom1 = discountsList[0];

                //Hago una instancia del formulario Nomina_Individual y agrego los miembros de las clases a las list del nuevo form.
                Nomina_Individual frm = new Nomina_Individual();
                frm.obj.Add(employeeData);
                frm.objMovimientos.Add(employeeMovs);

                frm.Show();


            });
        }

        private void dataOnNominareport()
        {
            string[] array = { "Cuchilllo", "Cofia", "Escafandra", "Mandil", "Cubrebocas", "Botas", "Bata", "Guantes", "Descuento Comedor", "Prestamo Empresa" };
            List<String> discountsList = new List<string>();
            DataTable dt = sql.SQLdata("Select nombre,nss,rfc,curp,area_laboral,puesto from personal where nombre like '%" + listaEmpleadosDestajo1.SelectedItem.ToString() + "%';", null, dataValues);
            DataTable ded = sql.SQLdata("select cuchillo,cofia,escafandra,mandil,cubrebocas,botas,bata,guantes,comedor,prestamo from datosDestajo where nomEmpleado like '%" + listaEmpleadosDestajo1.SelectedItem.ToString() + "%';", null, dataValues);
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

            int w = 0;


            foreach (DataColumn col in ded.Columns)
            {
                if (Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString()) > 0)
                {
                    if (employeeMovs.desc1 <= 0)
                    {
                        MessageBox.Show("desc1 está vacío ...");
                        employeeMovs.nom1 = array[w];
                        employeeMovs.desc1 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                    }
                    else
                    {
                        if(employeeMovs.desc2 <= 0)
                        {
                            MessageBox.Show("desc2 está vacío ...");
                            employeeMovs.nom2 = array[w];
                            employeeMovs.desc2 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                        }
                        else
                        {
                            if(employeeMovs.desc3 <= 0)
                            {
                                MessageBox.Show("desc3 está vacío ...");
                                employeeMovs.nom3 = array[w];
                                employeeMovs.desc3 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                if (employeeMovs.desc4 <= 0)
                            {
                                MessageBox.Show("desc4 está vacío ...");
                                employeeMovs.nom4 = array[w];
                                employeeMovs.desc4 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                if (employeeMovs.desc5 <= 0)
                            {
                                MessageBox.Show("desc5 está vacío ...");
                                employeeMovs.nom5 = array[w];
                                employeeMovs.desc5 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                if (employeeMovs.desc6 <= 0)
                            {
                                MessageBox.Show("desc6 está vacío ...");
                                employeeMovs.nom6 = array[w];
                                employeeMovs.desc6 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                if (employeeMovs.desc7 <= 0)
                            {
                                MessageBox.Show("desc7 está vacío ...");
                                employeeMovs.nom7 = array[w];
                                employeeMovs.desc7 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                if (employeeMovs.desc8 <= 0)
                            {
                                MessageBox.Show("desc8 está vacío ...");
                                employeeMovs.nom8 = array[w];
                                employeeMovs.desc8 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                if (employeeMovs.desc9 <= 0)
                            {
                                MessageBox.Show("desc9 está vacío ...");
                                employeeMovs.nom9 = array[w];
                                employeeMovs.desc9 = Convert.ToDouble(ded.Rows[0].ItemArray[w].ToString());
                            }
                            else
                                MessageBox.Show("Lista de deducciones llena ...");
                        }
                    }
                }
                w++;
            }

            employeeMovs.montoDeducc = (employeeMovs.desc1 + employeeMovs.desc2 + employeeMovs.desc3 + employeeMovs.desc4 + employeeMovs.desc5 + employeeMovs.desc6 + employeeMovs.desc7 + employeeMovs.desc8 + employeeMovs.desc9);

            MessageBox.Show("Numero de elementos en lista: " + discountsList.Count());
            //employeeMovs.nom1 = discountsList[0];

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

        private void btnMostrarRecibo_Click(object sender, EventArgs e)
        {
            String cad = DateTime.Today.ToShortDateString();
            MessageBox.Show("Fecha del sistema: " + cad);
            dataOnNominareport();
        }

        private void groupBox4_Enter(object sender, EventArgs e){}

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
            generateDataList();
        }

        
        // ======================================================== Métodos en seccion jornada diurna
        private void tabJornadaDia_Click(object sender, EventArgs e){}

        private void LLenarGrid(string archivo, string hoja)
        {
            //declaramos las variables         
            OleDbConnection conexion = null;
            DataSet dataSet = null;
            OleDbDataAdapter dataAdapter = null;
            string consultaHojaExcel = "Select * from [" + hoja + "$]";

            //esta cadena es para archivos excel 2007 y 2010
            string cadenaConexionArchivoExcel = "provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + archivo + "';Extended Properties=Excel 12.0;";

            //para archivos de 97-2003 usar la siguiente cadena
            //string cadenaConexionArchivoExcel = "provider=Microsoft.Jet.OLEDB.4.0;Data Source='" + archivo + "';Extended Properties=Excel 8.0;";

            //Validamos que el usuario ingrese el nombre de la hoja del archivo de excel a leer
            if (string.IsNullOrEmpty(hoja))
            {
                MessageBox.Show("No hay una hoja para leer");
            }
            else
            {
                try
                {
                    //Si el usuario escribio el nombre de la hoja se procedera con la busqueda
                    conexion = new OleDbConnection(cadenaConexionArchivoExcel);//creamos la conexion con la hoja de excel
                    conexion.Open(); //abrimos la conexion
                    dataAdapter = new OleDbDataAdapter(consultaHojaExcel, conexion); //traemos los datos de la hoja y las guardamos en un dataSdapter
                    dataSet = new DataSet(); // creamos la instancia del objeto DataSet
                    dataAdapter.Fill(dataSet, hoja);//llenamos el dataset
                    dataGridView1.DataSource = dataSet.Tables[0]; //le asignamos al DataGridView el contenido del dataSet
                    conexion.Close();//cerramos la conexion
                    dataGridView1.AllowUserToAddRows = false;       //eliminamos la ultima fila del datagridview que se autoagrega
                }
                catch (Exception ex)
                {
                    //en caso de haber una excepcion que nos mande un mensaje de error
                    MessageBox.Show("Error, Verificar el archivo o el nombre de la hoja", ex.Message);
                }
            }
        }

        private void btnBuscarArchivo_Click(object sender, EventArgs e)
        {
            //creamos un objeto OpenDialog que es un cuadro de dialogo para buscar archivos
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Archivos de Excel (*.xls;*.xlsx)|*.xls;*.xlsx"; //le indicamos el tipo de filtro en este caso que busque
                                                                             //solo los archivos excel

            dialog.Title = "Seleccione el archivo de Excel";//le damos un titulo a la ventana

            dialog.FileName = string.Empty;//inicializamos con vacio el nombre del archivo

            //si al seleccionar el archivo damos Ok
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //el nombre del archivo sera asignado al textbox
                txtArchivo.Text = dialog.FileName;
                excelWorksheet.readWoorkSheet(dgvHorarios, txtArchivo.Text);
            }
        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void btnNominaMasiva_Click(object sender, EventArgs e)
        {
            showAllPaySheet();
        }

        private void btnSaveDestajoConfig_Click(object sender, EventArgs e)
        {
            ConfigurationManager.AppSettings.Set("keyCajas", txtCostoCaja.Text);
        }

        private void loadSettings()
        {
            txtAguinaldoD.Text = ConfigurationManager.AppSettings.Get("keyAguinaldo");
            txtVacacionesD.Text = ConfigurationManager.AppSettings.Get("keyVacaciones");
            txtDominicalD.Text = ConfigurationManager.AppSettings.Get("keySeptimoDia");
            txtVacacionalD.Text = ConfigurationManager.AppSettings.Get("keyPrimaVacacional");
            txtCostoCaja.Text = ConfigurationManager.AppSettings.Get("keyCostoCaja");
        }

        //private List<Classes.Resumen_Periodo> FillDgv()
        private void generateDataList()
        {
            String instruccion = "select * from vw_resumen_pagos_periodo where empleado <> 'NULL';";
            DataTable SQLresult = sql.SQLdata(instruccion, null, dataValues);
            

            class_Parametros = new Classes.Parametros_Reporte();

            class_Parametros.iniPeriodo = DateTime.Now.AddDays(-6.0);
            class_Parametros.finPeriodo = DateTime.Now;
            

            foreach (DataRow fila in SQLresult.Rows)
            {
                //Instancio un objeto de la clase y agrego datos de la fila correspondiente
                Classes.Resumen_Periodo item = new Classes.Resumen_Periodo();   
                item.nomEmpleado = SQLresult.Rows[indexR].ItemArray[0].ToString();
                item.nCajas = Convert.ToInt32(SQLresult.Rows[indexR].ItemArray[1].ToString());
                item.percepcion = Convert.ToDouble(SQLresult.Rows[indexR].ItemArray[2].ToString());
                item.deduccion = Convert.ToDouble(SQLresult.Rows[indexR].ItemArray[3].ToString());
                item.netoPago = Convert.ToDouble(SQLresult.Rows[indexR].ItemArray[4].ToString());

                
                MessageBox.Show(item.nomEmpleado + ", " + item.nCajas + ", " + item.percepcion + ", " + item.deduccion + ", " + item.netoPago);

                class_Parametros.detallePagos.Add(item);
                indexR++;
                
            }

            frmReporte_Acumulados new_report = new frmReporte_Acumulados();
            //new_report.Invoice.Add(item);
            
            new_report.Header.Add(class_Parametros);
            //
            //Enviamos el detalle de la Factura, como Detail es una lista e invoide.Details tambien
            //es un lista del tipo EArticulo bastara con igualarla
            //
            new_report.Invoice = class_Parametros.detallePagos;

            new_report.Show();
        }
        
        private string generaScript()
        {
            string query, cad = "SELECT id,nombre,area_laboral,puesto,status FROM personal ";
            string where = "WHERE ", order = "ORDER BY ";
            try
            {
                if (opcTodos.Checked == true)
                {
                    if (opcClave.Checked == true)
                        order = order + "id;";
                    else
                    {
                        if (opcNombre.Checked == true)
                        {
                            order = order + "nombre;";
                        }
                        else
                        {
                            if (opcDepto.Checked == true)
                            {
                                order = order + "area_laboral;";
                            }
                            else
                            {
                                if (opcStatus.Checked == true)
                                {
                                    order = order + "status;";
                                }
                                else
                                {
                                    order = "";
                                }
                            }
                        }
                    }
                    query = cad + order;
                    return query;
                    //generarCatEmpleados(cad);
                }
                else
                {

                    if (opcClave.Checked == true)
                        order = order + "id;";
                    else
                    {
                        if (opcNombre.Checked == true)
                        {
                            order = order + "nombre;";
                        }
                        else
                        {
                            if (opcDepto.Checked == true)
                            {
                                order = order + "area_laboral;";
                            }
                            else
                            {
                                if (opcStatus.Checked == true)
                                {
                                    order = order + "status;";
                                }
                                else
                                {
                                    order = "";
                                }
                            }
                        }
                    }
                    query = cad + "WHERE status = 'ALTA' " + order;
                    return query;
                    //generarCatEmpleados(cad);
                }
            }
            catch(Exception ee)
            {
                return (ee.Message);
            }
        }
        public void generarCatEmpleados(string param)
        {
            //string instruccion = "select id,nombre,area_laboral,puesto,status from personal";
            string instruccion = param;
            MessageBox.Show(instruccion);
            DataTable dt = sql.SQLdata(instruccion, null, dataValues);
            class_Parametros = new Classes.Parametros_Reporte();

            foreach (DataRow fila in dt.Rows)
            {
                //Instancio un objeto de la clase y agrego datos de la fila correspondiente
                Classes.cat_Empleados item = new Classes.cat_Empleados();
                item.clave = Convert.ToInt32(dt.Rows[indexR].ItemArray[0].ToString());
                item.nombre =dt.Rows[indexR].ItemArray[1].ToString();
                item.depto = dt.Rows[indexR].ItemArray[2].ToString();
                item.puesto = dt.Rows[indexR].ItemArray[3].ToString();
                item.status = dt.Rows[indexR].ItemArray[4].ToString();

                class_Parametros.catEmpleados.Add(item);
                indexR++;
            }

            frmCatalogoEmpleados new_report = new frmCatalogoEmpleados();
            //new_report.Invoice.Add(item);

            new_report.Header.Add(class_Parametros);
            //
            //Enviamos el detalle de la Factura, como Detail es una lista e invoide.Details tambien
            //es un lista del tipo EArticulo bastara con igualarla
            //
            new_report.detalle_catalogo = class_Parametros.catEmpleados;

            new_report.Show();

        }

        public void recibeParametro(string cad)
        {
            MessageBox.Show("Parametro Recibido ... " + cad);
            generarCatEmpleados(cad);
        }

        

        private void btnActualizarListas_Click(object sender, EventArgs e)
        {
            obtenerEmpleados();
        }

        private void btnCatEmpleados_Click(object sender, EventArgs e)
        {
            pnlReport.Visible = true;
        }

        private void btnCerrarPanel_Click(object sender, EventArgs e)
        {
            pnlReport.Visible = false;
        }

        private void btnGuardarOpc_Click(object sender, EventArgs e)
        {
            indexR = 0;
            string parametro = generaScript();
            generarCatEmpleados(parametro);
            pnlReport.Visible = false;
        }

        /*
        ===============================================================================================================================
                                                    Clases para trabajar con archivos excel
        ===============================================================================================================================
        */

        public class excelWorksheet
        {
            public static object missVal = System.Reflection.Missing.Value;

            public static excel.Application start()
            {
                try
                {
                    excel.Application xlApp = new excel.Application();
                    if (xlApp == null)
                    {
                        MessageBox.Show("Necesitas instalar Excel");
                        return null;
                    }
                    return xlApp;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return null;
                }
            }

            public static excel.Workbook createExcel()
            {
                excel.Application xlApp = start();
                try
                {
                    excel.Workbook xlWorkBook = xlApp.Workbooks.Add(missVal);
                    xlApp.Visible = true;
                    return xlWorkBook;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return null;
                }
            }

            public static void readWoorkSheet(DataGridView dgv, string path)
            {
                data.DataTable dt = new data.DataTable();
                excel.Application xlApp = start();
                try
                {
                    excel.Workbook xlWorkBook = xlApp.Workbooks.Open(path, 0, true, 5, "", "", true, excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                    excel.Worksheet xlWorkSheet = (excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
                    excel.Range range = xlWorkSheet.UsedRange;
                    for (int i = 1; i <= range.Columns.Count; i++)
                        dt.Columns.Add(range.Cells[1, i].Value2);
                    for (int i = 2; i <= range.Rows.Count; i++)
                    {
                        data.DataRow dr = dt.NewRow();
                        for (int j = 1; j <= range.Columns.Count; j++)
                            dr[j - 1] = ((range.Cells[i, j] as excel.Range).Value2).ToString();
                        dt.Rows.Add(dr);
                    }
                    dgv.DataSource = dt;
                    xlWorkBook.Close(false, null, null);
                    xlApp.Quit();
                    releaseObject(xlWorkSheet);
                    releaseObject(xlWorkBook);
                    releaseObject(xlApp);
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

            public static excel.Worksheet createWoorkSheet(data.DataTable dt = null, DataGridView dgv = null)
            {
                excel.Workbook xlWorkBook = createExcel();
                excel.Worksheet xlWorkSheet = new excel.Worksheet();
                try
                {
                    if (xlWorkBook != null)
                    {
                        xlWorkSheet = (excel.Worksheet)xlWorkBook.Sheets[1];
                        fillExcel(xlWorkSheet, dt, dgv);
                        xlWorkSheet.Activate();
                        xlWorkBook.Saved = false;
                    }
                    return xlWorkSheet;
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                    return null;
                }
            }

            public static void fillExcel(excel.Worksheet xlWorkSheet, data.DataTable dt = null, DataGridView dgv = null)
            {
                try
                {
                    if (dt == null)
                    {
                        dt = new data.DataTable();
                        foreach (DataGridViewColumn column in dgv.Columns)
                        {
                            data.DataColumn col = new data.DataColumn(column.Name);
                            dt.Columns.Add(col);
                        }
                        foreach (DataGridViewRow row in dgv.Rows)
                        {
                            data.DataRow dr = dt.NewRow();
                            for (int i = 0; i < dgv.ColumnCount; i++)
                                dr[i] = row.Cells[i].Value.ToString();
                            dt.Rows.Add(dr);
                        }
                    }
                    int c = 1;
                    foreach (data.DataColumn column in dt.Columns)
                    {
                        xlWorkSheet.Cells[1, c] = column.ColumnName;
                        c++;
                    }
                    for (int i = 2; i <= dt.Rows.Count; i++)
                        for (int j = 1; j <= dt.Columns.Count; j++)
                            xlWorkSheet.Cells[i, j] = dt.Rows[i - 2].ItemArray[j - 1].ToString();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }

            private static void releaseObject(object obj)
            {
                try
                {
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                    obj = null;
                }
                catch (Exception ex)
                {
                    obj = null;
                    MessageBox.Show("Unable to release the Object " + ex.ToString());
                }
                finally
                {
                    GC.Collect();
                }
            }
        }
    }
}