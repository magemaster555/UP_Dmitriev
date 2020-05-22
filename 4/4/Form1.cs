using System;
using System.Windows.Forms;

namespace _4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            int size = int.Parse(textBox2.Text);
            int[,] m = new int[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (i == j) m[i, j] = 3;
                    if (j > i) m[i, j] = 2;
                    if (j < i) m[i, j] = 1;
                    textBox1.Text += m[i, j] + "   ";
                }
                textBox1.Text += Environment.NewLine;
            }
        }
    }
}
