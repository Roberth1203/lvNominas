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
    public partial class frmReporte_Acumulados : Form
    {
        public List<Classes.Parametros_Reporte> Header = new List<Classes.Parametros_Reporte>();
        public List<Classes.Resumen_Periodo> Invoice = new List<Classes.Resumen_Periodo>();
        
        public frmReporte_Acumulados()
        {
            InitializeComponent();
        }

        private void frmReporte_Acumulados_Load(object sender, EventArgs e)
        {
            //Limpiemos el DataSource del informe
            reportViewer1.LocalReport.DataSources.Clear();
            //
            //Establezcamos los parámetros que enviaremos al reporte
            //recuerde que son dos para el titulo del reporte y para el nombre de la empresa
            //
            //ReportParameter[] parameters = new ReportParameter[2];
            //parameters[0] = new ReportParameter("parameterTitulo", Titulo);
            //parameters[1] = new ReportParameter("parameterEmpresa", Empresa);

            //
            //Establezcamos la lista como Datasource del informe
            //
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", Header));
            reportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", Invoice));
            //
            //Enviemos la lista de parametros
            //
            //reportViewer1.LocalReport.SetParameters(parameters);
            //
            //Hagamos un refresh al reportViewer
            //
            this.reportViewer1.RefreshReport();
        }
    }
}
