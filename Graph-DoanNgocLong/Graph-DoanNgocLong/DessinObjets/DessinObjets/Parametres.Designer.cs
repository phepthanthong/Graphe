namespace DessinObjets
{
    partial class Parametres
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.numericUpDownEpaisseur = new System.Windows.Forms.NumericUpDown();
            this.labelEpaisseur = new System.Windows.Forms.Label();
            this.labelCouleur = new System.Windows.Forms.Label();
            this.labelTextCouleur = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpaisseur)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(468, 73);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 0;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(468, 142);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 1;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // numericUpDownEpaisseur
            // 
            this.numericUpDownEpaisseur.Location = new System.Drawing.Point(280, 73);
            this.numericUpDownEpaisseur.Name = "numericUpDownEpaisseur";
            this.numericUpDownEpaisseur.Size = new System.Drawing.Size(100, 20);
            this.numericUpDownEpaisseur.TabIndex = 2;
            this.numericUpDownEpaisseur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDownEpaisseur.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // labelEpaisseur
            // 
            this.labelEpaisseur.AutoSize = true;
            this.labelEpaisseur.Location = new System.Drawing.Point(151, 73);
            this.labelEpaisseur.Name = "labelEpaisseur";
            this.labelEpaisseur.Size = new System.Drawing.Size(53, 13);
            this.labelEpaisseur.TabIndex = 3;
            this.labelEpaisseur.Text = "Epaisseur";
            // 
            // labelCouleur
            // 
            this.labelCouleur.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelCouleur.Location = new System.Drawing.Point(280, 142);
            this.labelCouleur.Name = "labelCouleur";
            this.labelCouleur.Size = new System.Drawing.Size(100, 23);
            this.labelCouleur.TabIndex = 4;
            this.labelCouleur.Click += new System.EventHandler(this.labelCouleur_Click);
            // 
            // labelTextCouleur
            // 
            this.labelTextCouleur.AutoSize = true;
            this.labelTextCouleur.Location = new System.Drawing.Point(154, 142);
            this.labelTextCouleur.Name = "labelTextCouleur";
            this.labelTextCouleur.Size = new System.Drawing.Size(43, 13);
            this.labelTextCouleur.TabIndex = 5;
            this.labelTextCouleur.Text = "Couleur";
            // 
            // Parametres
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 264);
            this.Controls.Add(this.labelTextCouleur);
            this.Controls.Add(this.labelCouleur);
            this.Controls.Add(this.labelEpaisseur);
            this.Controls.Add(this.numericUpDownEpaisseur);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Name = "Parametres";
            this.Text = "Parametres";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownEpaisseur)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.NumericUpDown numericUpDownEpaisseur;
        private System.Windows.Forms.Label labelEpaisseur;
        private System.Windows.Forms.Label labelCouleur;
        private System.Windows.Forms.Label labelTextCouleur;
    }
}