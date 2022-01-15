using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ооп_лаба_7
{
    class Group : BaseObject
    {
        private int _maxcount; // максимально возможное количество фигур в группе
        private int _count; // текущее количество фигур в группе
        private List <BaseObject> _objects; // массив ссылок на хранимые фигуры

        public Group ()
        {
            _maxcount = 0; 
            _count = 0;
            _objects = new List<BaseObject>();
            for (int i = 0; i < _maxcount; i++)
                _objects[i] = null;
        }

        ~Group()
        {
            // очищаем массив ссылок (они удалятся, если никто их не держит)
            for (int i = 0; i < _count; i++)
                _objects[i] = null;
            _objects.Clear(); // очищаем сам массив
        }

        override public void addObject(BaseObject obj)
        {
            _objects.Add(obj);
            _count++; _maxcount++;
        }


        override public void move(KeyEventArgs e, Panel f)
        {
            bool flag_up = false; bool flag_down = false; bool flag_left = false; bool flag_right = false;
            for (int i = 0; i < _count; i++)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        if (_objects[i].out_of_form(0, -7, f) == true) flag_up = true;
                        break;

                    case Keys.Down:
                        if (_objects[i].out_of_form(0, 7, f) == true) flag_down = true;
                        break;

                    case Keys.Left:
                        if (_objects[i].out_of_form(-7, 0, f) == true) flag_left = true;
                        break;

                    case Keys.Right:
                        if (_objects[i].out_of_form(7, 0, f) == true) flag_right = true;
                        break;
                }
                if (flag_up == true || flag_down == true || flag_left == true || flag_right == true) break;
            }

            switch (e.KeyCode)
            {
            case Keys.Up:
            {
                if (flag_up == false)
                    for (int i = 0; i < _count; i++)
                        _objects[i].move(e, f);
                break;
            }
            case Keys.Down:
            {
                if (flag_down == false)
                    for (int i = 0; i < _count; i++)
                        _objects[i].move(e, f);
                break;
            }

            case Keys.Left:
                {
                    if (flag_left == false)
                        for (int i = 0; i < _count; i++)
                            _objects[i].move(e, f);
                    break;
                }

            case Keys.Right:
            {
                if (flag_right == false)
                    for (int i = 0; i < _count; i++)
                        _objects[i].move(e, f);
                break;
            }
            }   
        }

        override public bool out_of_form(int _x, int _y, Panel f) //чтобы оставалась в пределах формы
        {
            for (int i = 0; i < _count; i++)
            {
                if (_objects[i].out_of_form(_x, _y, f) == true) return true;
            }
            return false;
        }

        override public void change_size(KeyEventArgs e, Panel f)
        {
            bool flag_plus = false; bool flag_minus = false;
            switch (e.KeyCode)
            {
                case Keys.Oemplus:
                    for (int i = 0; i < _count; i++)
                        if (_objects[i].out_of_form(0, -7, f) == true || _objects[i].out_of_form(0, 7, f) == true ||
                        _objects[i].out_of_form(-7, 0, f) == true || _objects[i].out_of_form(7, 0, f) == true)
                        {
                            flag_plus = true;
                            break;
                        }
                    break;

                case Keys.OemMinus:
                    for (int i = 0; i < _count; i++)
                        if (_objects[i].classname() == "Group")
                        {
                            flag_minus = _objects[i].get_r_checked();
                            if (flag_minus == true)
                                break;
                        }
                        else
                        {
                            if (_objects[i].get_r() < 5)
                            {
                                flag_minus = true;
                                break;
                            }
                        }
                    break;
            }
            switch (e.KeyCode)
            {
                case Keys.Oemplus:
                        if (flag_plus == false)
                        for (int i = 0; i < _count; i++) _objects[i].set_r(1);
                    break;

                case Keys.OemMinus:
                    if (flag_minus == false)
                        for (int i = 0; i < _count; i++) _objects[i].set_r(-1); ;
                    break;
            }
        }

        override public void set_r(int _r)
        {
            for (int i = 0; i < _count; i++)
                _objects[i].set_r(_r);
        }

        public override bool get_r_checked()
        {
            for (int i = 0; i < _count; i++)
            {
                if (_objects[i].classname() == "Group")
                {
                    if (_objects[i].get_r_checked() == true) return true;
                }
                else
                {
                    if (_objects[i].get_r() < 5) return true;
                }
            }
            return false;
        }


        public override void draw(PaintEventArgs e)
        {
            for (int i = 0; i < _count; i++)
                _objects[i].draw(e);
        }

        override public bool ifselected(int _x, int _y)
        {
            for (int i = 0; i < _count; i++)
                if (_objects[i].ifselected(_x, _y) == true)
                    return true;
            return false;  
        }


        public override void set_select(bool _select)
        {
            for (int i = 0; i < _count; i++)
                _objects[i].set_select(_select);
        }

        public override bool get_select()
        {
            return _objects[0].get_select();
        }

        override public string classname()
        {
            return "Group";
        }

        public override void set_color(int _color)
        {
            for (int i = 0; i < _count; i++)
                _objects[i].set_color(_color);
        }

        public override BaseObject getObject(int i)
        {
            return _objects[i];
        }




    }
}
