﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace La_Vista_Nominas.employeeClasses
{
    class datosEmpleadosHoras
    {
        public String nssEmp { get; set; }
        public String rfcEmp { get; set; }
        public String curpEmp { get; set; }
        public String dptoEmp { get; set; }
        public String puestoEmp { get; set; }
        public DateTime periodoIni { get; set; }
        public DateTime periodoFin { get; set; }
        public Int32 diasLabores { get; set; }
        public Double totalPer { get; set; }
        public Double totalDed { get; set; }

        public Double idEmp { get; set; }
        public String nomEmp { get; set; }
        public Double perAguinaldo { get; set; }
        public Double perVacaciones { get; set; }
        public Double perPrimaVac { get; set; }
        public Double perDomingo { get; set; }
        public Double sueldo { get; set; }
        public Double perGratificacion { get; set; }

        public Double dedCuchillo { get; set; }
        public Double dedCofia { get; set; }
        public Double dedEscafandra { get; set; }
        public Double dedMandil { get; set; }
        public Double dedCubreB { get; set; }
        public Double dedBotas { get; set; }
        public Double dedBata { get; set; }
        public Double dedGuantes {get; set; }

        public Double desComedor { get; set; }
        public Double desEmpresa { get; set; }
        public Double desInfonavit { get; set; }
        
    }
}
