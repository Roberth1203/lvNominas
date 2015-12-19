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
        public IniciarSesion()
        {
            InitializeComponent();
            connectToDatabase();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Pantalla_Principal start = new Pantalla_Principal();
            start.Show();
            this.Close();
        }

        public void connectToDatabase()
        {
            try
            {
                string dataConnection = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True" ;
                con.ConnectionString = dataConnection;
                con.Open();
                MessageBox.Show("Conexión exitosa !!!");
                mostrarDataTable();
            }
            catch(SqlException e)
            {
                MessageBox.Show("Error: " + e);
            }
            
        }

        public void mostrarDataTable()
        {
            DataTable dat = cargarRegistros("Select * from users");

            for( int index = 0; index < dat.Rows.Count;index ++)
            {
                MessageBox.Show("Elemento en la posición " + index + ": " + dat.Rows[index].ToString());
            }
        }

        public DataTable cargarRegistros(string instruction)
        {
            DataTable dt = new DataTable();
            try
            {
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(instruction);
                    adapter.Fill(dt);
                    con.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("Error: " + e.Message);
            }
            return dt;
            
        }
    }
}
