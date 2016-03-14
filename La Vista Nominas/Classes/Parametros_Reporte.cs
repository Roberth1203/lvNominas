using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace La_Vista_Nominas.Classes
{
    public class Parametros_Reporte
    {
        public DateTime iniPeriodo { get; set; }
        public DateTime finPeriodo { get; set; }
        public List<Resumen_Periodo> detallePagos = new List<Resumen_Periodo>();
    }
}
