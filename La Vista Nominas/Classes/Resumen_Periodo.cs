using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace La_Vista_Nominas.Classes
{
    public class Resumen_Periodo
    {
        public string nomEmpleado { get; set; }
        public int nCajas { get; set; }
        public double percepcion { get; set; }
        public double deduccion { get; set; }
        public double netoPago { get; set; }
    }
}
