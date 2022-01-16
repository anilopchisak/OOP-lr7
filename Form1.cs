using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace ооп_лаба_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        Circle circle = new Circle();
        Square square = new Square();
        Triangle triangle = new Triangle();
        Storage storage = new Storage();
        Group group = new Group();
        bool ctrl = false;
        bool chosen_circle = true; bool chosen_square = false; bool chosen_triangle = false;

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool new_figure = true;
                for (int i = 0; i < storage.getCount(); i++) //для каждого объекта массива
                {
                    if (storage.get_current_obj(i).ifselected(e.X, e.Y) == true) //если на объект нажали
                    {
                        new_figure = false;
                        if (ctrl == true)
                        {
                            for (int j = 0; j < storage.getCount(); j++)
                            {
                                if (storage.get_current_obj(j).get_select() == true && j != i)
                                {
                                    group = new Group();
                                    group.addObject(storage.get_current_obj(i));
                                    group.addObject(storage.get_current_obj(j));
                                    storage.deleteObject(i);
                                    storage.deleteObject(j);
                                    storage.change_array();
                                    storage.addGroup(group);
                                }
                            }
                            storage.get_current_obj(storage.getCount() - 1).set_select(true);
                        }
                        else
                        {
                            for (int j = 0; j < storage.getCount(); j++)
                            {
                                if (storage.get_current_obj(j).get_select() == true) //снимаем выделение у всех объектов
                                    storage.get_current_obj(j).set_select(false);

                            }
                            storage.get_current_obj(i).set_select(true); //ставим выделение у объекта на который нажали
                        }
                    }
                }

                if (new_figure == true)
                {
                    for (int i = 0; i < storage.getCount(); i++) storage.get_current_obj(i).set_select(false); //снимаем выделение у всех объектов

                    if (chosen_circle == true)
                    {
                        circle = new Circle(e.X, e.Y);
                        storage.addCircle(circle);
                    }
                    else if (chosen_square == true)
                    {
                        square = new Square(e.X, e.Y);
                        storage.addSquare(square);
                    }
                    else if (chosen_triangle == true)
                    {
                        triangle = new Triangle(e.X, e.Y);
                        storage.addTriangle(triangle);
                    }

                    storage.get_current_obj(storage.getCount() - 1).set_select(true);
                }
                Refresh();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (storage.getCount() != 0)
            {
                for (int i = 0; i < storage.getCount(); i++) storage.get_current_obj(i).draw(e);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) ctrl = true;
            if (e.KeyCode == Keys.Delete)
            {
                for (int i = 0; i < storage.getCount(); i++)
                    if (storage.get_current_obj(i).get_select() == true)
                    {
                        storage.get_current_obj(i).set_select(false);
                        storage.deleteObject(i);
                    }
                storage.change_array();

                Refresh();
            }
            if (e.KeyCode == Keys.Oemplus || e.KeyCode == Keys.OemMinus)
            {
                for (int i = 0; i < storage.getCount(); i++)
                    if (storage.get_current_obj(i).get_select() == true)
                        storage.get_current_obj(i).change_size(e, panel1);
                Refresh();
            }
            if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                for (int i = 0; i < storage.getCount(); i++)
                    if (storage.get_current_obj(i).get_select() == true)
                        storage.get_current_obj(i).move(e, panel1);
                Refresh();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
                ctrl = false;
        }

        private void tsbtn_Ungroup_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < storage.getCount(); i++)
                if (storage.get_current_obj(i).get_select() == true)
                    if (storage.get_current_obj(i).classname() == "Group")
                    {
                        for (int j = 0; j < 2; j++)
                            storage.addBase(storage.get_current_obj(i).getObject(j));
                        for (int j = 0; j < storage.getCount(); j++)
                        {
                            if (storage.get_current_obj(j).get_select() == true) //снимаем выделение у всех объектов
                                storage.get_current_obj(j).set_select(false);
                        }
                        storage.deleteObject(i);
                        storage.change_array();
                        Refresh();
                        break;
                    }
        }

        private void tsbtn_Save_Click(object sender, EventArgs e)
        {
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream f = new FileStream(saveFileDialog.FileName, FileMode.Create);
                StreamWriter stream = new StreamWriter(f);
                stream.WriteLine(storage.getCount());
                if (storage.getCount() != 0)
                {
                    for (int i = 0; i < storage.getCount(); i++) storage.get_current_obj(i).save(stream);
                }
                stream.Close();
                f.Close();
            }
            this.Activate();
        }

        private void tsbtn_Load_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileStream f = new FileStream(openFileDialog.FileName, FileMode.Open);
                StreamReader stream = new StreamReader(f);
                int i = Convert.ToInt32(stream.ReadLine());
                MyAbstractFactory factory = new MyAbstractFactory();
                for (; i > 0; i--)
                {
                    string tmp = stream.ReadLine();
                    storage.addBase(factory.CreateBaseObject(tmp));
                    storage.get_current_obj(storage.getCount() - 1).load(stream, factory);
                }
                stream.Close();
                f.Close();
            }
            Refresh();
            //panel1.Invalidate();
            this.Activate();
        }

        private void tsbtn_Circle_Click(object sender, EventArgs e)
        {
            chosen_circle = true;
            chosen_square = false;
            chosen_triangle = false;
        }

        private void tsbtn_Square_Click(object sender, EventArgs e)
        {
            chosen_circle = false;
            chosen_square = true;
            chosen_triangle = false;
        }

        private void tsbtn_Triangle_Click(object sender, EventArgs e)
        {
            chosen_circle = false;
            chosen_square = false;
            chosen_triangle = true;
        }

        private void change_color(int color)
        {
            if (storage.getCount() != 0)
            {
                for (int i = 0; i < storage.getCount(); i++)
                    if (storage.get_current_obj(i).get_select() == true)
                        storage.get_current_obj(i).set_color(color);
                Refresh();
            }
        } //меняем цвет выделенных фигур

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(1);
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(2);
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(3);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(4);
        }

        private void lightBlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(5);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(6);
        }

        private void purpleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(7);
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            change_color(0);
        }


    }
}
