using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace ооп_лаба_7
{
    class BaseObject
    {
        protected int x; //координаты центра круга
        protected int y;
        protected int r; //радиус
        protected bool select;
        protected int color;

        public BaseObject() { }

        public BaseObject(int _x, int _y)
        {
            this.x = _x;
            this.y = _y;
            this.r = 20;
            this.select = false;
            this.color = 0;
        }

        ~BaseObject() { }

        public virtual void draw(PaintEventArgs e) { }

        public virtual bool ifselected(int _x, int _y) { return false; }

        virtual public bool out_of_form(int _x, int _y, Panel f) //чтобы оставалась в пределах формы
        {
            if (x - r + _x < 0) return true;
            if (y - r + _y < 0) return true;
            if (x + r + _x >= f.Width) return true;
            if (y + r + _y >= f.Height) return true;
            return false;
        }

        virtual public void move(KeyEventArgs e, Panel f)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (out_of_form(0, -7, f) == false) this.y = this.y - 5;
                    break;

                case Keys.Down:
                    if (out_of_form(0, 7, f) == false) this.y = this.y + 5;
                    break;

                case Keys.Left:
                    if (out_of_form(-7, 0, f) == false) this.x = this.x - 5;
                    break;

                case Keys.Right:
                    if (out_of_form(7, 0, f) == false) this.x = this.x + 5;
                    break;
            }
        }

        virtual public void change_size(KeyEventArgs e, Panel f)
        {
            switch (e.KeyCode)
            {
                case Keys.Oemplus:
                    if (out_of_form(0, -7, f) == false && out_of_form(0, 7, f) == false &&
                        out_of_form(-7, 0, f) == false && out_of_form(7, 0, f) == false)
                        this.r = this.r + 1;
                    break;

                case Keys.OemMinus:
                    if (r > 5) this.r = this.r - 1;
                    break;
            }
        }

        virtual public void set_select(bool _select)
        {
            this.select = _select;
        }

        virtual public bool get_select()
        {
            return select;
        }

        virtual public void set_color(int _color)
        {
            this.color = _color;
        }

        virtual public string classname()
        {
            return "BaseObject";
        }

        virtual public void addObject(BaseObject obj) { }

        virtual public BaseObject getObject(int i) { return null; }

        virtual public void set_r(int _r)
        {
            this.r = r + _r;
        }


        virtual public int get_r()
        {
            return r;
        }

        virtual public bool get_r_checked() { return false; }

        virtual public void deleteObjects() { }



        virtual public void save(StreamWriter stream) { }

        virtual public void load(StreamReader stream, AbstractFactory factory) { }


}
}
