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
        public static SqlConnection connectToSQL(string connectionValues)
        {
            try
            {
                string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
                SqlConnection remoteConnection = new SqlConnection(dataValues);
                remoteConnection.Open();
                MessageBox.Show("Conectado a lvserver", "Visual Studio dice: ", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    conn = connectToSQL(dataConnection);
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

        public void SQLstatement(string instruction, SqlConnection conn = null, string connectionValues = null) //Firebird insert, update or delete
        {
            try
            {
                if (conn == null)

                    conn = connectToSQL(connectionValues);
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
    }
}
