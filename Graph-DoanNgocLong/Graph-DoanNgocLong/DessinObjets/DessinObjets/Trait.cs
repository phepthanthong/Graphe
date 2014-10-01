using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;

namespace DessinObjets
{
    public class Trait
    {
        #region attributs
        private Noeud source;
        private Noeud destination;
        private Color couleur;
        private int epaisseur;
        #endregion attributs
        #region proprietes
        public Noeud Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        public Noeud Source
        {
            get { return source; }
            set { source = value; }
        }
        #endregion proprietes
        public Trait(Noeud sour, Noeud dest, Color c, int e)
        {
            source = sour;
            destination = dest;
            couleur = c;
            epaisseur = e;
        }

        public Trait(string champ, List<Noeud> noeuds)
        {
            string[] données = champ.Split(';');

            // premiere valeur: epaisseur
            epaisseur = int.Parse(données[0]);

            // deuxieme valeur: couleur
            couleur = Color.FromArgb(int.Parse(données[1]));

            // troisieme valeur: la source
            char[] delimiters = new char[] { '=', ',', '}' };
            string[] parts = données[2].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            //rect = new Rectangle(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[7]));
            Point pSource = new Point(int.Parse(parts[1]),int.Parse(parts[3]));
            foreach (Noeud n in noeuds)
                if (n.isAtLocation(pSource))
                    source = n;

            // quatrieme valeur: la destination
            //char[] delimiters = new char[] { '=', ',', '}' };
            parts = données[5].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            //rect = new Rectangle(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[7]));
            Point pDestination = new Point(int.Parse(parts[1]), int.Parse(parts[3]));
            foreach (Noeud n in noeuds)
                if (n.isAtLocation(pDestination))
                    source = n;
            
        }
        public void Dessine(Graphics g)
        {
            Pen p = new Pen(couleur, epaisseur);
            g.DrawLine(p, source.Centre, destination.Centre);
        }

        public override string ToString()
        {
            string s;
            s = source.ToString();
            s += ";";
            s += destination.ToString();
            s += ";";
            s += couleur.ToArgb().ToString();
            s += ";";
            s += epaisseur.ToString();
            return s;
        }

        public Rectangle rect { get; set; }

        public string ToXML()
        {
            string text = "<trait>";
            text += " <epaisseur>";
            text += " " + epaisseur.ToString();
            text += " </epaisseur>";
            text += " <couleur>";
            text += " " + couleur.ToArgb().ToString();
            text += " </couleur>";
            text += " <noeudsource>";               // Chaque trait est associe a un noeud source et un noeud destination
            text += " " + source.Centre.ToString(); // Pour chaque noeud, je garde seulement les coordonnees du point formant ce noeud (X et Y) 
            text += " </noeudsource>";
            text += " <noeuddestination>";
            text += " " + destination.Centre.ToString();
            text += " </noeuddestination>";
            text += "</trait>";
            return text;
        }

        public Trait(XmlNode xNN, List<Noeud> ln)
        {

            foreach (XmlNode xNNN in xNN.ChildNodes)
            {
                switch (xNNN.Name)
                {
                    case "epaisseur":
                        epaisseur = int.Parse(xNNN.InnerText);
                        break;
                    case "couleur":
                        int c = int.Parse(xNNN.InnerText);
                        couleur = Color.FromArgb(c);
                        break;
                    case "noeudsource":                        
                        string[] data = xNNN.InnerText.Split(',');
                        int x = int.Parse(data[0].Split('=')[1]);
                        int y = int.Parse(data[1].Replace("}", "").Split('=')[1]); 
                        Point scr = new Point(x, y); // on recree le point source formant le noeud source a partir des coordonnees X et Y deja coupees 
                        foreach (Noeud n in ln)
                        {
                            if (n.Contains(scr) == true) // On cherche le noeud correspondant dans la liste Noeud et affecte ce noeud au noeud source
                            { source = n; }
                        }
                        break;
                    case "noeuddestination": // meme algorithme pour le noeud destination
                        string[] data1 = xNNN.InnerText.Split(',');
                        int xx = int.Parse(data1[0].Split('=')[1]);
                        int yy = int.Parse(data1[1].Replace("}", "").Split('=')[1]);
                        Point des = new Point(xx, yy);
                        foreach (Noeud n in ln)
                        {
                            if (n.Contains(des) == true)
                            { destination = n; }
                        }
                        break;
                }
            }
        }
    }
}
