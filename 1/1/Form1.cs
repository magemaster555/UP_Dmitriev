using System;
using System.Windows.Forms;

namespace _1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int n = int.Parse(textBox1.Text), min = 10, max = 0, min_i = -1, max_i = -1;
            int[] m = new int[4];
            for(int i = 0; i < 4; i++)
            {
                m[i] = GetDigit(n, i+1);
                if (m[i] > max) { max = m[i]; max_i = i; } 
                if (m[i] < min) { min = m[i]; min_i = i; } 
            }

            m[min_i] = max;
            m[max_i] = min;
            textBox1.Text = "";
            for (int i = 0; i < 4; i++)
                textBox1.Text += m[i];
        }
        private int GetDigit(int x, int digitNumber)
        {

            int digitCount = (int)Math.Log10(x) + 1;
            if (digitNumber > digitCount)
                return 0;

            var pow = (int)Math.Pow(10, digitCount - digitNumber);
            return (x / pow) % 10;
        }
    }
}
