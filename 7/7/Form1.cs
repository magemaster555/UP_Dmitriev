using System;
using System.Windows.Forms;

namespace _7
{
    public partial class Form1 : Form
    {
        WORKER[] list = new WORKER[10];
        int counter = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.TextLength > 0 && textBox2.TextLength > 0 && textBox3.TextLength > 0 && counter < 10)
            {
                list[counter] = new WORKER(textBox1.Text, textBox2.Text, textBox3.Text);
                listBox1.Items.Add(list[counter].NAME + " - " + list[counter].POST);
                counter++;
                textBox1.Text = textBox2.Text = textBox3.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox4.TextLength > 0)
            {
                textBox5.Text = "";
                for(int i = 0; i < counter; i++)
                {
                    if (list[i].POST.ToLower() == textBox4.Text.ToLower())
                        textBox5.Text += list[i].NAME+", "+list[i].POST+", "+list[i].YEAR + Environment.NewLine;
                }
                if (textBox5.TextLength == 0) textBox5.Text = "Нет сотрудников с такой должностью";
            }
        }
    }
}
public struct WORKER
{
    public string NAME;
    public string POST;
    public string YEAR;
    public WORKER(string name, string post, string year)
    {
        NAME = name;
        POST = post;
        YEAR = year;
    }

}