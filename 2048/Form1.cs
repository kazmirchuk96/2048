using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace _2048
{
    public partial class Form1 : Form
    {
        private int[,] array = new int[4, 4];//массив с цифрами, дубляж игрового поля
        Random rand = new Random();
        PictureBox[,] field;//серые ячейки
        PictureBox[,] cells = new PictureBox[4, 4];//ячейки с цифрами
        Timer timer = new Timer();
        bool flagRandPict = false;
        bool isJoined;
        private int score = 0;
        bool flag = true;
        public Form1()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(251, 246, 240);
            pictureBack.BackColor = Color.FromArgb(189, 173, 160);
            field = new PictureBox[4, 4] { { pb00, pb01, pb02, pb03 }, { pb10, pb11, pb12, pb13 }, { pb20, pb21, pb22, pb23 }, { pb30, pb31, pb32, pb33 } };
            foreach (PictureBox pb in field)
            {
                pb.BackColor = Color.FromArgb(214, 205, 196);
            }
            for (int k = 0; k < 2; k++)
            {
                GenerationRandomCell();
            }
            if (File.Exists(Application.StartupPath + "\\bestscore.txt"))//выводим лучший счет
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\bestscore.txt");
                labelBestScore.Text = sr.ReadLine();
                sr.Close();
            }
            else
            {
                labelBestScore.Text = "0";
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
                                    else if (array[k, j] == array[k - 1, j])
                                    {
                                        if (isJoined == false)
                                        {
                                            array[k, j] += array[k - 1, j];
                                            score += array[k, j];
                                            array[k - 1, j] = 0;
                                            cells[k - 1, j].Location = cells[k, j].Location;

                                            //создание нового элемента
                                            PictureBox pictureBox = new PictureBox();
                                            pictureBox.Location = cells[k, j].Location;
                                            pictureBox.Size = cells[k, j].Size;
                                            DrawNumeral(pictureBox, array[k, j]);
                                            Controls.Add(pictureBox);
                                            pictureBox.BringToFront();
                                            Controls.Remove(cells[k, j]);
                                            Controls.Remove(cells[k - 1, j]);
                                            cells[k, j] = null;
                                            cells[k - 1, j] = null;
                                            cells[k, j] = pictureBox;
                                            isJoined = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isJoined = false;
                                        break;
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
                                    else if (array[i, k] == array[i, k + 1])
                                    {
                                        if (isJoined == false)
                                        {
                                            array[i, k] += array[i, k + 1];
                                            score += array[i, k];
                                            array[i, k + 1] = 0;
                                            cells[i, k + 1].Location = cells[i, k].Location;

                                            //создание нового элемента
                                            PictureBox pictureBox = new PictureBox();
                                            pictureBox.Location = cells[i, k].Location;
                                            pictureBox.Size = cells[i, k].Size;
                                            DrawNumeral(pictureBox, array[i, k]);
                                            Controls.Add(pictureBox);
                                            pictureBox.BringToFront();
                                            Controls.Remove(cells[i, k]);
                                            Controls.Remove(cells[i, k + 1]);
                                            cells[i, k] = null;
                                            cells[i, k + 1] = null;
                                            cells[i, k] = pictureBox;
                                            isJoined = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isJoined = false;
                                        break;
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
                                    else if (array[i, k] == array[i, k - 1])
                                    {
                                        if (isJoined == false)
                                        {
                                            array[i, k] += array[i, k - 1];
                                            score += array[i, k];
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
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isJoined = false;
                                        break;
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
                                    else if (array[k, j] == array[k + 1, j])
                                    {
                                        if (isJoined == false)
                                        {
                                            array[k, j] += array[k + 1, j];
                                            score += array[k, j];
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
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isJoined = false;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    GenerationRandomCell();
                    break;
            }
            labelScore.Text = score.ToString();
            BestScore(score);
        }
        private void DrawNumeral(PictureBox pb, int text)
        {
            Brush brush = Brushes.Black;
            switch (text.ToString())
            {
                case "2":
                    pb.BackColor = (flagRandPict) ? Color.White : Color.FromArgb(238, 228, 218);
                    brush = Brushes.Black;
                    break;
                case "4":
                    pb.BackColor = (flagRandPict) ? Color.White : Color.FromArgb(236, 224, 200);
                    brush = Brushes.Black;
                    break;
                case "8":
                    pb.BackColor = (flagRandPict) ? Color.White : Color.FromArgb(245, 176, 117);
                    brush = Brushes.Black;
                    break;
                case "16":
                    pb.BackColor = (flagRandPict) ? Color.White : Color.Pink;
                    brush = Brushes.Black;
                    break;
                case "32":
                    pb.BackColor = (flagRandPict) ? Color.White : Color.FromArgb(245, 124, 95);
                    brush = Brushes.Black;
                    break;
                case "64":
                    pb.BackColor = (flagRandPict) ? Color.White : Color.Green;
                    brush = Brushes.Black;
                    break;
                default:
                    pb.BackColor = Color.White;
                    break;
            }
            pb.Paint += new PaintEventHandler((sender, e) =>
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                Font font = new Font("Arial", 26, FontStyle.Bold);
                SizeF textSize = e.Graphics.MeasureString(text.ToString(), font);
                PointF locationToDraw = new PointF();
                locationToDraw.X = (pb.Width / 2) - (textSize.Width / 2);
                locationToDraw.Y = (pb.Height / 2) - (textSize.Height / 2);
                e.Graphics.DrawString(text.ToString(), font, brush, locationToDraw);
            });

            if (flagRandPict)
            {
                timer.Tick += (sender, args) => TimerEventProcessor(sender, args, timer, pb, text);
                timer.Interval = 600;
                timer.Start();
                flagRandPict = false;
            }
        }
        private static void TimerEventProcessor(Object myObject,
                                           EventArgs myEventArgs, Timer timer, PictureBox pb, int text)
        {
            switch (text.ToString())
            {
                case "2":
                    pb.BackColor = Color.FromArgb(238, 228, 218);
                 
                    break;
                case "4":
                    pb.BackColor = Color.FromArgb(236, 224, 200);
                   
                    break;
                case "8":
                    pb.BackColor = Color.FromArgb(245, 176, 117);
                  
                    break;
                case "16":
                    pb.BackColor = Color.Pink;
                 
                    break;
                case "32":
                    pb.BackColor = Color.FromArgb(245, 124, 95);
                    break;
                case "64":
                    pb.BackColor = Color.Green;
                
                    break;
                default:
                    pb.BackColor = Color.White;
                    break;
            }
            timer.Stop();
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
                    flagRandPict = true;
                    DrawNumeral(pictureBox, array[i,j]);
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

           
            //симулирование игровой ситуации
            /*if (flag)
            {
                int i = 0;
                //0,0
                array[0, 0] = 2;
                PictureBox pictureBox = new PictureBox();
                pictureBox.Location = field[i, 0].Location;
                pictureBox.Size = field[i, 0].Size;
                flagRandPict = true;
                DrawNumeral(pictureBox, array[i, 0]);
                Controls.Add(pictureBox);
                pictureBox.BringToFront();
                cells[0, 0] = pictureBox;


                //0,1
                array[0, 1] = 2;
                pictureBox = new PictureBox();
                pictureBox.Location = field[i, 1].Location;
                pictureBox.Size = field[i, 1].Size;
                flagRandPict = true;
                DrawNumeral(pictureBox, array[i, 1]);
                Controls.Add(pictureBox);
                pictureBox.BringToFront();
                cells[0, 1] = pictureBox;

                //0,2
                array[0, 2] = 8;
                pictureBox = new PictureBox();
                pictureBox.Location = field[i, 2].Location;
                pictureBox.Size = field[i, 2].Size;
                flagRandPict = true;
                DrawNumeral(pictureBox, array[i, 2]);
                Controls.Add(pictureBox);
                pictureBox.BringToFront();
                cells[0, 2] = pictureBox;

                //0,3
                array[0, 3] = 8;
                pictureBox = new PictureBox();
                pictureBox.Location = field[i, 3].Location;
                pictureBox.Size = field[i, 3].Size;
                flagRandPict = true;
                DrawNumeral(pictureBox, array[i, 3]);
                Controls.Add(pictureBox);
                pictureBox.BringToFront();
                cells[i, 3] = pictureBox;
                flag = false;
            }*/
        }

        private void BestScore(int score)
        {
            int bestScore = 0;
            if (File.Exists(Application.StartupPath + "\\bestscore.txt"))
            {
                StreamReader sr = new StreamReader(Application.StartupPath + "\\bestscore.txt");
                bestScore = int.Parse(sr.ReadLine());
                sr.Close();
            }

            if (score > bestScore)
            {
                StreamWriter sw = new StreamWriter(Application.StartupPath + "\\bestscore.txt", false, System.Text.Encoding.Default);
                sw.WriteLine(score.ToString());
                sw.Close();
                labelBestScore.Text = score.ToString();
            }
        }
    }
}
