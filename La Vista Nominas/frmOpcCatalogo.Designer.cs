namespace La_Vista_Nominas
{
    partial class frmOpcCatalogo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.opcTodos = new System.Windows.Forms.RadioButton();
            this.opcActivos = new System.Windows.Forms.RadioButton();
            this.opcClave = new System.Windows.Forms.RadioButton();
            this.opcNombre = new System.Windows.Forms.RadioButton();
            this.opcDepto = new System.Windows.Forms.RadioButton();
            this.opcStatus = new System.Windows.Forms.RadioButton();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.opcActivos);
            this.groupBox1.Controls.Add(this.opcTodos);
            this.groupBox1.ForeColor = System.Drawing.Color.SeaGreen;
            this.groupBox1.Location = new System.Drawing.Point(12, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(234, 52);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mostrar:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.opcStatus);
            this.groupBox2.Controls.Add(this.opcDepto);
            this.groupBox2.Controls.Add(this.opcNombre);
            this.groupBox2.Controls.Add(this.opcClave);
            this.groupBox2.ForeColor = System.Drawing.Color.SeaGreen;
            this.groupBox2.Location = new System.Drawing.Point(12, 61);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 76);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ordenar Por:";
            // 
            // opcTodos
            // 
            this.opcTodos.AutoSize = true;
            this.opcTodos.Location = new System.Drawing.Point(7, 20);
            this.opcTodos.Name = "opcTodos";
            this.opcTodos.Size = new System.Drawing.Size(126, 17);
            this.opcTodos.TabIndex = 0;
            this.opcTodos.TabStop = true;
            this.opcTodos.Text = "Todos los Empleados";
            this.opcTodos.UseVisualStyleBackColor = true;
            // 
            // opcActivos
            // 
            this.opcActivos.AutoSize = true;
            this.opcActivos.Location = new System.Drawing.Point(137, 20);
            this.opcActivos.Name = "opcActivos";
            this.opcActivos.Size = new System.Drawing.Size(84, 17);
            this.opcActivos.TabIndex = 1;
            this.opcActivos.TabStop = true;
            this.opcActivos.Text = "Solo Activos";
            this.opcActivos.UseVisualStyleBackColor = true;
            // 
            // opcClave
            // 
            this.opcClave.AutoSize = true;
            this.opcClave.Location = new System.Drawing.Point(7, 20);
            this.opcClave.Name = "opcClave";
            this.opcClave.Size = new System.Drawing.Size(52, 17);
            this.opcClave.TabIndex = 0;
            this.opcClave.TabStop = true;
            this.opcClave.Text = "Clave";
            this.opcClave.UseVisualStyleBackColor = true;
            // 
            // opcNombre
            // 
            this.opcNombre.AutoSize = true;
            this.opcNombre.Location = new System.Drawing.Point(137, 20);
            this.opcNombre.Name = "opcNombre";
            this.opcNombre.Size = new System.Drawing.Size(62, 17);
            this.opcNombre.TabIndex = 1;
            this.opcNombre.TabStop = true;
            this.opcNombre.Text = "Nombre";
            this.opcNombre.UseVisualStyleBackColor = true;
            // 
            // opcDepto
            // 
            this.opcDepto.AutoSize = true;
            this.opcDepto.Location = new System.Drawing.Point(7, 44);
            this.opcDepto.Name = "opcDepto";
            this.opcDepto.Size = new System.Drawing.Size(92, 17);
            this.opcDepto.TabIndex = 2;
            this.opcDepto.TabStop = true;
            this.opcDepto.Text = "Departamento";
            this.opcDepto.UseVisualStyleBackColor = true;
            // 
            // opcStatus
            // 
            this.opcStatus.AutoSize = true;
            this.opcStatus.Location = new System.Drawing.Point(137, 44);
            this.opcStatus.Name = "opcStatus";
            this.opcStatus.Size = new System.Drawing.Size(55, 17);
            this.opcStatus.TabIndex = 3;
            this.opcStatus.TabStop = true;
            this.opcStatus.Text = "Status";
            this.opcStatus.UseVisualStyleBackColor = true;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Location = new System.Drawing.Point(171, 143);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(75, 23);
            this.btnAceptar.TabIndex = 2;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // frmOpcCatalogo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(257, 174);
            this.Controls.Add(this.btnAceptar);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmOpcCatalogo";
            this.Text = "Opciones de Reporte";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.RadioButton opcActivos;
        public System.Windows.Forms.RadioButton opcTodos;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.RadioButton opcStatus;
        public System.Windows.Forms.RadioButton opcDepto;
        public System.Windows.Forms.RadioButton opcNombre;
        public System.Windows.Forms.RadioButton opcClave;
        public System.Windows.Forms.Button btnAceptar;
    }
}