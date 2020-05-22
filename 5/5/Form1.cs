using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace _5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = textBox1.Text;
            s = Regex.Replace(s, "[0-9]", "");
            textBox2.Text = s;
        }
    }
}
