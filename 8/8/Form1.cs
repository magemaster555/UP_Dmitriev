using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _8
{
    public partial class Form1 : Form
    {
        List<STUDENT> list = new List<STUDENT>();
        int kol = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0 && numericUpDown1.Value <= 5 && numericUpDown2.Value <= 5 && numericUpDown3.Value <= 5)
            {
                list.Add(new STUDENT(textBox1.Text, (int)numericUpDown1.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value));
                label6.Text = "Добавлен "+textBox1.Text;
                textBox1.Text = "";

                STUDENT t = list[list.Count() - 1];
                if (
                        (t.M == 4 && t.I !=4 && t.P!=4) ||
                        (t.I == 4 && t.P != 4 && t.M != 4) ||
                        (t.P == 4 && t.M != 4 && t.I != 4)
                    )
                {
                    label8.Text= "Список сдавших с одной \"4\" - " + (++kol);
                    textBox3.Text += t.NAME + Environment.NewLine;
                }
                    
                updateList();
            }
        }
        private void updateList()
        {
            textBox2.Text = "";
            for(int i = 0; i < list.Count(); i++)
            {
                STUDENT t = list[i];
                textBox2.Text += t.NAME +
                    Environment.NewLine + "Математика: " + t.M +
                    Environment.NewLine + "Информатика: " + t.I +
                    Environment.NewLine + "Физика: " + t.P +
                    Environment.NewLine + "------------------" + Environment.NewLine;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("report.txt", false, System.Text.Encoding.UTF8))
            {
                sw.WriteLine("["+kol+"]"+ Environment.NewLine+textBox3.Text);
            }
            MessageBox.Show("Отчет сохранен в report.txt");
        }
    }
}
public struct STUDENT
{
    public string NAME;
    public int M, I, P;
    public STUDENT(string name, int m, int i, int p)
    {
        NAME = name;
        I = i;
        M = m;
        P = p;
    }
}
