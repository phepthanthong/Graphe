using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Bipartie
{
    public partial class Form1 : Form
    {
        string[] array;
        string type;
        public Form1()
        {
            InitializeComponent();
            array = new string[100];
            for (int i = 0; i < 100; i++)
            {
                array[i] = "";
            }
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Fichier txt|*.txt";
            openDialog.FilterIndex = 1;
            openDialog.RestoreDirectory = true;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader read = new StreamReader(openDialog.FileName);
                type = read.ReadLine();
                int i = 0;
                while (!read.EndOfStream)
                {
                    array[i] = read.ReadLine();                    
                    i++;
                }
                read.Close();
            }
            if (type == "0")
            {
                estBipartieMA();
                resetArray();
            }
            else if (type == "1")
            {
                estBipartieMI();
                resetArray();
            }
            else if (type == "2")
            {
                estBipartieLA();
                resetArray();
            }

        }

        private void estBipartieMA()
        { 
            int nbrSommet = array[0].Length;
            string[] sommet = new string[nbrSommet];
            for (int h = 0; h < nbrSommet; h++)
                sommet[h] = "";            
            bool ok = true;
            sommet[0] = "a";
            int i = 0;
            recursive(i, ref sommet, ref ok);
            
            if (ok == false)
                MessageBox.Show("Le graphe entre n'est pas bipartie","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("Le graphe entre est bipartie", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void recursive(int i, ref string[] temp, ref bool ok)
        {
            if (temp[i] == "a")
            {
                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == '1')
                    {
                        if (temp[j] == "a")
                            ok = false;
                        else if (temp[j] == "")
                        {
                            temp[j] = "b";
                            recursive(j, ref temp, ref ok);
                        }
                    }
                }
            }
            else if (temp[i] == "b")
                for (int j = 0; j < array[i].Length; j++)
                    if (array[i][j] == '1')
                    {
                        if (temp[j] == "b")
                            ok = false;
                        else if (temp[j] == "")
                        {
                            temp[j] = "a";
                            recursive(j, ref temp, ref ok);
                        }
                    }
        }

        private void resetArray()
        {
            for (int i = 0; i < 100; i++)
            {
                array[i] = "";
            }
        }

        private void estBipartieMI()
        {
            int nbrArret = array[0].Length;
            int nbrSommet = 0;
            for (int h = 0; h < 50; h++)
            {
                if (array[h] != "")
                    nbrSommet++;
            }
            string[] sommet = new string[nbrSommet];
            for (int h = 0; h < nbrSommet; h++)
                sommet[h] = "";
            bool quitter = false;
            bool continuer = true;
            sommet[0] = "a";
            int i = 0;
            while (quitter == false && continuer == true)
            {
                if (sommet[i] == "a")
                {
                    for (int j = 0; j < nbrArret; j++)
                        if (array[i][j] == '1')
                        {
                            for (int k = 0; k < nbrSommet; k++)
                                if (k != i)
                                {
                                    if (array[k][j] == '1')
                                    {
                                        if (sommet[k] == "a")
                                            continuer = false;
                                        else if (sommet[k] != "b")
                                            sommet[k] = "b";
                                    }
                                }
                        }
                }
                else if (sommet[i] == "b")
                {
                    for (int j = 0; j < nbrArret; j++)
                        if (array[i][j] == '1')
                        {
                            for (int k = 0; k < nbrSommet; k++)
                                if (k != i)
                                {
                                    if (array[k][j] == '1')
                                    {
                                        if (sommet[k] == "b")
                                            continuer = false;
                                        else if (sommet[k] != "a")
                                            sommet[k] = "a";
                                    }
                                }
                        }
                }
                int count = 0;
                for (int j = 0; j < nbrSommet; j++)
                    if (sommet[j] == "a" || sommet[j] == "b")
                        count++;
                if (count == nbrSommet && i == nbrSommet - 1)
                    quitter = true;
                i++;
                if (i == nbrSommet)
                    i = 0;
            }
            
            if (continuer == false)
                MessageBox.Show("Le graphe entre n'est pas bipartie","Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("Le graphe entre est bipartie", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void estBipartieLA()
        {
            int nbrSommet = 0;
            for (int h = 0; h < 50; h++)
            {
                if (array[h] == "")
                    break;
                nbrSommet++;
            }
            string[] sommet = new string[nbrSommet];
            for (int h = 0; h < nbrSommet; h++)
                sommet[h] = "";
            bool quitter = false;
            bool continuer = true;
            sommet[0] = "a";
            int i = 0;
            while (quitter == false && continuer == true)
            {
                if (sommet[i] == "a")
                {
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        if (sommet[int.Parse(array[i][j].ToString())] == "a")
                            continuer = false;
                        else if (sommet[(int.Parse(array[i][j].ToString()))] != "b")
                            sommet[(int.Parse(array[i][j].ToString()))] = "b";
                    }
                }
                else if (sommet[i] == "b")
                {
                    for (int j = 0; j < array[i].Length; j++)
                    {
                        if (sommet[int.Parse(array[i][j].ToString())] == "b")
                            continuer = false;
                        else if (sommet[int.Parse(array[i][j].ToString())] != "a")
                            sommet[int.Parse(array[i][j].ToString())] = "a";
                    }
                }
                int count = 0;
                for (int j = 0; j < nbrSommet; j++)
                    if (sommet[j] == "a" || sommet[j] == "b")
                        count++;
                if (count == nbrSommet && i == nbrSommet - 1)
                    quitter = true;
                i++;
                if (i == nbrSommet)
                    i = 0;
            }
            
            if (continuer == false)
                MessageBox.Show("Le graphe entre n'est pas bipartie", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
            {
                MessageBox.Show("Le graphe entre est bipartie", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        

    }
}
