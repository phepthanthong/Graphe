using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml;

namespace Devoir_DessinObjets
{
    public class Noeud
    {
        #region attributs
        //attribut
        private Rectangle rect;
        private Color couleur;
        private int épaisseur; //do day
        #endregion attributs
        //methode
        public Color Colour
        {
            get { return couleur; }
            set { couleur = value; }
        }

        public int Epais
        {
            get { return (int)épaisseur; }
            set { épaisseur = (int)value; }
        }
        //constructeur
        public Point Centre
        {
            get { return new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2); }
            set
            {
                rect.X = value.X - rect.Width / 2;
                rect.Y = value.Y - rect.Height / 2;
            }
        }
        public Noeud(Point p, Size s, Color c, int e)
        {
            rect = new Rectangle(new Point(p.X - s.Width/2, p.Y - s.Height/2), s);
            couleur = c;
            épaisseur = e;
            //Centre = p;
        }
        public Noeud(string champ)
        {
            string [] donne = champ.Split(';');
            épaisseur = int.Parse(donne[0]);
            couleur = Color.FromArgb(int.Parse(donne[1]));
            char[] delimiters = new char[] { '=', ',', '}' };
            string[] parts = donne[2].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            rect = new Rectangle(int.Parse(parts[1]),int.Parse(parts[3]),int.Parse(parts[5]),int.Parse(parts[7]) );
        }

        //methode dessin

        public void Dessine(Graphics g)
        {
            g.DrawRectangle(new Pen(couleur, épaisseur), rect);
        }
        public void Move(Point p)
        {
            rect.X = p.X - rect.Width / 2;
            rect.Y = p.Y - rect.Height / 2;
        }
        public bool Contains(Point p)
        {
            return rect.Contains(p);
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
        public string ToXML()
        {
            string text = "<NOEUD>";
            text += " <epaisseur>";
            text += " " + épaisseur.ToString();
            text += " </epaisseur>";
            text += " <couleur>";
            text += " " + couleur.ToArgb().ToString();
            text += " </couleur>";
            text += " <rectangle>";
            text += " " + rect.ToString();
            text += " </rectangle>";
            text += "</NOEUD>";
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
                        int colour = int.Parse(xNNN.InnerText);
                        couleur = Color.FromArgb(colour);
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
