using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace _6
{
    public partial class Form1 : Form
    {
        List<Point> points = new List<Point>();
        List<Line> lines = new List<Line>();
        public Form1()
        {
            InitializeComponent();
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader(@"coords.txt");
            while ((line = file.ReadLine()) != null)
            {
                if (line.Length <= 0) continue;
                string[] s = line.Split(' ');
                Point p = new Point(double.Parse(s[0]), double.Parse(s[1]));
                points.Add(p);
            }
            file.Close();
            for(int i = 0; i < points.Count(); i++)
            {
                Point p = points[i];
                for (int j = 0; j < points.Count(); j++)
                {
                    Point pp = points[j];
                    if (p.X == pp.X && p.Y == pp.Y) continue;
                    bool flag = false;
                    Line temp = new Line(p, pp);
                    for (int l = 0; l < lines.Count(); l++)
                    { if (temp.Len == lines[l].Len) { flag = true; break; } }
                    if (!flag)
                        lines.Add(temp);
                }
            }
            int max_i = 0, min_i = 0;
            for (int i = 0; i < lines.Count(); i++)
            {
                double cur = lines[i].Len;
                if (cur > lines[max_i].Len) max_i = i;
                if (cur < lines[min_i].Len) min_i = i;
            }
            textBox1.Text += "Самый длинный отрезок:"+Environment.NewLine;
            textBox1.Text += string.Format("\tДлина: {0}. Точки: ({1}, {2}) .. ({3}, {4})", lines[max_i].Len, lines[max_i].P1.X, lines[max_i].P1.Y, lines[max_i].P2.X, lines[max_i].P2.Y) +Environment.NewLine + Environment.NewLine;
            textBox1.Text += "Самый короткий отрезок:" + Environment.NewLine;
            textBox1.Text += string.Format("\tДлина: {0}. Точки: ({1}, {2}) .. ({3}, {4})", lines[min_i].Len, lines[min_i].P1.X, lines[min_i].P1.Y, lines[min_i].P2.X, lines[min_i].P2.Y) +Environment.NewLine;
        }
    }
}
public struct Point
{
    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }
    public double X { get; }
    public double Y { get; }
}
public struct Line
{
    public Line(Point p1, Point p2)
    {
        P1 = p1; P2 = p2;
        Len = Math.Sqrt(Math.Pow((p2.X - p1.X), 2) + Math.Pow((p2.Y - p1.Y), 2));
    }
    public double Len;
    public Point P1 { get; }
    public Point P2 { get; }
}