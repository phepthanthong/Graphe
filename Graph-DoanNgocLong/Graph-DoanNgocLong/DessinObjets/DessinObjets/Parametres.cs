using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DessinObjets
{
    public partial class Parametres : Form
    {
        //private Color Couleur;
        //private int Épais;
        #region proprietes
        public Color Couleur
        {
            get { return labelCouleur.BackColor; }
            set { labelCouleur.BackColor = value; }
        }
        public int Épais
        {
            get { return (int)numericUpDownEpaisseur.Value; }
            set { numericUpDownEpaisseur.Value = value; }
        }
        #endregion propriete
        public Parametres()
        {
            InitializeComponent();
        }

        private void labelCouleur_Click(object sender, EventArgs e)
        {
            ColorDialog d = new ColorDialog();
            d.Color = labelCouleur.BackColor;
            if (d.ShowDialog() == DialogResult.OK)
            {
                labelCouleur.BackColor = d.Color;
            }

        }
    }
}
