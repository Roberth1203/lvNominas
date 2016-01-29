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
using System.Configuration;

namespace La_Vista_Nominas
{
    public partial class IniciarSesion : Form
    {
        SqlConnection con = new SqlConnection();
        DataTable dt = new DataTable();
        Pantalla_Principal start;
        //string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
        String dataValues = ConfigurationManager.ConnectionStrings["La_Vista_Nominas.Properties.Settings.nominaConnectionString"].ConnectionString;
        public IniciarSesion()
        {
            InitializeComponent();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            //cargaDatos();
            start = new Pantalla_Principal();
            start.Show();
            this.Close();
        }

        public static SqlConnection connectToSQL(string connectionValues)
        {
            try
            {
                //string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
                String dataValues = ConfigurationManager.ConnectionStrings["La_Vista_Nominas.Properties.Settings.nominaConnectionString"].ConnectionString;
                SqlConnection remoteConnection = new SqlConnection(dataValues);
                remoteConnection.Open();
                //MessageBox.Show("Conectado a lvserver","Visual Studio dice: ",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
            try
            {
                if(txtUser.Text == "" || txtPassword.Text == "")
                {
                    MessageBox.Show("El nombre de usuario o la contraseña no pueden estar vacíos \nVerfique por favor !!","Advertencia",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtUser.Focus();
                }
                else
                {
                    try
                    {
                        string instruccion = "select usuario,password from users where usuario ='" + txtUser.Text + "'";
                        DataTable dt = SQLdata(instruccion, null, dataValues);
                        MessageBox.Show(dt.Rows[0].ItemArray[0].ToString());

                        if(dt.Rows[0].ItemArray[1].Equals(txtPassword.Text))
                        {
                            MessageBox.Show("Bienvenido: " + txtUser.Text, "La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            start.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Contraseñe incorrecta !", "La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            txtPassword.SelectAll();
                        }
                        
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Usuario no registrado", "La Vista Alimentos S.A. de C.V.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtUser.SelectAll();
                    }
                    
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsLetter(e.KeyChar)) && (e.KeyChar != (char)Keys.Back))
            {
                MessageBox.Show("Solo se permiten letras", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Handled = true;
                return;
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            start = new Pantalla_Principal();
            if((int)e.KeyChar == (int)Keys.Enter)
            {
                cargaDatos();
            }
        }

        private void pictureBox4_MouseHover(object sender, EventArgs e)
        {
            pictureBox4.Image = imageList1.Images[1];
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = imageList1.Images[0];
        }
    }
}