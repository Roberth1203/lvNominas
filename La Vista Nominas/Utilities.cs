using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace La_Vista_Nominas
{
    class Utilities
    {
        public static SqlConnection connectToSQL()
        {
            try
            {
                string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
                SqlConnection remoteConnection = new SqlConnection(dataValues);
                remoteConnection.Open();
                MessageBox.Show("Conectado !");
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

        public void SQLstatement(string instruction, SqlConnection conn = null, string connectionValues = null) //SQL insert, update or delete
        {
            try
            {
                if (conn == null)

                    conn = connectToSQL();
                SqlCommand cmd = new SqlCommand(instruction, conn);
                cmd.ExecuteNonQuery();
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

        
    }
}
