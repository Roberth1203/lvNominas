using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace La_Vista_Nominas
{
    public partial class nominaHoras : Form
    {
        public List<employeeClasses.datosEmpleadosHoras> datosReporte = new List<employeeClasses.datosEmpleadosHoras>();

        public nominaHoras()
        {
            InitializeComponent();
        }

        private void nominaHoras_Load(object sender, EventArgs e)
        {

            this.rpViewerHoras.RefreshReport();
        }
    }
}
