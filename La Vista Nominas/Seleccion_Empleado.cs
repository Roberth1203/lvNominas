using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace La_Vista_Nominas
{
    public partial class Seleccion_Empleado : Form
    {
        DataTable dt;
        Utilities sql = new Utilities();
        string dataValues = "Data Source=lvserver \\" + "sqlexpress;Initial Catalog=nomina;Integrated Security=True";
        public Seleccion_Empleado()
        {
            InitializeComponent();
        }
        public void Buscar()
        {
            if(cmbBuscar.SelectedItem.ToString().Equals("nombre"))
            {
                dt = new DataTable();
                dt = sql.SQLdata("select id AS ID_EMPLEADO,nombre AS NOMBRE_COMPLETO,status AS ACTIVO  where nombre like '%" + txtFiltrado.Text + "%'", null, dataValues);
                dataGridView1.DataSource = dt;
            }
            else if (cmbBuscar.SelectedItem.ToString().Equals("id"))
            {
                dt = new DataTable();
                dt = sql.SQLdata("select id AS ID_EMPLEADO,nombre AS NOMBRE_COMPLETO,status AS ACTIVO from personal where id = " + Convert.ToInt32(txtFiltrado.Text), null, dataValues);
                dataGridView1.DataSource = dt;
            }

        }

        private void txtFiltrado_TextChanged(object sender, EventArgs e)
        {
            Buscar();
        }
    }
}
