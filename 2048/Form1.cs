using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace _2048
{
    public partial class Form1 : Form
    {
        private int[,] array = new int[4, 4];//массив с цифрами, дубляж игрового поля
        Random rand = new Random();
        PictureBox[,] field;//серые ячейки
        PictureBox[,] cells = new PictureBox[4, 4];//ячейки с цифрами
        private bool isJoined;

        public Form1()
        {
            
            InitializeComponent();
            this.BackColor = Color.FromArgb(251, 246, 240);
            pictureBack.BackColor = Color.FromArgb(189, 173, 160);

            //this.KeyPreview = true;
            field = new PictureBox[4, 4] { { pb00, pb01, pb02, pb03 }, { pb10, pb11, pb12, pb13 }, { pb20, pb21, pb22, pb23 }, { pb30, pb31, pb32, pb33 } };

            foreach (PictureBox pb in field)
            {
                pb.BackColor = Color.FromArgb(214, 205, 196);
            }
            for (int k = 0; k < 6; k++)
            {
                GenerationRandomCell();
            }
            KeyDown += new KeyEventHandler(OKP);
        }


        private void OKP(object sender, KeyEventArgs e)//функция которая вызывается во время нажатия одной из стрелочок клавиатуры
        {
            switch (e.KeyCode.ToString())
            {
                case "Down":
                    for (int j = 0; j < array.GetLength(0); j++)
                    {
                        isJoined = false;
                        for (int i = 2; i >= 0; i--)
                        {
                            if (array[i, j] != 0)
                            {
                                for (int k = i + 1; k < array.GetLength(0); k++)
                                {
                                    if (array[k, j] == 0)
                                    {
                                        array[k, j] = array[k - 1, j];
                                        array[k - 1, j] = 0;
                                        cells[k - 1, j].Location = field[k, j].Location;
                                        cells[k, j] = cells[k - 1, j];
                                        cells[k - 1, j] = null;
                                    }
                                    else
                                    {
                                        if (array[k, j] == array[k - 1, j] && isJoined == false)
                                        {
                                            array[k, j] += array[k - 1, j];
                                            array[k - 1, j] = 0;
                                            cells[k - 1, j].Location = cells[k, j].Location;

                                            //создание нового элемента
                                            PictureBox pictureBox = new PictureBox();
                                            pictureBox.Location = cells[k, j].Location;
                                            pictureBox.Size = cells[k, j].Size;
                                            DrawNumeral(pictureBox, array[k, j]);
                                            Controls.Add(pictureBox);
                                            pictureBox.BringToFront();
                                            this.Controls.Remove(cells[k, j]);
                                            this.Controls.Remove(cells[k - 1, j]);
                                            cells[k, j] = null;
                                            cells[k - 1, j] = null;
                                            cells[k, j] = pictureBox;
                                            isJoined = true;
                                        }
                                        else
                                        {
                                            isJoined = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GenerationRandomCell();
                    break;
                case "Left":

                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        isJoined = false;
                        for (int j = 1; j < array.GetLength(0); j++)
                        {
                            if (array[i, j] != 0)
                            {
                                for (int k = j - 1; k >= 0; k--)
                                {
                                    if (array[i, k] == 0)
                                    {
                                        array[i, k] = array[i, k + 1];
                                        array[i, k + 1] = 0;
                                        cells[i, k + 1].Location = field[i, k].Location;
                                        cells[i, k] = cells[i, k + 1];
                                        cells[i, k + 1] = null;
                                    }
                                    else
                                    {
                                        if (array[i, k] == array[i, k + 1] && isJoined == false)
                                        {
                                            array[i, k] += array[i, k + 1];
                                            array[i, k + 1] = 0;
                                            cells[i, k + 1].Location = cells[i, k].Location;

                                            //создание нового элемента
                                            PictureBox pictureBox = new PictureBox();
                                            pictureBox.Location = cells[i, k].Location;
                                            pictureBox.Size = cells[i, k].Size;
                                            DrawNumeral(pictureBox, array[i, k]);
                                            Controls.Add(pictureBox);
                                            pictureBox.BringToFront();
                                            this.Controls.Remove(cells[i, k]);
                                            this.Controls.Remove(cells[i, k + 1]);
                                            cells[i, k] = null;
                                            cells[i, k + 1] = null;
                                            cells[i, k] = pictureBox;
                                            isJoined = true;
                                        }
                                        else
                                        {
                                            isJoined = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GenerationRandomCell();
                    break;
                case "Right":

                    for (int i = 0; i < array.GetLength(0); i++)
                    {
                        isJoined = false;
                        for (int j = 2; j >= 0; j--)
                        {
                            if (array[i, j] != 0)
                            {
                                for (int k = j + 1; k < array.GetLength(0); k++)
                                {
                                    if (array[i, k] == 0)
                                    {
                                        array[i, k] = array[i, k - 1];
                                        array[i, k - 1] = 0;
                                        cells[i, k - 1].Location = field[i, k].Location;
                                        cells[i, k] = cells[i, k - 1];
                                        cells[i, k - 1] = null;
                                    }
                                    else
                                    {
                                        if (array[i, k] == array[i, k - 1] && isJoined == false)
                                        {
                                            array[i, k] += array[i, k - 1];
                                            array[i, k - 1] = 0;
                                            cells[i, k - 1].Location = cells[i, k].Location;

                                            //создание нового элемента
                                            PictureBox pictureBox = new PictureBox();
                                            pictureBox.Location = cells[i, k].Location;
                                            pictureBox.Size = cells[i, k].Size;
                                            DrawNumeral(pictureBox, array[i, k]);
                                            Controls.Add(pictureBox);
                                            pictureBox.BringToFront();
                                            this.Controls.Remove(cells[i, k]);
                                            this.Controls.Remove(cells[i, k - 1]);
                                            cells[i, k] = null;
                                            cells[i, k - 1] = null;
                                            cells[i, k] = pictureBox;
                                            isJoined = true;
                                        }
                                        else
                                        {
                                            isJoined = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GenerationRandomCell();
                    break;
                case "Up":

                    for (int j = 0; j < array.GetLength(0); j++)
                    {
                        isJoined = false;
                        for (int i = 1; i < array.GetLength(0); i++)
                        {
                            if (array[i, j] != 0)
                            {
                                for (int k = i - 1; k >= 0; k--)
                                {
                                    if (array[k, j] == 0)
                                    {
                                        array[k, j] = array[k + 1, j];
                                        array[k + 1, j] = 0;
                                        cells[k + 1, j].Location = field[k, j].Location;
                                        cells[k, j] = cells[k + 1, j];
                                        cells[k + 1, j] = null;
                                    }
                                    else
                                    {
                                        if (array[k, j] == array[k + 1, j] && isJoined == false)
                                        {
                                            array[k, j] += array[k + 1, j];
                                            array[k + 1, j] = 0;
                                            cells[k + 1, j].Location = cells[k, j].Location;

                                            //создание нового элемента
                                            PictureBox pictureBox = new PictureBox();
                                            pictureBox.Location = cells[k, j].Location;
                                            pictureBox.Size = cells[k, j].Size;
                                            DrawNumeral(pictureBox, array[k, j]);
                                            Controls.Add(pictureBox);
                                            pictureBox.BringToFront();
                                            Controls.Remove(cells[k, j]);
                                            Controls.Remove(cells[k + 1, j]);
                                            cells[k, j] = null;
                                            cells[k + 1, j] = null;
                                            cells[k, j] = pictureBox;
                                            isJoined = true;
                                        }
                                        else
                                        {
                                            isJoined = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    GenerationRandomCell();
                    break;
            }
        }
        private void DrawNumeral(PictureBox pb, int text)
        {
            Brush brush = Brushes.Black;
            switch (text.ToString())
            {
                case "2":
                    pb.BackColor = Color.FromArgb(238,228,218);
              
                    brush = Brushes.Black;
                    break;
                case "4":
                    pb.BackColor = Color.FromArgb(236,224,200);
                    brush = Brushes.Black;
                    break;
                case "8":
                    pb.BackColor = Color.FromArgb(245,176,117);
                    brush = Brushes.White;
                    break;
                case "16":
                    pb.BackColor = Color.Pink;
                    brush = Brushes.Black;
                    break;
                case "32":
                    pb.BackColor = Color.FromArgb(245,124,95);
                    brush = Brushes.White;
                    break;
                case "64":
                    pb.BackColor = Color.Green;
                    brush = Brushes.Black;
                    break;
                case "128":
                    pb.BackColor = Color.Green;
                    brush = Brushes.Black;
                    break;
                case "256":
                    pb.BackColor = Color.Green;
                    brush = Brushes.Black;
                    break;
                case "512":
                    pb.BackColor = Color.Green;
                    brush = Brushes.Black;
                    break;
                case "1028":
                    pb.BackColor = Color.Green;
                    brush = Brushes.Black;
                    break;
                case "2048":
                    pb.BackColor = Color.Green;
                    brush = Brushes.Black;
                    break;
                default:
                    pb.BackColor = Color.White;
                    break;
            }
            pb.Paint += new PaintEventHandler((sender, e) =>
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Font font = new Font("Arial", 30, FontStyle.Bold);
                SizeF textSize = e.Graphics.MeasureString(text.ToString(), font);
                PointF locationToDraw = new PointF();
                locationToDraw.X = (pb.Width / 2) - (textSize.Width / 2);
                locationToDraw.Y = (pb.Height / 2) - (textSize.Height / 2);
                e.Graphics.DrawString(text.ToString(), font, brush, locationToDraw);
            });
        }
        private void GenerationRandomCell()
        {
            bool flag;
            do
            {
                int i = rand.Next(0, 4);
                int j = rand.Next(0, 4);
                int num = rand.Next(0, 2);

                if (array[i, j] == 0)
                {
                    array[i, j] = num == 0 ? 2 : 4;
                    PictureBox pictureBox = new PictureBox();
                    pictureBox.Location = field[i, j].Location;
                    pictureBox.Size = field[i, j].Size;
                    DrawNumeral(pictureBox, num == 0 ? 2 : 4);
                    Controls.Add(pictureBox);
                    pictureBox.BringToFront();
                    cells[i, j] = pictureBox;
                    flag = true;
                }
                else
                {
                    flag = false;
                }
            } while (flag == false);
        }

        private void pb02_Click(object sender, EventArgs e)
        {

        }
        /*private void button1_Click(object sender, EventArgs e)//влево
{
   bool isJoined;
   for (int i = 0; i < array.GetLength(0); i++)
   {
       isJoined = false;
       for (int j = 1; j < array.GetLength(0); j++)
       {
           if (array[i, j] != 0)
           {
               for (int k = j - 1; k >= 0; k--)
               {
                   if (array[i, k] == 0)
                   {
                       array[i, k] = array[i, k + 1];
                       array[i, k + 1] = 0;
                       cells[i, k + 1].Location = field[i, k].Location;
                       cells[i, k] = cells[i, k + 1];
                       cells[i, k + 1] = null;
                   }
                   else
                   {
                       if (array[i, k] == array[i, k + 1] && isJoined == false)
                       {
                           array[i, k] += array[i, k + 1];
                           array[i, k + 1] = 0;
                           cells[i, k + 1].Location = cells[i, k].Location;

                           //создание нового элемента
                           PictureBox pictureBox = new PictureBox();
                           pictureBox.Location = cells[i, k].Location;
                           pictureBox.Size = cells[i, k].Size;
                           DrawNumeral(pictureBox, array[i, k]);
                           Controls.Add(pictureBox);
                           pictureBox.BringToFront();
                           this.Controls.Remove(cells[i, k]);
                           this.Controls.Remove(cells[i, k+1]);
                           cells[i, k] = null;
                           cells[i, k + 1] = null;
                           cells[i, k] = pictureBox;
                           isJoined = true;
                       }
                       else
                       {
                           isJoined = false;
                       }
                   }
               }
           }
       }
   }
   //GenerationRandomCell();
}

private void button2_Click(object sender, EventArgs e)//вверх
{
   bool isJoined;
   for (int j = 0; j < array.GetLength(0); j++)
   {
       isJoined = false;
       for (int i = 1; i < array.GetLength(0); i++)
       {
           if (array[i, j] != 0)
           {
               for (int k = i - 1; k >= 0; k--)
               {
                   if (array[k, j] == 0)
                   {
                       array[k, j] = array[k+1, j];
                       array[k+1, j] = 0;
                       cells[k+1, j].Location = field[k, j].Location;
                       cells[k, j] = cells[k+1, j];
                       cells[k+1, j] = null;
                   }
                   else
                   {
                       if (array[k, j] == array[k+1, j] && isJoined == false)
                       {
                           array[k, j] += array[k+1, j];
                           array[k+1, j] = 0;
                           cells[k+1, j].Location = cells[k, j].Location;

                           //создание нового элемента
                           PictureBox pictureBox = new PictureBox();
                           pictureBox.Location = cells[k, j].Location;
                           pictureBox.Size = cells[k, j].Size;
                           DrawNumeral(pictureBox, array[k, j]);
                           Controls.Add(pictureBox);
                           pictureBox.BringToFront();
                           Controls.Remove(cells[k, j]);
                           Controls.Remove(cells[k+1, j]);
                           cells[k, j] = null;
                           cells[k+1, j] = null;
                           cells[k, j] = pictureBox;
                           isJoined = true;
                       }
                       else
                       {
                           isJoined = false;
                       }
                   }
               }
           }
       }
   }
   // GenerationRandomCell();

}

private void button3_Click(object sender, EventArgs e)//вправо
{
   bool isJoined;
   for (int i = 0; i < array.GetLength(0); i++)
   {
       isJoined = false;
       for (int j = 2; j >= 0; j--)
       {
           if (array[i, j] != 0)
           {
               for (int k = j + 1; k < array.GetLength(0); k++)
               {
                   if (array[i, k] == 0)
                   {
                       array[i, k] = array[i, k - 1];
                       array[i, k - 1] = 0;
                       cells[i, k - 1].Location = field[i, k].Location;
                       cells[i, k] = cells[i, k - 1];
                       cells[i, k - 1] = null;
                   }
                   else
                   {
                       if (array[i, k] == array[i, k - 1] && isJoined == false)
                       {
                           array[i, k] += array[i, k - 1];
                           array[i, k - 1] = 0;
                           cells[i, k - 1].Location = cells[i, k].Location;

                           //создание нового элемента
                           PictureBox pictureBox = new PictureBox();
                           pictureBox.Location = cells[i, k].Location;
                           pictureBox.Size = cells[i, k].Size;
                           DrawNumeral(pictureBox, array[i, k]);
                           Controls.Add(pictureBox);
                           pictureBox.BringToFront();
                           this.Controls.Remove(cells[i, k]);
                           this.Controls.Remove(cells[i, k - 1]);
                           cells[i, k] = null;
                           cells[i, k - 1] = null;
                           cells[i, k] = pictureBox;
                           isJoined = true;
                       }
                       else
                       {
                           isJoined = false;
                       }
                   }
               }
           }
       }
   }
}
private void button4_Click(object sender, EventArgs e)
{
   bool isJoined;
   for (int j = 0; j < array.GetLength(0); j++)
   {
       isJoined = false;
       for (int i = 2; i >= 0; i--)
       {
           if (array[i, j] != 0)
           {
               for (int k = i + 1; k < array.GetLength(0); k++)
               {
                   if (array[k, j] == 0)
                   {
                       array[k, j] = array[k - 1, j];
                       array[k - 1, j] = 0;
                       cells[k - 1, j].Location = field[k, j].Location;
                       cells[k, j] = cells[k - 1, j];
                       cells[k - 1, j] = null;
                   }
                   else
                   {
                       if (array[k, j] == array[k - 1, j] && isJoined == false)
                       {
                           array[k, j] += array[k - 1, j];
                           array[k - 1, j] = 0;
                           cells[k - 1, j].Location = cells[k, j].Location;

                           //создание нового элемента
                           PictureBox pictureBox = new PictureBox();
                           pictureBox.Location = cells[k, j].Location;
                           pictureBox.Size = cells[k, j].Size;
                           DrawNumeral(pictureBox, array[k, j]);
                           Controls.Add(pictureBox);
                           pictureBox.BringToFront();
                           this.Controls.Remove(cells[k, j]);
                           this.Controls.Remove(cells[k - 1, j]);
                           cells[k, j] = null;
                           cells[k - 1, j] = null;
                           cells[k, j] = pictureBox;
                           isJoined = true;
                       }
                       else
                       {
                           isJoined = false;
                       }
                   }
               }
           }
       }
   }
}*/
    }
}
