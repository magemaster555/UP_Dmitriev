using System;
using System.Windows.Forms;

namespace _2
{
    public partial class Form1 : Form
    {
        static int MAX_ELEMS = 100;
        int[] m = new int[MAX_ELEMS]; 
        public Form1()
        {
            InitializeComponent();
            createArray();
        }
        private void createArray()
        {
            Random rnd = new Random();
            textBox2.Text = "";
            for (int i = 0; i < MAX_ELEMS; i++)
            {
                m[i] = rnd.Next(-100, 100);
                textBox2.Text += m[i] + " ";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            createArray();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int s = m[0] + m[MAX_ELEMS - 1];
            int r = 0;
            for (int i = 0; i < MAX_ELEMS; i++)
            {
                if (i % 2 != 0 && m[i] > s) r += m[i];
            }
            textBox1.Text = "";
            textBox1.Text = r.ToString();
        }
    }
}
