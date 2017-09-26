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

namespace Lab1
{
	public partial class Form1 : Form
	{
		Bitmap image;
        Bitmap image2;

        int[,] Res;
        double[,] Res2;
        string path1 = @"C:\NN\binarization.txt";
        string path2 = @"C:\NN\bipolarization.txt";
        string path3 = @"C:\NN\mashtab.txt";


        public Form1()
		{
			InitializeComponent();
		}



		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
            
			OpenFileDialog open_dialog = new OpenFileDialog(); 
			open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            string filename = open_dialog.FileName;
			if (open_dialog.ShowDialog() == DialogResult.OK) 
			{
				try
				{
                    image = new Bitmap(open_dialog.FileName); 
                    this.pictureBox1.Size = image.Size; 
                    pictureBox1.Image = image;
					pictureBox1.Invalidate();
				}
				catch
				{
					DialogResult result = MessageBox.Show("Невозможно открыть выбранный файл",
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void pictureBox1_Click(object sender, EventArgs e)
		{
			
		}

		private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
		{
			
		}

		private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
		{
            // if (e.Button == MouseButtons.Left)
            
                Color pixelColor = image.GetPixel(e.X, e.Y);
                pictureBox2.BackColor = pixelColor;
                SolidBrush pixelBrush = new SolidBrush(pixelColor);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Res = new int[image.Width, image.Height];
            Res2 = new double[image.Width, image.Height];
            StreamWriter sw;
            if (radioButton1.Checked)
            {
                int average = (int)(pictureBox2.BackColor.R + pictureBox2.BackColor.G + pictureBox2.BackColor.B) / 3;
                image2 = new Bitmap(image);
                sw = new StreamWriter(path1);
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        int average2 = (int)(pixel.R + pixel.G + pixel.B) / 3;
                        if (average2 >= average - Convert.ToInt32(textBox1.Text) && average2 <= average + Convert.ToInt32(textBox1.Text))
                        {
                            image2.SetPixel(i, j, Color.Black);
                            Res[i, j] = 1;
                            sw.Write(Convert.ToString(Res[i, j]));
                        }
                        else
                        {
                            image2.SetPixel(i, j, Color.White);
                            Res[i, j] = 0;
                            sw.Write(Convert.ToString(Res[i, j]));
                        }

                    }
                    sw.WriteLine();
                }
                pictureBox3.Image = image2;
                sw.Close();
            }
            
            if (radioButton2.Checked)
            {
                int average = (int)(pictureBox2.BackColor.R + pictureBox2.BackColor.G + pictureBox2.BackColor.B) / 3;
                image2 = new Bitmap(image);
                sw = new StreamWriter(path2);
                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        int average2 = (int)(pixel.R + pixel.G + pixel.B) / 3;
                        if (average2 >= average - Convert.ToInt32(textBox1.Text) && average2 <= average + Convert.ToInt32(textBox1.Text))
                        {
                            image2.SetPixel(i, j, Color.Black);
                            Res[i, j] = 1;
                            sw.Write(Convert.ToString(Res[i, j]));
                        }
                        else
                        {
                            image2.SetPixel(i, j, Color.White);
                            Res[i, j] = -1;
                            sw.Write(Convert.ToString(Res[i, j]));
                        }
                    }
                    sw.WriteLine();
                }
                pictureBox3.Image = image2;
                sw.Close();
            }

            if (radioButton3.Checked)
            {
                int average = (int)(pictureBox2.BackColor.R + pictureBox2.BackColor.G + pictureBox2.BackColor.B) / 3;
                image2 = new Bitmap(image);
                sw = new StreamWriter(path3);
                int min = 0;
                int max = 0;
                for (int i = 0; i < image.Width; i++)
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        int average2 = (int)(pixel.R + pixel.G + pixel.B) / 3;
                        if (average2 < min)
                            min = average2;
                        if (average2 > max)
                            max = average2;
                        if (average2 >= average - Convert.ToInt32(textBox1.Text) && average2 <= average + Convert.ToInt32(textBox1.Text))
                        {
                            image2.SetPixel(i, j, Color.Black);
                            
                        }
                        else
                        {
                            image2.SetPixel(i, j, Color.White);
                            
                        }
                    }
                pictureBox3.Image = image2;
                double a = Convert.ToDouble(textBox2.Text);
                double b = Convert.ToDouble(textBox3.Text);

                for (int i = 0; i < image.Width; i++)
                {
                    for (int j = 0; j < image.Height; j++)
                    {
                        Color pixel = image.GetPixel(i, j);
                        double average2 = (double)(pixel.R + pixel.G + pixel.B) / 3;
                        if (average2 == min)
                            Res2[i, j] = a;
                        else if (average2 == max)
                            Res2[i, j] = b;
                        else
                            Res2[i, j] = (average2 / 255) * (b - a) + a;
                        sw.Write(Convert.ToString(Res2[i, j]).Replace(',','.'));
                        sw.Write(" ");
                    }
                    sw.WriteLine();
                }
                sw.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }
    }
}
