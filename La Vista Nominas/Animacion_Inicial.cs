using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace La_Vista_Nominas
{
    public partial class Animacion_Inicial : Form
    {
        public Animacion_Inicial()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.Write("Inicia carga de form !!!");
            timer1.Enabled = true;
            label1.BackColor = Color.Transparent;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            temporizarProgress();
         /*   
            if (progressBar1.Value < 100)
            {
                progressBar1.Value = progressBar1.Value + 2;
                label1.Text = "Cargando componentes... " + progressBar1.Value + "%";
            }
            else
            {
                timer1.Enabled = false;
                System.Threading.Thread.Sleep(2000);

                IniciarSesion log = new IniciarSesion();
                log.Show();
                this.Hide();
            }
            */
        }

        private void temporizarProgress()
        {
            if (circularProgress1.Value < 100)
            {
                circularProgress1.Value = circularProgress1.Value + 2;
                //label1.Text = "Cargando componentes... " + circularProgress1.Value + "%";
                //label1.Text = circularProgress1.Value + "%";
            }
            else
            {
                timer1.Enabled = false;
                System.Threading.Thread.Sleep(2000);

                IniciarSesion log = new IniciarSesion();
                log.Show();
                this.Hide();
            }
        }
    }
}
