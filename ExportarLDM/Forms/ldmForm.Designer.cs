namespace ExportarLDM.Forms
{
    partial class ldmForm
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
            this.comboBoxBoms = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExportar = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // comboBoxBoms
            // 
            this.comboBoxBoms.FormattingEnabled = true;
            this.comboBoxBoms.Location = new System.Drawing.Point(88, 69);
            this.comboBoxBoms.Name = "comboBoxBoms";
            this.comboBoxBoms.Size = new System.Drawing.Size(121, 21);
            this.comboBoxBoms.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(251, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Seleccione la lista de materiales que desea exportar";
            // 
            // buttonExportar
            // 
            this.buttonExportar.Location = new System.Drawing.Point(111, 111);
            this.buttonExportar.Name = "buttonExportar";
            this.buttonExportar.Size = new System.Drawing.Size(75, 23);
            this.buttonExportar.TabIndex = 2;
            this.buttonExportar.Text = "Exportar";
            this.buttonExportar.UseVisualStyleBackColor = true;
            this.buttonExportar.Click += new System.EventHandler(this.buttonExportar_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(229, 158);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(60, 23);
            this.buttonClose.TabIndex = 3;
            this.buttonClose.Text = "Cerrar";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // ldmForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 193);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonExportar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBoxBoms);
            this.Name = "ldmForm";
            this.Text = "Exportar LDM";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxBoms;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExportar;
        private System.Windows.Forms.Button buttonClose;
    }
}