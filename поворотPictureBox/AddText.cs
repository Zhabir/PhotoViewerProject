using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace поворотPictureBox
{
    public partial class AddText : Form
    {
        public AddText()
        {
            InitializeComponent();
        }
        
        PictureBox pic = new PictureBox();
        int begin_text_x, begin_text_y;
        int size_text_x, size_text_y;
        int RGBflag = 0;
        bool Drow;
        bool DrowButton = false;
        bool TText = false;
        private void AddText_Load(object sender, EventArgs e)
        {
            pic.Parent = pictureBox1;
            pic.BackColor = Color.Transparent;
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Visible = false;
            this.pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex];
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Drow = true;
            if (TText)
            {
                if (e.Button == MouseButtons.Left)
                {
                    begin_text_x = e.X;
                    begin_text_y = e.Y;
                    pic.Left = e.X;
                    pic.Top = e.Y;
                    pic.Width = 0;
                    pic.Height = 0;
                    pic.Visible = true;
                    timer1.Start();
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (TText)
            {
                if (e.Button == MouseButtons.Left)
                {
                    pic.Width = e.X - begin_text_x;
                    pic.Height = e.Y - begin_text_y;
                }
            }
            if(Drow == true && DrowButton == true)
            {
                Image a = pictureBox1.Image;
                Graphics graf = Graphics.FromImage(a);
                if (RGBflag == 0 || RGBflag == 1)
                    graf.FillEllipse(Brushes.Red, e.X, e.Y, 3, 3); // толщина кисти
                else if (RGBflag == 2)
                    graf.FillEllipse(Brushes.Green, e.X, e.Y, 3, 3); // толщина кисти
                else if (RGBflag == 3)
                    graf.FillEllipse(Brushes.Blue, e.X, e.Y, 3, 3); // толщина кисти
                pictureBox1.Image = a;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TText = true;

        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image files(*.jpg) | *.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void returnToEditToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Dispose();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            RGBflag = 1;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
          
            RGBflag = 2;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            
            RGBflag = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DrowButton = true;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrowButton = false;
        }

        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

       
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(textBox3.Text=="")
            {
                MessageBox.Show("Fill fields!");
                return;
            }
            int size = ImageList.sizeException(Convert.ToInt32(textBox3.Text));
            if (TText)
            {
                pic.Width = 0;
                pic.Height = 0;
                pic.Visible = false;
                timer1.Stop();
                size_text_x = e.X - begin_text_x;
                size_text_y = e.Y - begin_text_y;

                Image a = pictureBox1.Image;
                Graphics part2 = Graphics.FromImage(a); //получаем его часть
                if (RGBflag == 1 || RGBflag == 0)
                    part2.DrawString(textBox2.Text, new System.Drawing.Font("Arial", size, FontStyle.Bold), new SolidBrush(Color.Red), new RectangleF(begin_text_x, begin_text_y, size_text_x, size_text_y), new StringFormat(StringFormatFlags.NoWrap)); // наносим на эту часть текст с параметрами
                else if (RGBflag == 2)
                    part2.DrawString(textBox2.Text, new System.Drawing.Font("Arial", size, FontStyle.Bold), new SolidBrush(Color.Green), new RectangleF(begin_text_x, begin_text_y, size_text_x, size_text_y), new StringFormat(StringFormatFlags.NoWrap)); // наносим на эту часть текст с параметрами
                else if (RGBflag == 3)
                    part2.DrawString(textBox2.Text, new System.Drawing.Font("Arial", size, FontStyle.Bold), new SolidBrush(Color.Blue), new RectangleF(begin_text_x, begin_text_y, size_text_x, size_text_y), new StringFormat(StringFormatFlags.NoWrap)); // наносим на эту часть текст с параметрами
                pictureBox1.Image = a;

                TText = false;
            }
            Drow = false;
        }
        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
