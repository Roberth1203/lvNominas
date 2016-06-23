namespace La_Vista_Nominas
{
    partial class nominaHoras
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
            this.rpViewerHoras = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rpViewerHoras
            // 
            this.rpViewerHoras.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rpViewerHoras.LocalReport.ReportEmbeddedResource = "La_Vista_Nominas.rptNominaHoras.rdlc";
            this.rpViewerHoras.Location = new System.Drawing.Point(1, 2);
            this.rpViewerHoras.Name = "rpViewerHoras";
            this.rpViewerHoras.Size = new System.Drawing.Size(705, 452);
            this.rpViewerHoras.TabIndex = 0;
            // 
            // nominaHoras
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(707, 457);
            this.Controls.Add(this.rpViewerHoras);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "nominaHoras";
            this.Text = "Recibo de Nómina Individual";
            this.Load += new System.EventHandler(this.nominaHoras_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpViewerHoras;
    }
}