using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;

namespace DessinObjets
{
    public class Noeud
    {
        #region attributs
            private Rectangle rect;
            private Color couleur;
            private int épaisseur;
        #endregion attributs
        #region propriete
            public int Épaisseur
                {
                    get { return épaisseur; }
                    set { épaisseur = value; }
                }
            public Color Couleur
            {
                get { return couleur; }
                set { couleur = value; }
            }
            public Point Centre
            {
                get { return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2); }
                set
                    {
                        rect.X = value.X - rect.Width / 2;
                        rect.Y = value.Y - rect.Height / 2;
                    }
            }
        #endregion propriete
        public Noeud(Rectangle r, Color c, int e)
        {
            rect = r;
            couleur = c;
            épaisseur = e;
        }
        public Noeud(Point p, Size s, Color c, int e)
        {
            rect = new Rectangle(new Point(p.X - s.Width / 2, p.Y - s.Height /2), s);
            couleur = c;
            épaisseur = e;
        }
        public Noeud(string champ)
        {
            string[] données = champ.Split(';');
            // premiere valeur: epaisseur
            int épaisseur = int.Parse(données[0]);
            // deuxieme valeur: couleur
            couleur = Color.FromArgb(int.Parse(données[1]));
            // troisieme valeur: le rectangle
            char[] delimiters = new char[] { '=', ',', '}' };
            string[] parts = données[2].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            rect = new Rectangle(int.Parse(parts[1]), int.Parse(parts[3]), int.Parse(parts[5]), int.Parse(parts[7]));
        }
        public void Dessine(Graphics g)
        {
            Pen p = new Pen(couleur, épaisseur);
            g.DrawRectangle(p, rect);
        }
        public bool Contains(Point p)
        {   
            /*if (rect.Contains(p))
            //if (p.X >= rect.X && p.X <= rect.X + rect.Width && p.Y >= rect.Y && p.Y <= rect.Height)
                return true;
            else return false; */
            return rect.Contains(p);
        }
        public void Move(Point p)
        {
            rect.Location = new Point(p.X - rect.Width / 2, p.Y - rect.Height /2);
        }
        public override string ToString()
        {
            string s;
            s = épaisseur.ToString();
            s += ";";
            s += couleur.ToArgb().ToString();
            s += ";";
            s += rect.ToString();
            return s;
        }
        public bool isAtLocation(Point p)
        {
            return (rect.Location.X == p.X) && (rect.Location.Y == p.Y);
        }
        public string ToXML()
        {
            string text = "<noeud>";
            text += " <epaisseur>";
            text += " " + épaisseur.ToString();
            text += " </epaisseur>";
            text += " <couleur>";
            text += " " + couleur.ToArgb().ToString();
            text += " </couleur>";
            text += " <rectangle>";
            text += " " + rect.ToString();
            text += " </rectangle>";
            text += "</noeud>";
            return text;
        }
        public Noeud(XmlNode xNN)
        {

            foreach (XmlNode xNNN in xNN.ChildNodes)
            {
                switch (xNNN.Name)
                {
                    case "epaisseur":
                        épaisseur = int.Parse(xNNN.InnerText);
                        break;
                    case "couleur":
                        int c = int.Parse(xNNN.InnerText);
                        couleur = Color.FromArgb(c);
                        break;
                    case "rectangle":
                        string[] data = xNNN.InnerText.Split(',');
                        int x = int.Parse(data[0].Split('=')[1]);
                        int y = int.Parse(data[1].Split('=')[1]);
                        int w = int.Parse(data[2].Split('=')[1]);
                        int h = int.Parse(data[3].Replace("}", "").Split('=')[1]);
                        rect = new Rectangle(x, y, w, h);
                        break;
                }
            }
           
        }

    }
}
