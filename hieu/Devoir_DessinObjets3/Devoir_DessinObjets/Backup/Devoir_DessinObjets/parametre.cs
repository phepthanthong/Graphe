using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Devoir_DessinObjets
{
    public partial class parametre : Form
    {
        public parametre()
        {
            InitializeComponent();
        }

        public Color p_Couleur
        {
            get { return labelCouleur.BackColor; }
            set { labelCouleur.BackColor = value; }
        }

        public int p_epaisseur
        {
            get { return (int) numericUpDown1.Value; }
            set { numericUpDown1.Value = value; }
        }
    

        private void labelCouleur_Click(object sender, EventArgs e)
        {
            ColorDialog c = new ColorDialog();
            c.Color = labelCouleur.BackColor;
            if (c.ShowDialog() == DialogResult.OK)
                labelCouleur.BackColor = c.Color;
        }

       

    }
}
