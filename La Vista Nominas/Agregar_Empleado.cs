using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace La_Vista_Nominas
{
    public partial class Agregar_Empleado : Form
    {
        public Agregar_Empleado()
        {
            InitializeComponent();
        }

        private void btnGuardar_MouseHover(object sender, EventArgs e)
        {
            btnGuardar.Image = imageList1.Images[1];
        }

        private void btnGuardar_MouseLeave(object sender, EventArgs e)
        {
            btnGuardar.Image = imageList1.Images[0];
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNombre_Enter(object sender, EventArgs e)
        {
            pictureBox3.BackgroundImage = imageList1.Images[4];   
        }
    }
}
