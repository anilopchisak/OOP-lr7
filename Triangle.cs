using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace ооп_лаба_7
{
    class Triangle : BaseObject
    {
        public Triangle() { }

        public Triangle(int _x, int _y) : base(_x, _y)
        {
        }

        override public void draw(PaintEventArgs e)
        {
            Pen pen; pen = new Pen(Color.Black, 1);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (select == true)
            {
                switch (color)
                {
                    case 0:
                        pen = new Pen(Color.Black, 2);
                        break;
                    case 1:
                        pen = new Pen(Color.Red, 2);
                        break;
                    case 2:
                        pen = new Pen(Color.Orange, 2);
                        break;
                    case 3:
                        pen = pen = new Pen(Color.Yellow, 2);
                        break;
                    case 4:
                        pen = new Pen(Color.Green, 2);
                        break;
                    case 5:
                        pen = new Pen(Color.LightBlue, 2);
                        break;
                    case 6:
                        pen = new Pen(Color.Blue, 2);
                        break;
                    case 7:
                        pen = new Pen(Color.Purple, 2);
                        break;
                }
            }
            else
            {
                switch (color)
                {
                    case 0:
                        pen = new Pen(Color.Black, 1);
                        break;
                    case 1:
                        pen = new Pen(Color.Red, 1);
                        break;
                    case 2:
                        pen = new Pen(Color.Orange, 1);
                        break;
                    case 3:
                        pen = pen = new Pen(Color.Yellow, 1);
                        break;
                    case 4:
                        pen = new Pen(Color.Green, 1);
                        break;
                    case 5:
                        pen = new Pen(Color.LightBlue, 1);
                        break;
                    case 6:
                        pen = new Pen(Color.Blue, 1);
                        break;
                    case 7:
                        pen = new Pen(Color.Purple, 1);
                        break;
                }
            }

            Point[] vertices = new Point[3];
            vertices[0] = new Point(x, y - r); //a
            vertices[1] = new Point(x - r, y + r); //b
            vertices[2] = new Point(x + r, y + r); //c
            e.Graphics.DrawPolygon(pen, vertices);
        }

        override public bool ifselected(int _x, int _y)
        {
            Point a = new Point(x, y - r);
            Point b = new Point(x - r, y + r);
            Point c = new Point(x + r, y + r);
            int a1 = (a.X - _x) * (b.Y - a.Y) - (b.X - a.X) * (a.Y - _y);
            int b1 = (b.X - _x) * (c.Y - b.Y) - (c.X - b.X) * (b.Y - _y);
            int c1 = (c.X - _x) * (a.Y - c.Y) - (a.X - c.X) * (c.Y - _y);

            if ((a1 > 0 && b1 > 0 && c1 > 0) || (a1 < 0 && b1 < 0 && c1 < 0))
                return true;
            else return false;
        }

        override public string classname()
        {
            return "Triangle";
        }

        public override void save(StreamWriter stream)
        {
            stream.WriteLine("Triangle");
            stream.WriteLine(x + " " + y + " " + r + " " + color);
        }
        public override void load(StreamReader stream, AbstractFactory factory)
        {
            string[] data = stream.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            x = Convert.ToInt32(data[0]);
            y = Convert.ToInt32(data[1]);
            r = Convert.ToInt32(data[2]);
            color = Convert.ToInt32(data[3]);
        }

    }
}
