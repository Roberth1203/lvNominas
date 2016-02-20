
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace La_Vista_Nominas.Classes
{
    public class Datos_Empleados
    {
        public string nombre { get; set; }
        public string nss { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public int nCajas { get; set; }
        public string depto { get; set; }
        public string puesto { get; set; }
        public DateTime iniPeriodo { get; set; }
        public DateTime finPeriodo { get; set; }
        public int diasLab { get; set; }
    }
}
