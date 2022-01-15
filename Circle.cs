using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace ооп_лаба_7
{
    class Circle : BaseObject
    {
        public Circle() { }

        public Circle(int _x, int _y) : base(_x, _y)
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
            //-r чтобы центр круга оказывался в месте клика, тк эллипс рисуется из верхнего левого угла
            e.Graphics.DrawEllipse(pen, x - r, y - r, 2 * r, 2 * r);
        }

        override public bool ifselected(int _x, int _y)
        {
            if (Math.Pow((_x - x), 2) + Math.Pow((_y - y), 2) <= Math.Pow(this.r, 2))
                return true;
            else return false;
        }

        override public string classname()
        {
            return "Circle";
        }
    }
}
