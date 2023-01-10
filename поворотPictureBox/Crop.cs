using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace поворотPictureBox
{
    public partial class Crop : Form
    {
        public Crop()
        {
            InitializeComponent();
        }
        PictureBox pic = new PictureBox();
        int begin_x, begin_y;
        bool resize = false;
        int size_crop_x, size_crop_y;
        bool cropButton = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && cropButton)
            {
                begin_x = e.X;
                begin_y = e.Y;
                pic.Left = e.X;
                pic.Top = e.Y;
                pic.Width = 0;
                pic.Height = 0;
                pic.Visible = true;
                timer1.Start();
                resize = true;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && cropButton)
            {
                pic.Width = e.X - begin_x;
                pic.Height = e.Y - begin_y;
            }
        }
        static public Image Copy(Image srcBitmap, Rectangle section)
        {
            // Вырезаем выбранный кусок картинки
            Bitmap bmp = new Bitmap(section.Width, section.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {

                g.DrawImage(srcBitmap, 0, 0, section, GraphicsUnit.Pixel);
            }
            //Возвращаем кусок картинки.
            return bmp;
        }

        private void Crop_Load(object sender, EventArgs e)
        {
            pic.Parent = pictureBox1;
            pic.BackColor = Color.Transparent;
            pic.SizeMode = PictureBoxSizeMode.AutoSize;
            pic.BorderStyle = BorderStyle.FixedSingle;
            pic.Visible = false;
            this.pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex];
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            cropButton = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cropButton = false;

            Bitmap bmp = new Bitmap(size_crop_x, size_crop_y);
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
           
        }

        public static Bitmap RotateImage(Bitmap img, int rotationAngle)
        {
            //create an empty Bitmap image
            Bitmap bmp = new Bitmap(img.Width, img.Height);
            bmp.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            //turn the Bitmap into a Graphics object
            Graphics gfx = Graphics.FromImage(bmp);

            //now we set the rotation point to the center of our image
            gfx.TranslateTransform((float)bmp.Width / 2, (float)bmp.Height / 2);

            //now rotate the image
            gfx.RotateTransform(rotationAngle);

            gfx.TranslateTransform(-(float)bmp.Width / 2, -(float)bmp.Height / 2);

            //set the InterpolationMode to HighQualityBicubic so to ensure a high
            //quality image once it is transformed to the specified size
            gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

            //now draw our new image onto the graphics object
            gfx.DrawImage(img, new Point(0, 0));

            //dispose of our Graphics object
            gfx.Dispose();

            //return the image
            return bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int actualAngle = ImageList.angleActual;
            if (textBox1.Text == "") { MessageBox.Show("Error"); return; }
            int angle = Convert.ToInt32(textBox1.Text);
            ImageList.angleActual += angle;
            angle += actualAngle;
            if (angle > 360)
            {

                angle -= 360;
            }

            pictureBox1.Image = RotateImage(ImageList.bitmaps[ImageList.actualIndex], angle);
            pictureBox1.DrawToBitmap(ImageList.bitmaps[ImageList.actualIndex], new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp.SetResolution(pictureBox1.Image.HorizontalResolution, pictureBox1.Image.VerticalResolution);
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            ImageList.bitmaps[ImageList.actualIndex] = bmp;

            this.Dispose();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(size_crop_x, size_crop_y);
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Image files(*.jpg) | *.jpg";
            if (sfd.ShowDialog() == DialogResult.OK)
                bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            pic.Width = 0;
            pic.Height = 0;
            pic.Visible = false;
            timer1.Stop();
            if (resize == true && cropButton)
            {
                if ((e.X > begin_x + 10) && (e.Y > begin_y + 10)) //Чтобы совсем уж мелочь не вырезал - и по случайным нажатиям не срабатывал! (можно убрать +10)
                {
                    Rectangle rec = new Rectangle(begin_x, begin_y, e.X - begin_x, e.Y - begin_y);
                    size_crop_x = e.X - begin_x;
                    size_crop_y = e.Y - begin_y;
                    pictureBox1.Image = Copy(pictureBox1.Image, rec);
                }
            }
            resize = false;
        }
        
    }
}
