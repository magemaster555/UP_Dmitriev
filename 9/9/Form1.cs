using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _9
{
    public partial class Form1 : Form
    {
        List<Product> plist = new List<Product>();
        List<Kit> kitlist = new List<Kit>();
        List<Party> partylist = new List<Party>();

        List<string> kitnamelist = new List<string>();
        List<string> partynamelist = new List<string>();

        int selectedAll = -1, selectedList = -1, selectedParty = -1;
        public Form1()
        {
            InitializeComponent();

            ntb.Text = "Молоко";
            stb.Text = "20.05.2020";
            ptb.Text = "1000";
            dtb.Text = "15.05.2020";
        }
        /*
         * Converting string date like "01.02.2020" to UNIX time if dir == true
         * Converting UNIX time to string if dir == false
         */
        private string str_date(string t, bool dir)
        {
            if (dir)
            {
                string[] strdate = t.Split('.');
                var dt = new DateTime(int.Parse(strdate[2]), int.Parse(strdate[1]), int.Parse(strdate[0]), 0, 0, 0, DateTimeKind.Local);
                var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                var unixDateTime = (dt - epoch).TotalSeconds;
                return unixDateTime.ToString();
            }
            else
            {
                DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(int.Parse(t)).ToLocalTime();
                string r = dtDateTime.Day.ToString() + "." + dtDateTime.Month.ToString() + "." + dtDateTime.Year.ToString();
                return r;
            }
        }
        private void updateLB(ListBox lb)
        {
            lb.Items.Clear();
            switch (lb.Name.ToString())
            {
                case "listBox1":
                    for (int i = 0; i < plist.Count(); i++)
                        lb.Items.Add(plist[i].name);
                    break;
                case "listBox4":
                    for (int i = 0; i < plist.Count(); i++)
                        lb.Items.Add(plist[i].name);
                    break;
                case "listBox2":
                    for (int i = 0; i < kitlist.Count(); i++)
                        lb.Items.Add(kitlist[i].kitname);
                    break;
                case "listBox3":
                    for (int i = 0; i < partylist.Count(); i++)
                        lb.Items.Add(partylist[i].pname);
                    break;
                default:
                    MessageBox.Show("Ошибка определения листбокса");
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ntb.TextLength > 0 && ptb.TextLength > 0 && stb.TextLength > 0 && dtb.TextLength > 0)
            {
                try
                {
                    var p = new Product(ntb.Text, int.Parse(str_date(stb.Text, true)), int.Parse(ptb.Text), int.Parse(str_date(dtb.Text, true)));
                    //MessageBox.Show(p.getSrok());
                    plist.Add(p);
                    updateLB(listBox1);
                    updateLB(listBox4);
                    updateOutOfDate();
                    textBox2.Text = "";
                    for (int i = 0; i < plist.Count(); i++)
                        textBox2.Text += ("================" 
                            + Environment.NewLine 
                            + "Название: "+plist[i].name
                            + Environment.NewLine
                            + "Годен до: "+ str_date(plist[i].srok.ToString(), false)
                            + Environment.NewLine
                            + "Цена: " + plist[i].price
                            + Environment.NewLine
                            + "Дата производства: " + str_date(plist[i].date.ToString(), false)
                            + Environment.NewLine);
                    ntb.Text = ptb.Text = stb.Text = dtb.Text = "";

                }
                catch
                {
                    MessageBox.Show("Проверьте правильность введенных данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        public void updateOutOfDate()
        {
            listBox5.Items.Clear();
            for (int i = 0; i < plist.Count(); i++)
            {
                int curtime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
                if (plist[i].srok - curtime < 0)
                    listBox5.Items.Add(plist[i].name);
            }
        }
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox2.SelectedIndex != -1)
                selectedList = listBox2.SelectedIndex;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (selectedList >= 0)
            {
                kitlist[selectedList].PrintKit();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if(listBox4.SelectedIndex >= 0 && listBox4.SelectedIndex >= 0 && comboBox1.SelectedIndex >= 0) 
            {
                bool flag = false;
                bool flag1 = false;
                int _i = 0;
                for (int i = 0; i < kitlist.Count(); i++)
                {
                    if(kitlist[i].kitname == comboBox1.SelectedItem.ToString())
                    {
                        flag = true;
                        _i = i;
                    }
                    if(kitlist[i].kitname == listBox4.SelectedItem.ToString())
                    {
                        flag1 = true;
                    }
                }
                if (!flag1)
                {
                    if (flag)
                    {
                        kitlist[_i].list.Add(plist[listBox4.SelectedIndex]);
                        MessageBox.Show("Добалвено!");
                    }
                    else
                        MessageBox.Show("Выберите название комплекта из списка ниже");
                }
                else
                    MessageBox.Show("Такой товар уже есть в комплекте");
            }
            else
                MessageBox.Show("Выберите название комплекта из списка ниже");
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0)
            {
                kitnamelist.Add(textBox1.Text);
                kitlist.Add(new Kit(textBox1.Text));
                comboBox1.Items.Clear();
                listBox2.Items.Clear();
                for (int i = 0; i < kitnamelist.Count(); i++)
                {
                    comboBox1.Items.Add(kitnamelist[i]);
                    listBox2.Items.Add(kitnamelist[i]);
                }
                updateLB(listBox2);
                textBox1.Text = "";
            }
        }
        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                partynamelist.Add(textBox3.Text);
                partylist.Add(new Party(textBox3.Text, (int)numericUpDown1.Value));
                comboBox2.Items.Clear();
                listBox3.Items.Clear();
                for (int i = 0; i < partynamelist.Count(); i++)
                {
                    comboBox2.Items.Add(partynamelist[i]);
                    listBox3.Items.Add(partynamelist[i]);
                }
            }
            updateLB(listBox3);
            textBox3.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex >= 0 && listBox4.SelectedIndex >= 0)
            {
                bool flag = false;
                bool flag1 = false;
                int _i = 0;
                for (int i = 0; i < partylist.Count(); i++)
                {
                    if (partylist[i].pname == comboBox2.SelectedItem.ToString())
                    {
                        flag = true;
                        _i = i;
                    }
                    if (partylist[i].pname == listBox4.SelectedItem.ToString())
                        flag1 = true;
                }
                if (!flag1)
                {
                    if (flag)
                    {
                        partylist[_i].list.Add(plist[listBox4.SelectedIndex]);
                        MessageBox.Show("Добалвено!");
                    }
                    else
                        MessageBox.Show("Выберите название комплекта из списка ниже");
                }
                else
                    MessageBox.Show("Такой товар уже есть в партии");
            }
            else
                MessageBox.Show("Такой товар уже есть в партии");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (selectedParty >= 0)
            {
                partylist[selectedParty].PrintParty();
            }
        }
        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedParty = listBox3.SelectedIndex;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
                selectedAll = listBox1.SelectedIndex;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if(selectedAll >= 0)
            {
                string s = plist[selectedAll].getSrok();
                if (s == "out") s = "Срок годности вышел";
                MessageBox.Show(s);
            }
               
        }
    }
}
abstract class Tovar
{
    public string name;
    public int srok, price, date;
    public string getSrok()
    {
        int curtime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
        int rem = srok - curtime;
        return (rem > 0) ? "Продукт годен еще " + TStoStr(rem): "out";
    }
    private string TStoStr(int ts)
    {
        string result = "";
        double hours = ts / 3600, days = 0;
        if(hours > 24)
        {
            days = Math.Floor(hours / 24);
            hours -= (days * 24);
        }
        result = (days == 0) ? hours + " часов" :  days + " дня(дней) и "+ hours + " часов";
        return result;
    }
}
class Product : Tovar
{
    private string inttostr(long t)
    {
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(t).ToLocalTime();
        string r = dtDateTime.Day.ToString() + "." + dtDateTime.Month.ToString() + "." + dtDateTime.Year.ToString();
        return r;
    }
    public Product(string n, int s, int p, int d) 
    {
        name = n;
        srok = s;
        price = p;
        date = d;
    }
    public string get(bool n = false)
    {
        return n 
            ?string.Format("Название: {0}, Срок годности: {1}, Цена: {2}, Дата поступления: {3}", name, inttostr(srok), price, inttostr(date)) 
            :string.Format("Название: {0}"+Environment.NewLine + "Срок годности: {1}"+Environment.NewLine + "Цена: {2}"+Environment.NewLine + "Дата поступления: {3}", name, inttostr(srok), price, inttostr(date));
    }
}
class Party : Tovar
{
    public List<Product> list = new List<Product>();
    public string pname { get; }
    private int kolvo;
    public Party(string pn, int k)
    {
        kolvo = k;
        pname = pn;
    }
    public void PrintParty()
    {
        string ret = "";
        if (kolvo != -1)
            ret = Environment.NewLine + "Количество: " + kolvo + Environment.NewLine;
        string l = "=============\n";
        int i;
        for (i = 0; i < list.Count(); i++)
            l += list[i].get() + ret + "=============\n";
        if (i != 0)
            MessageBox.Show("Партия \"" + pname + "\"\nТовары:\n" + l);
        else
            MessageBox.Show("Партия пустая");
    }
}
class Kit : Tovar
{
    public List<Product> list = new List<Product>();
    public string kitname;
    public Kit(string kn)
    {
        kitname = kn;
    }
    public void PrintKit()
    {
        string l = "=============\n";
        int i;
        for (i = 0; i < list.Count(); i++)
            l += list[i].get() + "\n=============\n";
        if (i != 0)
            MessageBox.Show("Комплект \"" + kitname + "\"\nТовары:\n" + l);
        else
            MessageBox.Show("Комплект пустой");
    }
}