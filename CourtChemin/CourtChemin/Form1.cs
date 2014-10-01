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

namespace CourtChemin
{
    public partial class Form1 : Form
    {
        string type;
        string [] array;
        
        public Form1()
        {
            InitializeComponent();
            array = new string[100];
        }

        private void Floyd(string[] temp)
        {
            int i, j, k;
            string a;
            for (k=0;k<100;k++)
            {
                for (i=0;i<100;i++)
                {
                    for (j=0;j<100;j++)
                    {
                        if ((temp[i][k] * temp[k][j] != 0) && i != j )
                        {
                            if ((temp[i][k] + temp[k][j] < temp[i][j]) || temp[i][j] == 0)
                            {
                                a = temp[i][k].ToString() + temp[k][j].ToString();
                                          
                            }
                                }
                    }
                }
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
        }
    }
}
