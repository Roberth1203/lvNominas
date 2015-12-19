using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace La_Vista_Nominas
{
    public partial class IniciarSesion : Form
    {
        SqlConnection con = new SqlConnection();
        DataTable dt = new DataTable();
        string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";

        public IniciarSesion()
        {
            InitializeComponent();
            
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Pantalla_Principal start = new Pantalla_Principal();
            start.Show();
            cargaDatos();
            this.Close();
        }

        public static SqlConnection connectToSQL(string connectionValues)
        {
            try
            {
                string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
                SqlConnection remoteConnection = new SqlConnection(dataValues);
                remoteConnection.Open();
                MessageBox.Show("Conectado a lvserver","Visual Studio dice: ",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return remoteConnection;
            }
            catch(SqlException e)
            {
                MessageBox.Show("Error SQL: " + e);
                return null;
            }
        }

        public static void closeSQL(SqlConnection connection)
        {
            try
            {
                if(connection != null)
                {
                    connection.Close();
                    SqlConnection.ClearPool(connection);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error SQL:" + ex,"La Vista Alimentos S.A. de C.V.",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        public DataTable SQLdata(string instruction, SqlConnection conn,string dataConnection)
        {
            DataTable dt = new DataTable();

            try
            {
                if(conn == null)
                {
                    conn = connectToSQL(dataConnection);
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(instruction, conn);
                    adapter.Fill(dt);
                    if (dataConnection != null)
                        closeSQL(conn);
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Error capturado: " + e.Message,"La Vista Alimentos S.A. de C.V",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            return dt;
        }

        public void cargaDatos()
        {
            string[] arr = new string[2];
            try
            {
                string instruccion = "select * from users where usuario ='" + txtUser.Text + "'";
                DataTable dt = SQLdata(instruccion, null, dataValues);
                
                if (!dt.Rows[0].ItemArray[0].Equals(""))
                {
                    // Cargo datos del DataTable a textbox para presentar informacion
                    arr[0] = dt.Rows[0].ItemArray[1].ToString();
                    arr[1] = dt.Rows[0].ItemArray[3].ToString();
                    
                    if(arr[0].ToString() == txtUser.Text && arr[1].ToString() == txtPassword.Text)
                    {
                        MessageBox.Show("Bienvenido","La Vista Alimentos S.A. de C.V.",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Su usuario o contraseña no son correctos \nverifique por favor !!","La Vista Alimentos S.A. de C.V.",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("No se encontró nada !!");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error capturado: " + ex.Message, "La Vista Alimentos S.A. de C.V", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}