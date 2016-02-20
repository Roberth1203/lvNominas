using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace La_Vista_Nominas.Classes
{
    public class Movimientos_Destajo
    {
        public int cajas { get; set; }
        public double sueldo { get; set; }
        public double aguinaldo { get; set; }
        public double vacaciones { get; set; }
        public double prDominical { get; set; }
        public double prVacacional { get; set; }

        public double montoPercep { get; set; }
        public double montoDeducc { get; set; }

        public double netoPago { get; set; }
    }
}
