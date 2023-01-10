using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace поворотPictureBox
{
    public partial class Collages : Form
    {
        protected string ImageFileName;
        protected int i = 0;
        protected int j = 0;
        protected int z = 0;
        protected string SaveFileName;
        public IContainer container1;
        public Collages()
        {
            InitializeComponent();
        }

        private void addPicturesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void saveFIleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(i == 0)
            {
                Bitmap bmp = new Bitmap(480, 495);
                pictureBox9.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox9.Width, pictureBox9.Height));
                pictureBox10.DrawToBitmap(bmp, new Rectangle(0, pictureBox9.Height + 6, pictureBox10.Width, pictureBox10.Height));
                pictureBox11.DrawToBitmap(bmp, new Rectangle(pictureBox11.Width + 6, 0, pictureBox11.Width, pictureBox11.Height));
                pictureBox12.DrawToBitmap(bmp, new Rectangle(pictureBox12.Width + 6, pictureBox12.Height + 6, pictureBox12.Width, pictureBox12.Height));
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image files(*.jpg) | *.jpg";
                if (sfd.ShowDialog() == DialogResult.OK)
                    bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
           else if(i == 1)
            {
                Bitmap bmp = new Bitmap(580, 370);
                pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
                pictureBox5.DrawToBitmap(bmp, new Rectangle(pictureBox1.Width + 6, 0, pictureBox5.Width, pictureBox5.Height));
                pictureBox2.DrawToBitmap(bmp, new Rectangle(pictureBox1.Width + 6 + pictureBox5.Width + 6, 0, pictureBox2.Width, pictureBox2.Height));
                pictureBox3.DrawToBitmap(bmp, new Rectangle(0, pictureBox1.Height + 6, pictureBox3.Width, pictureBox3.Height));
                pictureBox4.DrawToBitmap(bmp, new Rectangle(pictureBox3.Width + 6, pictureBox1.Height + 6, pictureBox3.Width, pictureBox3.Height));
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image files(*.jpg) | *.jpg";
                if (sfd.ShowDialog() == DialogResult.OK)
                    bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
           else if(i == 2)
            {
                Bitmap bmp = new Bitmap(500, 495);
                pictureBox6.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox6.Width, pictureBox6.Height));
                pictureBox7.DrawToBitmap(bmp, new Rectangle(pictureBox6.Width + 6, 0, pictureBox7.Width, pictureBox7.Height));
                pictureBox8.DrawToBitmap(bmp, new Rectangle(80, pictureBox6.Height + 6, pictureBox8.Width, pictureBox8.Height));
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Image files(*.jpg) | *.jpg";
                if (sfd.ShowDialog() == DialogResult.OK)
                    bmp.Save(sfd.FileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            z++;
            if (i == 0)
            {
                pictureBox9.BorderStyle = BorderStyle.None;
                pictureBox9.Image = null;
                pictureBox10.BorderStyle = BorderStyle.None;
                pictureBox10.Image = null;
                pictureBox11.BorderStyle = BorderStyle.None;
                pictureBox11.Image = null;
                pictureBox12.BorderStyle = BorderStyle.None;
                pictureBox12.Image = null;


                pictureBox1.BorderStyle = BorderStyle.FixedSingle;
                pictureBox1.BringToFront();

                pictureBox2.BorderStyle = BorderStyle.FixedSingle;
                pictureBox2.BringToFront();

                pictureBox3.BorderStyle = BorderStyle.FixedSingle;
                pictureBox3.BringToFront();

                pictureBox4.BorderStyle = BorderStyle.FixedSingle;
                pictureBox4.BringToFront();

                pictureBox5.BorderStyle = BorderStyle.FixedSingle;
                pictureBox5.BringToFront();
                i++;

            }
            else if (i == 1)
            {

                pictureBox1.BorderStyle = BorderStyle.None;
                pictureBox1.Image = null;
                pictureBox2.BorderStyle = BorderStyle.None;
                pictureBox2.Image = null;
                pictureBox3.BorderStyle = BorderStyle.None;
                pictureBox3.Image = null;
                pictureBox4.BorderStyle = BorderStyle.None;
                pictureBox4.Image = null;
                pictureBox5.BorderStyle = BorderStyle.None;
                pictureBox5.Image = null;

                pictureBox6.BorderStyle = BorderStyle.FixedSingle;
                pictureBox6.BringToFront();

                pictureBox7.BorderStyle = BorderStyle.FixedSingle;
                pictureBox7.BringToFront();

                pictureBox8.BorderStyle = BorderStyle.FixedSingle;
                pictureBox8.BringToFront();

                i++;

            }
            else if (i == 2)
            {

                pictureBox6.BorderStyle = BorderStyle.None;
                pictureBox6.Image = null;
                pictureBox7.BorderStyle = BorderStyle.None;
                pictureBox7.Image = null;
                pictureBox8.BorderStyle = BorderStyle.None;
                pictureBox8.Image = null;

                pictureBox9.BorderStyle = BorderStyle.FixedSingle;
                pictureBox9.BringToFront();

                pictureBox10.BorderStyle = BorderStyle.FixedSingle;
                pictureBox10.BringToFront();

                pictureBox11.BorderStyle = BorderStyle.FixedSingle;
                pictureBox11.BringToFront();

                pictureBox12.BorderStyle = BorderStyle.FixedSingle;
                pictureBox12.BringToFront();

                i = 0;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(i == 1 && z != 0) 
            {
                pictureBox1.Image = null;
                pictureBox2.Image = null;
                pictureBox3.Image = null;
                pictureBox4.Image = null;
                pictureBox5.Image = null;

                for (int j = 0; j < 5; j++)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "JPEG|*.jpg";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ImageFileName = openFileDialog.FileName;
                        imageList1.Images.Add(Image.FromFile(ImageFileName));
                    }
                }

                pictureBox1.Image = imageList1.Images[0];
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox2.Image = imageList1.Images[1];
                pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox3.Image = imageList1.Images[2];
                pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox4.Image = imageList1.Images[3];
                pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox5.Image = imageList1.Images[4];
                pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            }
           if(i == 2 && z != 0)
            {
                pictureBox6.Image = null;
                pictureBox7.Image = null;
                pictureBox8.Image = null;

                for (int j = 0; j < 3; j++)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "JPEG|*.jpg";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ImageFileName = openFileDialog.FileName;
                        imageList2.Images.Add(Image.FromFile(ImageFileName));
                    }
                }

                pictureBox6.Image = imageList2.Images[0];
                pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox7.Image = imageList2.Images[1];
                pictureBox7.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox8.Image = imageList2.Images[2];
                pictureBox8.SizeMode = PictureBoxSizeMode.StretchImage;
            }
           if(i == 0 && z != 0)
            {
                pictureBox9.Image = null;
                pictureBox10.Image = null;
                pictureBox11.Image = null;
                pictureBox12.Image = null;

                for (int j = 0; j < 4; j++)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    openFileDialog.Filter = "JPEG|*.jpg";
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ImageFileName = openFileDialog.FileName;
                        imageList3.Images.Add(Image.FromFile(ImageFileName));
                    }
                }

                pictureBox9.Image = imageList3.Images[0];
                pictureBox9.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox10.Image = imageList3.Images[1];
                pictureBox10.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox11.Image = imageList3.Images[2];
                pictureBox11.SizeMode = PictureBoxSizeMode.StretchImage;

                pictureBox12.Image = imageList3.Images[3];
                pictureBox12.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void splitContainer1_Panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
