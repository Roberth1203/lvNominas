using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using data = System.Data;
using excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Configuration;

namespace La_Vista_Nominas
{
    public partial class Cargar_Lista_Asistencia : Form
    {
        Utilities sql = new Utilities();
        string connectionString = ConfigurationManager.AppSettings.Get("rutaBD");
        String dataValues = ConfigurationManager.ConnectionStrings["La_Vista_Nominas.Properties.Settings.nominaConnectionString"].ConnectionString;
        int indexR;
        public Cargar_Lista_Asistencia()
        {
            InitializeComponent();
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

        private void addHours()
        {
            try
            {
                string query = "INSERT INTO HORARIOS_EMPLEADOS VALUES ('" + dgvHorarios.Rows[indexR].Cells["Empleado"].Value + "','" + dgvHorarios.Rows[indexR].Cells["Horas Laboradas"].Value + "');";
                sql.SQLstatement(query, null, connectionString);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            List<string> listaHorarios = new List<string>();
            indexR = 0;
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
                dgvHorarios.Columns[0].Width = 200;
                dgvHorarios.Columns[1].Width = 80;
                dgvHorarios.Columns[indexR].DefaultCellStyle.Format = "hh:mm";
            }
            
            addHours();
            indexR = 0;
        }

        private void button1_Click(object sender, EventArgs e) // hace referencia al control btnGuardar
        {
            try
            {
                /*
                int i = 0;
                foreach (DataGridViewRow file in dgvHorarios.Rows)
                {
                    MessageBox.Show("Horas laboradas de: " + dgvHorarios.Rows[i].Cells["Empleado"].Value.ToString() + " son: " + dgvHorarios.Rows[i].Cells["Horas Laboradas"].Value.ToString());
                    i++;
                }
                */

                //Cargo los datos a la tabla perdep_deduc_emp
                DateTime date = DateTime.Now;
                String fecha = date.ToShortDateString();
                string query = "INSERT INTO percep_deduc_Emp (nomEmpleado,totalHoras,ultimaModificacion)values('" +
                                dgvHorarios.Rows[0].Cells["Empleado"].Value.ToString() + "'," +
                                dgvHorarios.Rows[0].Cells["Horas Laboradas"].Value.ToString() + ",'" +
                                fecha + "');";

                sql.SQLstatement(query, null, connectionString);
                MessageBox.Show("Datos Almacenados !!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error SQL \n" + ex.Message);
            }
        }
    }
}
