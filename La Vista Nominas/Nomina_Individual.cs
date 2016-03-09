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
    public partial class Nomina_Individual : Form
    {
        public List<Classes.Datos_Empleados> obj = new List<Classes.Datos_Empleados>();
        public List<Classes.Movimientos_Destajo> objMovimientos = new List<Classes.Movimientos_Destajo>();

        public Nomina_Individual()
        {
            InitializeComponent();
        }

        private void Nomina_Individual_Load(object sender, EventArgs e)
        {
            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", obj));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", objMovimientos));

            this.reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            
            this.reportViewer1.RefreshReport();
        }
    }
}
