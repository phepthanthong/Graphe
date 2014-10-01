using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DessinObjets
{
    public partial class DessinObjets : Form
    {
        #region DessinObjet

        #region attributs
        List<Noeud> noeuds = new List<Noeud>();
        List<Trait> traits = new List<Trait>();
        List<Int16> sommets = new List<Int16>();

        Noeud noeudCourant = null;
        Point pointCourant = Point.Empty;
        bool enDéplacement;
        bool dessinTrait;

        Color couleurParDéfaut = Color.Black;
        int epaisseurParDéfaut = 1;
        Size tailleParDéfaut = new Size(10, 15);
        #endregion attributs        

        public DessinObjets()
        {
            InitializeComponent();
            //moinsToolStripMenuItem.Enabled = false;
        }

        private void DessinObjets_MouseDown(object sender, MouseEventArgs e)
        {
            noeudCourant = NoeudCourant(e);
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    {
                        if (déplacement.Checked)
                        {
                            if (noeudCourant != null)
                            {
                                enDéplacement = true;
                            }
                        }
                        else
                        {
                            if (noeudCourant == null)
                            {
                                Noeud noeud = new Noeud(e.Location, new Size(10, 15), couleurParDéfaut, epaisseurParDéfaut);
                                noeuds.Add(noeud);
                            }
                            else dessinTrait = true;
                        }
                        break;
                    }
                case System.Windows.Forms.MouseButtons.Right:
                    {
                        if (noeudCourant != null)
                        {
                            string[] libellés = new string[] { "Supprimer", "Modifier" };
                            ContextMenuStrip cm = new ContextMenuStrip();
                            foreach (string libel in libellés)
                            {
                                ToolStripMenuItem menuItem = new ToolStripMenuItem(libel);
                                menuItem.Click += menuItem_Click;
                                cm.Items.Add(menuItem);
                            }
                            cm.Show(this, e.Location);
                            /*
                            Parametres d = new Parametres();
                            d.Couleur = noeudCourant.Couleur;
                            d.Épais = noeudCourant.Épaisseur;
                            if (d.ShowDialog() == DialogResult.OK)
                            {
                                noeudCourant.Couleur = d.Couleur;
                                noeudCourant.Épaisseur = d.Épais;
                            }
                             */
                        }
                        break;
                    }
            }
            Refresh();
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            switch (tm.Text)
            {
                case "Modifier":
                    {
                        Parametres d = new Parametres();
                        d.Couleur = noeudCourant.Couleur;
                        d.Épais = noeudCourant.Épaisseur;
                        if (d.ShowDialog() == DialogResult.OK)
                        {
                            noeudCourant.Couleur = d.Couleur;
                            noeudCourant.Épaisseur = d.Épais;
                        }
                        break;
                    }    
                    
                case "Supprimer":
                    {
                        // dans un foreach, il est impossible de Remove un element ds une liste
                        // il faut creer une liste intermediare aSupprimer
                        List<Trait> aSupprimer = new List<Trait>(); 
                        foreach (Trait tr in traits)
                            if (tr.Destination == noeudCourant || tr.Source == noeudCourant)
                                aSupprimer.Add(tr);
                        foreach (Trait tr in aSupprimer)
                            traits.Remove(tr);
                        noeuds.Remove(noeudCourant);
                    }
                    break;
            }
        }
 
        private Noeud NoeudCourant(MouseEventArgs e)
        {
            foreach (Noeud re in noeuds)
            {
                if (re.Contains(e.Location))
                {
                    return re;
                }
            }
            return null;
        }

        private void DessinObjets_Paint(object sender, PaintEventArgs e)
        {
            foreach (Noeud n in noeuds)
                n.Dessine(e.Graphics);
            foreach (Trait t in traits)
                t.Dessine(e.Graphics);
            if (pointCourant != Point.Empty)
            {
                Noeud fin = new Noeud(pointCourant, tailleParDéfaut, couleurParDéfaut, epaisseurParDéfaut);
                fin.Dessine(e.Graphics);
                e.Graphics.DrawLine(Pens.Red, noeudCourant.Centre, pointCourant);
            }
        }

        private void DessinObjets_MouseUp(object sender, MouseEventArgs e)
        {
                enDéplacement = false;
                if (dessinTrait)
                {
                    Noeud fin = NoeudCourant(e);
                    if (fin == null)
                    {
                        fin = new Noeud(e.Location, new Size(10, 15), Color.Black, 2);
                        noeuds.Add(fin);
                    }
                    Trait t = new Trait(noeudCourant, fin, couleurParDéfaut, 1);
                    traits.Add(t);
                    Refresh();
                    dessinTrait = false;
                }
                //noeudCourant = null;
                pointCourant = Point.Empty;
        }
        
        private void DessinObjets_MouseMove(object sender, MouseEventArgs e)
        {
            // si noeud en deplacement, deplacer le noeud
            if (enDéplacement)
            {
                if (noeudCourant != null)
                {
                    noeudCourant.Move(e.Location);
                }
            }
            if (dessinTrait)
                pointCourant = e.Location;
            Refresh();
        }

        private Noeud TrouveNoeud(Point p)
        {
            foreach (Noeud re in noeuds)
            {
                if (re.Contains(p))
                {
                    return re;
                }
            }
            return null;
        }
                
        private Noeud NoeudParDéfaut(Point point)
        {
            return new Noeud(point, tailleParDéfaut, couleurParDéfaut, epaisseurParDéfaut);
        }
 
        private void toolStripButtonCouleur_Click(object sender, EventArgs e)
        {
            ColorDialog d = new ColorDialog();
            d.Color = couleurParDéfaut;
            if (d.ShowDialog() == DialogResult.OK) 
                couleurParDéfaut = d.Color;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            noeuds.Clear();
            traits.Clear();
            this.Refresh();
        }
 
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*"  ; //"csv files (*.csv)|*.csv|All files (*.*)|*.*"
            saveDialog.FilterIndex = 1 ;
            saveDialog.RestoreDirectory = true ;
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                string fichier = saveDialog.FileName;
                StreamWriter sw = new StreamWriter(fichier);
                /*
                sw.WriteLine(noeuds.Count.ToString()); // calculer le nombre de noeud
                foreach (Noeud n in noeuds)
                {
                    //Sauvegarde d'un noeud
                    sw.WriteLine(n.ToString()); // ajouter dans la classe Noeud une fonction ToString() même si ca a l'air marche
                }
                sw.WriteLine(traits.Count.ToString()); // calculer le nombre de trait
                foreach (Trait t in traits)
                {
                    sw.WriteLine(t.ToString());
                }
                */
                sw.WriteLine("<!--?xml version=\"1.0\" encoding=\"UTF-8\" ?--> ");
                sw.WriteLine("<dessin>");
                foreach (Noeud r in noeuds)
                {
                    sw.WriteLine(r.ToXML());
                }
                foreach (Trait tr in traits)
                {
                    sw.WriteLine(tr.ToXML());
                }
                sw.WriteLine("</dessin>");
                sw.Close();
            }
        }

        private void WriteLine()
        {
            throw new NotImplementedException();
        }     

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Fichier xml|*.xml";
            openDialog.FilterIndex = 1 ;
            openDialog.Title = "Choisir le fichier";
            openDialog.RestoreDirectory = true ;
            if(openDialog.ShowDialog() == DialogResult.OK)
            {
                /*
                noeuds.Clear();
                traits.Clear();
                StreamReader sw = new StreamReader(openDialog.FileName);
                int nbNoeuds = int.Parse(sw.ReadLine());
                // lecture de Noeud
                for (int i = 0; i < nbNoeuds && !sw.EndOfStream; i++)
                {
                     // ligne suivante
                    string champ = sw.ReadLine();
                    // traitement d'une ligne du fichier
                    Noeud n = new Noeud(champ);
                    noeuds.Add(n);
                }
                int nbTraits = int.Parse(sw.ReadLine());
                // lecture de Trait
                for (int i = 0; i < nbTraits && !sw.EndOfStream; i++)
                {
                    // ligne suivante
                    string champ = sw.ReadLine();
                    // traitement d'une ligne du fichier
                    Trait t = new Trait(champ, noeuds);
                    traits.Add(t);
                }
                */

                XmlDocument hieu = new XmlDocument();
                hieu.Load(openDialog.FileName);
                foreach (XmlNode xN in hieu.ChildNodes)
                {
                    if (xN.Name == "dessin")
                    {
                        foreach (XmlNode xNN in xN.ChildNodes)
                        {
                            if (xNN.Name == "noeud")
                            {
                                Noeud n = new Noeud(xNN);
                                noeuds.Add(n);
                            }
                            if (xNN.Name == "trait")
                            {
                                Trait t = new Trait(xNN, noeuds);
                                traits.Add(t);
                            }
                        }
                    }

                }                
            }
            Refresh();
        }

        #endregion

        private bool bipartie;
        List<String>[] newsommet;
        
        List<List<String>> region1 = new List<List<String>>();
        List<List<String>> region2 = new List<List<String>>();        

        #region Bipartie
        private void toolStripButton1_Click(object sender, EventArgs e)
        {            
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Fichier txt|*.txt";
            openDialog.FilterIndex = 1 ;
            openDialog.Title = "Choisir le fichier";
            openDialog.RestoreDirectory = true ;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                noeuds.Clear();
                traits.Clear();
                StreamReader sw = new StreamReader(openDialog.FileName);
                int nb_sommet;
                string line = sw.ReadLine();
                nb_sommet = int.Parse(line);

                newsommet = new List<String>[nb_sommet];
                for(int i=0;i<nb_sommet;i++)
                    newsommet[i] = new List<String>();

                #region Partie 1
                //-------------Matrice d'adjacent + Matrice D'incident-------------//
                 
                int count = 0;
                for (int i = 0; i < nb_sommet; i++)
                {
                    line = sw.ReadLine();
                    for (int j = 0; j < nb_sommet; j++)
                    {                       
                        if (line[j].ToString() != "0")
                        {
                            newsommet[i].Add(count.ToString());
                        }
                        count++;
                    }
                    count = 0;
                }
                //-----------------------------------------------------------------/*/

                /*------------------------Liste d'adjacent--------------------------//

                for (int i = 0; i < nb_sommet; i++)
                {
                    line = sw.ReadLine();
                    for (int j = 0; j < nb_sommet; j++)
                    {
                        if (j != 0)
                        {
                            newsommet[i].Add(j.ToString());
                        }
                    }
                }

                //-----------------------------------------------------------------/*/
                #endregion

                #region Partie 2
                //----------------------Matrice D'incident-------------------------/*/

                region1.Add(newsommet[0]);

                /*for (int i = 0; i < nb_sommet; i++)
                {
                    for (int j = 0; j < nb_sommet; j++)
                    {
                        if (j != i)
                        {
                             if(region1.Contains(newsommet[i]))
                             {
                                foreach(String k1 in newsommet[j])                                                           
                                {
                                    if (newsommet[i].Contains(k1) && !region2.Contains(newsommet[j]))
                                        region2.Add(newsommet[j]);                                    
                                }
                             }                                  
                             else if (region2.Contains(newsommet[i]))
                             {
                                foreach(String k1 in newsommet[j])  
                          
                                {
                                    if (newsommet[i].Contains(k1) && !region1.Contains(newsommet[j]))
                                        region1.Add(newsommet[j]);
                                }
                            }
                        }
                    }

                } */

                //---------------------------------------------------------------------/*/

                //---------------Matrice D'adjacent + Liste D'adjacent-----------------//
                 
                for (int i = 0; i < nb_sommet-1; i++)
                {
                    foreach (List<String> k1 in region1)
                    {
                        foreach (String k2 in k1)
                        {
                            if (!region2.Contains(newsommet[int.Parse(k2)]))
                            {
                                region2.Add(newsommet[int.Parse(k2)]);
                            }
                        }
                    }

                    foreach (List<String> k1 in region2)
                    {
                        foreach (String k2 in k1)
                        {
                            if (!region1.Contains(newsommet[int.Parse(k2)]))
                            {
                                region1.Add(newsommet[int.Parse(k2)]);
                            }
                        }
                    }
                }
                //---------------------------------------------------------------------/
                #endregion

                bipartie = estBipartie(region1, region2);
                ShowBipartie();
                sw.Close();
            }
            
        }
        
        public bool estBipartie(List<List<String>> reg1, List<List<String>> reg2)
        {
            for (int i = 0; i < reg1.Count(); i++)
            {
                foreach (List<String> k1 in reg1)
                {
                    foreach (String k2 in k1)
                    {
                        if (reg1.Contains(newsommet[int.Parse(k2)]))
                            return false;

                    }
                }
            }
            for (int i = 0; i < reg2.Count(); i++)
            {
                foreach (List<String> k1 in reg2)
                {
                    foreach (String k2 in k1)
                    {
                        if (reg2.Contains(newsommet[int.Parse(k2)]))
                            return false;
                    }
                }
            }
            return true;
        }

        public void ShowBipartie()
        {
            if (bipartie == true)
                MessageBox.Show("Duoc");
            else
                MessageBox.Show("Khong Duoc");
        }

        #endregion

    }
}