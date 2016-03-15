using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace La_Vista_Nominas
{
    public partial class frmCatalogoEmpleados : Form
    {
        public List<Classes.Parametros_Reporte> Header = new List<Classes.Parametros_Reporte>();
        public List<Classes.cat_Empleados> detalle_catalogo = new List<Classes.cat_Empleados>();

        public frmCatalogoEmpleados()
        {
            InitializeComponent();
        }

        private void frmCatalogoEmpleados_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", Header));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", detalle_catalogo));
            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            this.reportViewer1.RefreshReport();
        }
    }
}
