using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Loading : Form
    {
        private int counter { get; set; }
        private int counter2 { get; set; }
        private int size { get; set; }
        private int size2 { get; set; }
        private int interval { get; set; }
        public Loading()
        {
            InitializeComponent();
            counter = 0;
            size = 0;
            size2 = pictureBox4.Width - 1;
            counter2 = 0;
            interval = 0;
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            timer1.Interval = 1;
            timer1.Start();

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (counter < 255)
            {
                Bitmap pic = new Bitmap(Properties.Resources.Loading, pictureBox1.Size);
                for (int w = 0; w < pictureBox1.Width; w++)
                {
                    for (int h = 0; h < pictureBox1.Height; h++)
                    {
                        Color c = pic.GetPixel(w, h);
                        Color newC = Color.FromArgb(counter, c);
                        pic.SetPixel(w, h, newC);
                    }
                }
                pictureBox1.Image = pic;
                pictureBox2.BackColor = Color.FromArgb(counter,78, 184, 206);

                counter += 10;
            }
            else if(size < 470)
            {
                pictureBox3.Width = size;
                size++;
            }
            else if (size2 >= 0)
            {
                pictureBox4.Width = size2;
                pictureBox4.Left++;
                if(pictureBox5.Width>=0)
                    pictureBox5.Width--;
                size2--;
            }
            else if (interval < 600)
            {
                if (interval < 20)
                {
                    pictureBox1.Width++;
                    pictureBox1.Left--;
                    if (interval % 2 == 0)
                    {
                        pictureBox1.Top++;
                        pictureBox1.Height++;
                    }
                }
                else
                {
                    if (pictureBox1.Width >= 100 || pictureBox1.Height >= 100)
                    {
                        pictureBox1.Width-=2;
                        pictureBox1.Left+=2;
                        pictureBox1.Top--;
                        pictureBox1.Height--;
                        Bitmap pic = new Bitmap(Properties.Resources.Loading, pictureBox1.Size);
                        pictureBox1.Image = pic;
                    }
                    if (Left.Left <= 540 && Right.Left >= 648)
                    {
                        Left.Left += 2;
                        Right.Left -= 2;
                    }
                    else
                    {
                        if (interval % 2 == 0)
                        {
                            Down.Top++;
                            Up.Top--;
                        }
                    }

                    if (Down.Top > 354)
                        Down.Hide();
                    if (Up.Top < 103)
                        Up.Hide();

                    if(!Down.Visible && !Up.Visible)
                        if (label3.Top <= 280)
                            if (interval % 2 == 0)
                                label3.Top++;
                }
                interval++;
            }
            else
            {
                timer1.Stop();
                Windows windows = new Windows();
                windows.StartPosition = this.StartPosition;
                windows.FormClosed += Windows_Closed;
                this.Hide();
                windows.Show();
                
            }

            
        }

        private void Windows_Closed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void Loading_Load(object sender, EventArgs e)
        {

        }
    }
}
