using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace La_Vista_Nominas
{
    public partial class frmOpcCatalogo : Form
    {
        string sentencia;

        public frmOpcCatalogo()
        {
            InitializeComponent();
        }

        public void revisar()
        {
            string cad = "SELECT id,nombre,area_laboral,puesto,status FROM personal ";
            string where = "WHERE ", order = "ORDER BY ";

            if (opcTodos.Checked == true)
            {
                if (opcClave.Checked == true)
                    order = order + "clave;";
                else
                {
                    if (opcNombre.Checked == true)
                    {
                        order = order + "nombre;";
                    }
                    else
                    {
                        if (opcDepto.Checked == true)
                        {
                            order = order + "area_laboral;";
                        }
                        else
                        {
                            if (opcStatus.Checked == true)
                            {
                                order = order + "status;";
                            }
                            else
                            {
                                order = "";
                            }
                        }
                    }
                }
                sentencia = cad + order;
                //generarCatEmpleados(cad);
            }
            else
            {

                if (opcClave.Checked == true)
                    order = order + "clave;";
                else
                {
                    if (opcNombre.Checked == true)
                    {
                        order = order + "nombre;";
                    }
                    else
                    {
                        if (opcDepto.Checked == true)
                        {
                            order = order + "area_laboral;";
                        }
                        else
                        {
                            if (opcStatus.Checked == true)
                            {
                                order = order + "status;";
                            }
                            else
                            {
                                order = "";
                            }
                        }
                    }
                }
                sentencia = cad + "WHERE status = 'ALTA' " + order;
                //generarCatEmpleados(cad);
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pantalla_Principal obj = new Pantalla_Principal();
            revisar();
            MessageBox.Show("sentencia:\n " + sentencia);
            obj.recibeParametro(sentencia);
            this.Close();
        }
    }
}
