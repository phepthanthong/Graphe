namespace Devoir_DessinObjets
{
    partial class parametre
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
            this.OK = new System.Windows.Forms.Button();
            this.Cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelCouleur = new System.Windows.Forms.Label();
            this.Epaisseur = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // OK
            // 
            this.OK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OK.Location = new System.Drawing.Point(208, 68);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(63, 26);
            this.OK.TabIndex = 0;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // Cancel
            // 
            this.Cancel.Location = new System.Drawing.Point(211, 125);
            this.Cancel.Name = "Cancel";
            this.Cancel.Size = new System.Drawing.Size(60, 24);
            this.Cancel.TabIndex = 1;
            this.Cancel.Text = "Cancel";
            this.Cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 14);
            this.label1.TabIndex = 2;
            this.label1.Text = "Couleur";
            // 
            // labelCouleur
            // 
            this.labelCouleur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCouleur.Location = new System.Drawing.Point(95, 125);
            this.labelCouleur.Name = "labelCouleur";
            this.labelCouleur.Size = new System.Drawing.Size(87, 24);
            this.labelCouleur.TabIndex = 3;
            this.labelCouleur.Click += new System.EventHandler(this.labelCouleur_Click);
            // 
            // Epaisseur
            // 
            this.Epaisseur.AutoSize = true;
            this.Epaisseur.Location = new System.Drawing.Point(31, 79);
            this.Epaisseur.Name = "Epaisseur";
            this.Epaisseur.Size = new System.Drawing.Size(55, 14);
            this.Epaisseur.TabIndex = 4;
            this.Epaisseur.Text = "epaisseur";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(95, 73);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(87, 20);
            this.numericUpDown1.TabIndex = 5;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // parametre
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Epaisseur);
            this.Controls.Add(this.labelCouleur);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Cancel);
            this.Controls.Add(this.OK);
            this.Name = "parametre";
            this.Text = "parametre";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.Button Cancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelCouleur;
        private System.Windows.Forms.Label Epaisseur;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
    }
}