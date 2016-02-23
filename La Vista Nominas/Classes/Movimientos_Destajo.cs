using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace La_Vista_Nominas.Classes
{
    public class Movimientos_Destajo
    {
        public int idEmp { get; set; }
        public String nomEmp { get; set; }
        public int cajas { get; set; }
        public double sueldo { get; set; }
        public double aguinaldo { get; set; }
        public double vacaciones { get; set; }
        public double prDominical { get; set; }
        public double prVacacional { get; set; }

        public double desc1 { get; set; }
        public double desc2 { get; set; }
        public double desc3 { get; set; }
        public double desc4 { get; set; }
        public double desc5 { get; set; }
        public double desc6 { get; set; }
        public double desc7 { get; set; }
        public double desc8 { get; set; }
        public double desc9 { get; set; }
        public double desc10 { get; set; }
        
        public double montoPercep { get; set; }
        public double montoDeducc { get; set; }

        public double netoPago { get; set; }
    }
}
