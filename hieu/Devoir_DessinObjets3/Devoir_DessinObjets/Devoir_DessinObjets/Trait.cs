using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Xml;

namespace Devoir_DessinObjets
{
    public class Trait
    {
        private Noeud source;
        public Noeud Source
        {
            get { return source; }
            set { source = value; }
        }
        private Noeud destination;
        public Noeud Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        private Color couleur;
        private int epaisseur;
        public Trait(Noeud sour, Noeud dest, Color c, int e)
        {
            source = sour;
            destination = dest;
            couleur = c;
            epaisseur = e;
        }

        public Trait(string champ, List<Noeud> noueds)
        {
            string [] donne = champ.Split(';');
            char[] delimiters = new char[] { '=', ',', '}' };
            string[] part_source = donne[2].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            Point p_s = new Point(int.Parse(part_source[1]), int.Parse(part_source[3]));
            string[] part_dest = donne[5].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
            Point p_d = new Point(int.Parse(part_dest[1]), int.Parse(part_dest[3]));
            epaisseur = int.Parse(donne[7]);
            couleur = Color.FromArgb(int.Parse(donne[6]));
            foreach (Noeud re in noueds)
            {
                if (re.Contains(p_s))
                {
                    source = re;
                }
                if (re.Contains(p_d))
                {
                    destination = re;
                }
            }
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
        public string ToXML()
        {
            string text = "<TRAIT>";
            text += " <epaisseur>";
            text += " " + epaisseur.ToString();
            text += " </epaisseur>";
            text += " <couleur>";
            text += " " + couleur.ToArgb().ToString();
            text += " </couleur>";
            text += " <source>";
            text += " " + source.Centre.ToString();
            text += " </source>";
            text += " <destination>";
            text += " " + destination.Centre.ToString();
            text += " </destination>";
            text += "</TRAIT>";
            return text;
        }

        public Trait(XmlNode xNN, List<Noeud> listnoeud)
        {

            foreach (XmlNode xNNN in xNN.ChildNodes)
            {
                switch (xNNN.Name)
                {
                    case "epaisseur":
                        epaisseur = int.Parse(xNNN.InnerText);
                        break;
                    case "couleur":
                        int colour = int.Parse(xNNN.InnerText);
                        couleur = Color.FromArgb(colour);
                        break;
                    case "source":
                        string[] src = xNNN.InnerText.Split(',');
                        int x = int.Parse(src[0].Split('=')[1]);
                        int y = int.Parse(src[1].Replace("}", "").Split('=')[1]);
                        Point scr = new Point(x, y);
                        foreach (Noeud n in listnoeud)
                        {
                            if (n.Contains(scr) == true)
                            { source = n; }
                        }
                        break;
                    case "destination":
                        string[] des = xNNN.InnerText.Split(',');
                        int xx = int.Parse(des[0].Split('=')[1]);
                        int yy = int.Parse(des[1].Replace("}", "").Split('=')[1]);
                        Point p = new Point(xx, yy);
                        foreach (Noeud n in listnoeud)
                        {
                            if (n.Contains(p) == true)
                            { destination = n; }
                        }
                        break;
                }
            }
        }
    }
}
