using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace Devoir_DessinObjets
{
    public partial class DessinObjets : Form
    {
        List<Noeud> noeuds = new List<Noeud>();
        List<Trait> traits = new List<Trait>();
        Noeud noeudCourant = null;
        Point pointCourant = Point.Empty;
        bool enDéplacement;
        bool dessinTrait;

        string[] array = new string[50]; // chi co toi da 50 diem hay 50 canh
        //int[][] matrice;
        //int elements;
        string  type_matrice;

        public DessinObjets()
        {
            InitializeComponent();
            for (int i = 0; i < 50; i++)
            {
                array[i] = "";
            }
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
                                Noeud nouv = new Noeud(e.Location, new Size(10, 15), Color.Black, 2);
                                noeuds.Add(nouv);
                            }
                            else
                            { dessinTrait = true; }
                        }
                        Refresh();
                        break;
                    }
                case System.Windows.Forms.MouseButtons.Right:
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
                        parametre p = new parametre();
                        p.p_Couleur = noeudCourant.Colour;
                        p.p_epaisseur = noeudCourant.Epais;

                        if (p.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            noeudCourant.Colour = p.p_Couleur;
                            noeudCourant.Epais = p.p_epaisseur;
                        }
                        */
                        break;
                    }
            }

            
        }

        private void menuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tm = (ToolStripMenuItem)sender;
            switch (tm.Text)
            {
                case "Modifier":
                    parametre d = new parametre();
                    d.p_Couleur = noeudCourant.Colour;
                    d.p_epaisseur = noeudCourant.Epais;
                    if (d.ShowDialog() == DialogResult.OK)
                    {
                        noeudCourant.Colour = d.p_Couleur;
                        noeudCourant.Epais = d.p_epaisseur;
                    }
                    break;
                case "Supprimer":
                    {
                        List<Trait> delete = new List<Trait>();
                        foreach (Trait t in traits)
                            if(t.Destination == noeudCourant||
                                t.Source == noeudCourant)
                                delete.Add(t);
                        foreach(Trait t in delete)
                            traits.Remove(t);
                            noeuds.Remove(noeudCourant);
                        break;
                    }
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
                Noeud fin = new Noeud(pointCourant, new Size(10, 15), Color.Black, 2);
                fin.Dessine(e.Graphics);
                e.Graphics.DrawLine(Pens.Red, noeudCourant.Centre, pointCourant);
            }
        }

        private void DessinObjets_MouseMove(object sender, MouseEventArgs e)
        {
            if (enDéplacement)
            {
                if (noeudCourant != null)
                {
                    noeudCourant.Move(e.Location);
                }
            }
            if (dessinTrait)
            {
                pointCourant = e.Location;
            }
            Refresh();
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
                Trait t = new Trait(noeudCourant, fin, Color.Black, 1);
                traits.Add(t);
                Refresh();
                dessinTrait = false;
            }
            //noeudCourant = null;//dep vi nguy hiem
            pointCourant = Point.Empty;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "XML file (*.xml)|*.xml";
            saveDialog.FilterIndex = 1;
            saveDialog.RestoreDirectory = true;

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                string fichier = saveDialog.FileName; 
                StreamWriter sw = new StreamWriter(fichier);
                /*sw.WriteLine(noeuds.Count.ToString());
                foreach (Noeud n in noeuds)
                {
                    sw.WriteLine(n.ToString());
                }
                sw.WriteLine(traits.Count.ToString());
                foreach (Trait t in traits)
                {
                    sw.WriteLine(t.ToString());
                }*/

                sw.WriteLine("<!--?xml version=\"1.0\" encoding=\"UTF-8\" ?--> ");
                sw.WriteLine("<DESSIN>");
                foreach (Noeud n in noeuds)
                {
                    sw.WriteLine(n.ToXML());
                }
                foreach (Trait t in traits)
                {
                    sw.WriteLine(t.ToXML());
                }
                sw.WriteLine("</DESSIN>");
                
                sw.Close();
            }

        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Fichier txt|*.txt";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
                StreamReader sw = new StreamReader(file);
                type_matrice = sw.ReadLine();
                int m = 0;
                while (!sw.EndOfStream)
                {
                    array[m] = sw.ReadLine();
                    m++;
                }
                sw.Close();
            }
            if (type_matrice == "0")
                binaire_adjacency();
            else if (type_matrice == "1")
                binaire_incidence();
            else if (type_matrice == "2")
                binaire_Adjacency_Lists();
            Refresh();
        }
        private void binaire_Adjacency_Lists()
        {
           // int socanh = array[0].Length;
            //int sodinh = 0;
            //for (int h = 0; h < 50; h++)
            //{
            //    if (array[h] == "")
            //        break;
            //    sodinh++;
            //}
            //string[] dinh = new string[sodinh];
            //for (int h = 0; h < sodinh; h++)
            //    dinh[h] = "";
            //bool thoat = false;
            //bool ok = true;
            //dinh[0] = "a";
            //int i = 0;
            //while (thoat == false && ok == true)
            //{
            //    if (dinh[i] == "a")
            //    {
            //        for (int j = 0; j < socanh; j++)
            //            if (array[i][j] == '1')
            //            {
            //                for (int k = 0; k < sodinh; k++)
            //                    if (k != i)
            //                    {
            //                        if (array[k][j] == '1')
            //                        {
            //                            if (dinh[k] == "a")
            //                                ok = false;
            //                            else if (dinh[k] != "b")
            //                                dinh[k] = "b";
            //                        }
            //                    }
            //            }
            //    }
            //    else if (dinh[i] == "b")
            //    {
            //        for (int j = 0; j < socanh; j++)
            //            if (array[i][j] == '1')
            //            {
            //                for (int k = 0; k < sodinh; k++)
            //                    if (k != i)
            //                    {
            //                        if (array[k][j] == '1')
            //                        {
            //                            if (dinh[k] == "b")
            //                                ok = false;
            //                            else if (dinh[k] != "a")
            //                                dinh[k] = "a";
            //                        }
            //                    }
            //            }
            //    }
            //    int count = 0;
            //    for (int j = 0; j < sodinh; j++)
            //        if (dinh[j] == "a" || dinh[j] == "b")
            //            count++;
            //    if (count == sodinh && i == sodinh - 1)
            //        thoat = true;
            //    i++;
            //    if (i == sodinh)
            //        i = 0;
            //}
            //string show = "";
            //if (ok == false)
            //    MessageBox.Show("khong duoc");
            //else
            //{
            //    for (int j = 0; j < sodinh; j++)
            //        if (dinh[j] == "a")
            //            show += j.ToString() + " ";
            //    MessageBox.Show(show);
            //}
        }
        private void binaire_incidence()
        {
            int socanh = array[0].Length;
            int sodinh = 0;
            for (int h = 0; h < 50; h++)
            {
                if (array[h] != "")
                    sodinh++;
            }
            string[] dinh = new string[sodinh];
            for (int h = 0; h < sodinh; h++)
                dinh[h] = "";
            bool thoat = false;
            bool ok = true;
            dinh[0] = "a";
            int i = 0;
            while (thoat == false && ok == true)
            {
                if (dinh[i] == "a")
                {
                    for (int j = 0; j < socanh; j++)
                        if (array[i][j] == '1')
                        {
                            for (int k = 0; k < sodinh; k++)
                                if (k != i)
                                {
                                    if (array[k][j] == '1')
                                    {
                                        if (dinh[k] == "a")
                                            ok = false;
                                        else if (dinh[k] != "b")
                                            dinh[k] = "b";
                                    }
                                }
                        }
                }
                else if (dinh[i] == "b")
                {
                    for (int j = 0; j < socanh; j++)
                        if (array[i][j] == '1')
                        {
                            for ( int k=0; k< sodinh; k++)
                                if (k != i)
                                {
                                    if (array[k][j] == '1')
                                    {
                                        if (dinh[k] == "b")
                                            ok = false;
                                        else if (dinh[k] != "a")
                                            dinh[k] = "a";
                                    }
                                }
                        }
                }
                int count = 0;
                for (int j = 0; j < sodinh; j++)
                    if (dinh[j] == "a" || dinh[j] == "b")
                        count++;
                if (count == sodinh && i == sodinh - 1)
                    thoat = true;
                i++;
                if (i == sodinh)
                    i = 0;
            }
            string show = "";
            if (ok == false)
                MessageBox.Show("khong duoc");
            else
            {
                for (int j = 0; j < sodinh; j++)
                    if (dinh[j] == "a")
                        show += j.ToString() + " ";
                MessageBox.Show(show);
            }
        }
        private void binaire_adjacency() // lam them cai test kiem tra ma tran co dung, hop le khong
        {
            int sodinh = array[0].Length;
            string[] dinh = new string[sodinh];
            for (int h = 0; h < sodinh; h++)
                dinh[h] = "";
            bool thoat = false;
            bool ok = true;
            dinh[0] = "a";
            int i = 0;
            while (thoat == false && ok == true)
            {
                if (dinh[i] == "a")
                {
                    for (int j = 0; j < sodinh; j++)
                        if (array[i][j] == '1')
                        {
                            if (dinh[j] == "a")
                                ok = false;
                            else if (dinh[j] != "b")
                                dinh[j] = "b";
                        }
                }
                else if (dinh[i] == "b")
                {
                    for (int j = 0; j < sodinh; j++)
                        if (array[i][j] == '1')
                        {
                            if (dinh[j] == "b")
                                ok = false;
                            else if (dinh[j] != "a")
                                dinh[j] = "a";
                        }
                }
                int count = 0;
                for (int j = 0; j < sodinh; j++)
                    if (dinh[j] == "a" || dinh[j] == "b")
                        count++;
                if (count == sodinh && i == sodinh - 1)
                    thoat = true;
                i++;
                if (i == sodinh)
                    i = 0;
            }
            string show = "";
            if (ok == false)
                MessageBox.Show("khong duoc");
            else
            {
                for (int j = 0; j < sodinh; j++)
                    if (dinh[j] == "a")
                        show += j.ToString() + " ";
                MessageBox.Show(show);
            }
        }
        //private void openToolStripButton_Click(object sender, EventArgs e)
        //{
        //    OpenFileDialog openDialog = new OpenFileDialog();
        //    openDialog.Filter = "Fichier xml|*.xml";
        //   // openDialog.FilterIndex = 1;
        //   // openDialog.RestoreDirectory = true;
        //    if (openDialog.ShowDialog() == DialogResult.OK)
        //    {
                
                
        //        /*noeuds.Clear();
        //        traits.Clear();
        //        StreamReader sw = new StreamReader(openDialog.FileName);
        //        int nbNoeud = int.Parse(sw.ReadLine());
        //        //string champ = sw.ReadLine();
        //        while (nbNoeud!=0)
        //        {
        //            string champ = sw.ReadLine();
        //            Noeud nouv = new Noeud(champ);
        //            noeuds.Add(nouv);
        //            //string[] donnes = champ.Split(';');
        //            //champ = sw.ReadLine();
        //            nbNoeud--;
        //        }
        //        int nbTrait = int.Parse(sw.ReadLine());
        //        while (!sw.EndOfStream)
        //        {
        //            string champ = sw.ReadLine();
        //            Trait nouv = new Trait(champ, noeuds);
        //            traits.Add(nouv);
        //            //string[] donnes = champ.Split(';');
        //            //champ = sw.ReadLine();
        //            nbTrait--;
        //        }
        //        sw.Close();*/
        //        XmlDocument doc = new XmlDocument();
        //        doc.Load(openDialog.FileName);
        //        foreach (XmlNode xN in doc.ChildNodes)
        //        {
        //            if (xN.Name == "DESSIN")
        //            {
        //                foreach (XmlNode xNN in xN.ChildNodes)
        //                {
        //                    if (xNN.Name == "NOEUD")
        //                    {
        //                        Noeud n = new Noeud(xNN);
        //                        noeuds.Add(n);
        //                    }
        //                    if (xNN.Name == "TRAIT")
        //                    {
        //                        Trait t = new Trait(xNN, noeuds);
        //                        traits.Add(t);
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    Refresh();
        //}


    }
}