﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using data = System.Data;
using excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Configuration;

namespace La_Vista_Nominas
{
    class Utilities
    {
        string connectionString = ConfigurationManager.AppSettings.Get("rutaBD");
        public static object missVal = System.Reflection.Missing.Value;

        public static SqlConnection connectToSQL()
        {
            try
            {
                //string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
                String dataValues = ConfigurationManager.ConnectionStrings["La_Vista_Nominas.Properties.Settings.nominaConnectionString"].ConnectionString;
                SqlConnection remoteConnection = new SqlConnection(dataValues);
                remoteConnection.Open();
                //MessageBox.Show("Conectado !");
                return remoteConnection;
            }
            catch (SqlException e)
            {
                MessageBox.Show("Error SQL: " + e);
                return null;
            }
        }

        public static void closeSQL(SqlConnection connection)
        {
            try
            {
                if (connection != null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error SQL:" + ex, "La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public DataTable SQLdata(string instruction, SqlConnection conn, string dataConnection)
        {
            DataTable dt = new DataTable();

            try
            {
                if (conn == null)
                {
                    conn = connectToSQL();
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(instruction, conn);
                    adapter.Fill(dt);
                    if (dataConnection != null)
                        closeSQL(conn);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error capturado: " + e.Message, "La Vista Alimentos S.A. de C.V", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return dt;
        }

        public void SQLstatement(String instruction, SqlConnection conn = null, string connectionValues = null) //SQL insert, update or delete
        {
            try
            {
                if (conn == null)
                { 
                    conn = connectToSQL();
                    SqlCommand cmd = new SqlCommand(instruction, conn);
                    cmd.ExecuteNonQuery();
                }
                if (connectionValues != null)
                    closeSQL(conn);

            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }
        }

        public static SqlDataReader sqlReader(string instruction,SqlConnection conn = null, string connectionValues = null)  
        {
            SqlDataReader reader = null;
            try
            {
                if (conn == null)
                    conn = connectToSQL();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = instruction;
                reader = cmd.ExecuteReader();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error capturado: " + e.Message);
            }
            return (reader);
        }

        public int nextId(string field, string table,SqlConnection conn = null,string connectionValues = null)
        {
            string instruction = "SELECT MAX (" + field + ") FROM " + table;
            int id = 0;
            try
            {
                if (conn == null)
                    conn = connectToSQL();
                SqlDataReader reader = sqlReader(instruction.Replace("max", "count"), null,connectionValues);
                reader.Read();
                if (Convert.ToInt32(reader.GetValue(0)) == 0)
                    id = 0;
                else
                {
                    reader = sqlReader(instruction, null, connectionValues);
                    reader.Read();
                    id = Convert.ToInt32(reader.GetValue(0));
                }
                if (connectionValues != null)
                    closeSQL(conn);
                id = id + 1;
            }
            catch(Exception e)
            {
                MessageBox.Show("Error capturado: " + e.Message);
            }
            return id;
        }

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

        public void openExcelFile(DataGridView grid)
        {
            string path;
            List<string> listaHorarios = new List<string>();
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
                path = dialog.FileName;
                readWoorkSheet(grid, path);
            }
        }
    }
}
