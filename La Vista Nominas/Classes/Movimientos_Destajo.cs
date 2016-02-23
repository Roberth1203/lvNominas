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

        public double dedCubreB { get; set; }
        public double dedEscaf { get; set; }
        public double dedGuantes { get; set; }
        public double dedBata { get; set; }
        public double dedBotas { get; set; }
        public double dedMandil { get; set; }
        public double dedCuchillo { get; set; }
        public double dedCofia { get; set; }
        public double dedComedor { get; set; }
        public double dedPrestamo { get; set; }
        
        public double montoPercep { get; set; }
        public double montoDeducc { get; set; }

        public double netoPago { get; set; }
    }
}
