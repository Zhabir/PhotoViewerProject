using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace поворотPictureBox
{
    public partial class Form1 : Form
    {

       bool Drow;
        int RGBflag = 0;
        bool DrowButton = false;
        public bool text_drow_flag = false;
        int indexName;
        public Form1()
        {
            InitializeComponent();
            this.buttonR.Click += new System.EventHandler(this.button_Click);
            this.buttonG.Click += new System.EventHandler(this.button_Click);
            this.buttonB.Click += new System.EventHandler(this.button_Click);
            this.buttonBr.Click += new System.EventHandler(this.button_Click);
            this.buttonCont.Click += new System.EventHandler(this.button_Click);
           
            if (File.Exists("data.json"))           //deserialization
            {
                string data = File.ReadAllText("data.json");
                Data a = JsonSerializer.Deserialize<Data>(data);
                ImageList.actualIndex = a.actualIndex;
                trackBarR.Value = a.TrackBarR;
                trackBarG.Value = a.TrackBarG;
                trackBarB.Value = a.TrackBarB;
                trackBarBright.Value = a.Bright;
                trackBarContrast.Value = a.contrast;
                RGBflag = a.rgbFlag;
                indexName = a.name_index;
                if (RGBflag == 0 || RGBflag == 1)
                {
                    radioButtonR.Checked = true;
                    radioButtonG.Checked = false;
                    radioButtonB.Checked = false;
                }
                else if (RGBflag == 2)
                {
                    radioButtonR.Checked = false;
                    radioButtonG.Checked = true;
                    radioButtonB.Checked = false;
                }
                else if (RGBflag == 3)
                {
                    radioButtonR.Checked = false;
                    radioButtonG.Checked = false;
                    radioButtonB.Checked = true;
                }

                ImageList.angles= a.angles;

                for(int i=0; i<a.bitmaps_count; i++)
                {
                    Bitmap bmp;
                    if (File.Exists($"{nameOfDirectory()}"+@"\saved bitmaps\img" + $"{indexName-1}{i}" + ".jpeg"))
                    {
                        bmp = new Bitmap($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{indexName - 1}{i}" + ".jpeg");
                        Image img = bmp;
                        ImageList.bitmaps.Add((Bitmap)img);
                    }
                }
                if(ImageList.bitmaps.Count!=0 && pictureBox1.Image!=null) pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex];
                if (ImageList.bitmaps.Count!=0)
                {
                    cropToolStripMenuItem.Enabled = true;
                    textToolStripMenuItem.Enabled = true;
                }

                
            }

        }
        string nameOfDirectory()
        {
            string name = System.AppDomain.CurrentDomain.BaseDirectory;
            string directory = "-PictureBox-withoutjsonyet";
            int indexOfDirectory = name.IndexOf(directory);
            int size = directory.Length;
            name = name.Substring(0, indexOfDirectory + size);
            return name;
        }

        void deleteExtrabitmaps()
        {
            int j = 0;
            for(int i = 0; i<indexName-1; i++)
            {
                while(true)
                {
                    if (!File.Exists($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{i}{j}" + ".jpeg")) break;
                    System.IO.File.Delete($"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{i}{j}" + ".jpeg");
                    j++;
                }
            }

            if (indexName > 9) indexName = 0;
        }

        void save_bitmaps()
        {
            for (int i = 0; i < ImageList.bitmaps.Count; i++)
            {
                Bitmap bmp = new Bitmap(480, 495);
                bmp = ImageList.bitmaps[i];
                
                string Name = $"{nameOfDirectory()}" + @"\saved bitmaps\img" + $"{indexName}{i}" + ".jpeg";
                bmp.Save(Name, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            deleteExtrabitmaps();
        }
        public void FromBitmapToScreen()
        {
            pictureBox1.Image = ImageList.bitmap;

            
            if (ImageList.angleActual > 360)
            {

                ImageList.angleActual -= 360;
            }

            pictureBox1.Image = RotateImage(ImageList.bitmaps[ImageList.actualIndex], ImageList.angleActual);
        }
        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog open_dialog = new OpenFileDialog();
            open_dialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            if (open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ImageList.nameImage = open_dialog.FileName;
                    ImageList.bitmap = new Bitmap(open_dialog.FileName);
                    //this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    
                    pictureBox1.Image = ImageList.bitmap;
                    pictureBox1.Invalidate(); 
                    //получение матрицы с пикселями
                    ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                    for (int y = 0; y < ImageList.bitmap.Height; y++)
                        for (int x = 0; x < ImageList.bitmap.Width; x++)
                            ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                    ImageList.bitmaps.Add(ImageList.bitmap);
                    ImageList.actualIndex = ImageList.bitmaps.Count - 1;
                    

                }
                catch
                {
                    ImageList.nameImage = "\0";
                    DialogResult rezult = MessageBox.Show("Невозможно открыть выбранный файл",
                    "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                cropToolStripMenuItem.Enabled = true;
                textToolStripMenuItem.Enabled = true;
                trackBarR.Value = 0;
                trackBarG.Value = 0;
                trackBarB.Value = 0;
                trackBarBright.Value = 0;
                trackBarContrast.Value = 0;
            }
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
        

        

        public static void FromOnePixelToBitmap(int x, int y, UInt32 pixel)
        {
            
            ImageList.bitmap.SetPixel(y, x, Color.FromArgb((int)pixel));
            
        }


            private void buttonRight_Click(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0 || ImageList.bitmaps.Count == 1) return;
            if (ImageList.actualIndex == ImageList.bitmaps.Count - 1) { pictureBox1.Image = ImageList.bitmaps[0]; ImageList.actualIndex = 0; }
            else { ++ImageList.actualIndex; pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex]; }

            ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
            for (int y = 0; y < ImageList.bitmap.Height; y++)
                for (int x = 0; x < ImageList.bitmap.Width; x++)
                    ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
            trackBarR.Value = 0;
            trackBarG.Value = 0;
            trackBarB.Value = 0;
            trackBarBright.Value = 0;
            trackBarContrast.Value = 0;
            FromBitmapToScreen();
        }

        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0 || ImageList.bitmaps.Count == 1) return;
            if (ImageList.actualIndex == 0) { pictureBox1.Image = ImageList.bitmaps[ImageList.bitmaps.Count - 1]; ImageList.actualIndex = ImageList.bitmaps.Count - 1; }
            else { --ImageList.actualIndex; pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex]; }

            ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
            for (int y = 0; y < ImageList.bitmap.Height; y++)
                for (int x = 0; x < ImageList.bitmap.Width; x++)
                    ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
            trackBarR.Value = 0;
            trackBarG.Value = 0;
            trackBarB.Value = 0;
            trackBarBright.Value = 0;
            trackBarContrast.Value = 0;
            FromBitmapToScreen();
        }

        

        //цветовой баланс R
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (ImageList.nameImage != "\0")
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = ColorBalance.ColorBalance_R(ImageList.pixel[i, j], trackBarR.Value, trackBarR.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        //цветовой баланс G
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (ImageList.nameImage != "\0")
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = ColorBalance.ColorBalance_G(ImageList.pixel[i, j], trackBarG.Value, trackBarG.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        //цветовой баланс B
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (ImageList.nameImage != "\0")
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = ColorBalance.ColorBalance_B(ImageList.pixel[i, j], trackBarB.Value, trackBarB.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        void save()
        {
            for (int i = 0; i < ImageList.bitmap.Height; i++)
                for (int j = 0; j < ImageList.bitmap.Width; j++)
                    ImageList.pixel[i, j] = (UInt32)(ImageList.bitmap.GetPixel(j, i).ToArgb());
        }

        //сохранение изменений яркости или контрастности
        private void button_Click(object sender, EventArgs e)
        {
            if (ImageList.nameImage != "\0")
            {
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                        ImageList.pixel[i, j] = (UInt32)(ImageList.bitmap.GetPixel(j, i).ToArgb());
                trackBarR.Value = 0;
                trackBarG.Value = 0;
                trackBarB.Value = 0;
                trackBarBright.Value = 0;
                trackBarContrast.Value = 0;
            }
        }

        //вывод изображения на экран
        

        //обновление изображения в Bitmap и pictureBox при закрытии окна
        private void Form3Closing(object sender, System.EventArgs e)
        {
            if (ImageList.nameImage != "\0")
            {
                ImageList.FromPixelToBitmap();
                FromBitmapToScreen();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void trackBar2_Scroll_1(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (ImageList.bitmaps.Count != 0)
            {
                UInt32 p;
                ImageList.bitmap = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = ColorBalance.ColorBalance_G(ImageList.pixel[i, j], trackBarG.Value, trackBarG.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void trackBar3_Scroll_1(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (ImageList.bitmaps.Count != 0)
            {
                UInt32 p;
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = ColorBalance.ColorBalance_B(ImageList.pixel[i, j], trackBarB.Value, trackBarB.Maximum);
                        Form1.FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            if (ImageList.bitmaps.Count != 0)
            {
                UInt32 p;
                ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
                for (int y = 0; y < ImageList.bitmap.Height; y++)
                    for (int x = 0; x < ImageList.bitmap.Width; x++)
                        ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = BrightnessContrast.Brightness(ImageList.pixel[i, j], trackBarBright.Value, trackBarBright.Maximum);
                        FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (ImageList.bitmaps.Count == 0) return;
            UInt32 p;
            ImageList.pixel = new UInt32[ImageList.bitmap.Height, ImageList.bitmap.Width];
            for (int y = 0; y < ImageList.bitmap.Height; y++)
                for (int x = 0; x < ImageList.bitmap.Width; x++)
                    ImageList.pixel[y, x] = (UInt32)(ImageList.bitmap.GetPixel(x, y).ToArgb());
            if (ImageList.bitmaps.Count!=0)
            {
                for (int i = 0; i < ImageList.bitmap.Height; i++)
                    for (int j = 0; j < ImageList.bitmap.Width; j++)
                    {
                        p = BrightnessContrast.Contrast(ImageList.pixel[i, j], trackBarContrast.Value, trackBarContrast.Maximum);
                        FromOnePixelToBitmap(i, j, p);
                    }

                FromBitmapToScreen();
            }
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Drow = true;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (Drow == true && DrowButton==true)
            {
                Image a = pictureBox1.Image;
                Graphics graf = Graphics.FromImage(a);
                if (RGBflag==0 || RGBflag==1)
                    graf.FillEllipse(Brushes.Red, e.X, e.Y, 3, 3); // толщина кисти
                else if(RGBflag==2)
                    graf.FillEllipse(Brushes.Green, e.X+10, e.Y + 10, 3, 3); // толщина кисти
                else if(RGBflag == 3)
                    graf.FillEllipse(Brushes.Blue, e.X, e.Y, 3, 3); // толщина кисти
                pictureBox1.Image = a;
                
            }
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Drow = false;
        }
        private void buttonR_Click(object sender, EventArgs e)
        {

        }

        public void text_add()
        {
            
        }
        public void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void createCollageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Collages a = new Collages();
            a.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
  
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void addTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddText a = new AddText();
            a.ShowDialog();
            
            pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex];
        }

        private void cropImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Crop b = new Crop();
            b.ShowDialog();

            pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex];
            for (int i = 0; i < ImageList.bitmap.Height; i++)
                for (int j = 0; j < ImageList.bitmap.Width; j++)
                    ImageList.pixel[i, j] = (UInt32)(ImageList.bitmap.GetPixel(j, i).ToArgb());
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
           
            
           
        }

        private void buttonG_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DrowButton = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DrowButton = false;
            FromBitmapToScreen();
        }

        private void radioButtonR_CheckedChanged(object sender, EventArgs e)
        {
            RGBflag = 1;
        }

        private void radioButtonG_CheckedChanged(object sender, EventArgs e)
        {
            RGBflag = 2;
        }

        private void radioButtonB_CheckedChanged(object sender, EventArgs e)
        {
            RGBflag = 3;
        }

        private void saveDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data b = new Data
            {
                //bitmaps = ImageList.bitmaps,
                actualIndex = ImageList.actualIndex,
                TrackBarR = trackBarR.Value,
                TrackBarG = trackBarG.Value,
                TrackBarB = trackBarB.Value,
                Bright = trackBarBright.Value,
                contrast = trackBarContrast.Value,
                rgbFlag = RGBflag,
                bitmaps_count = ImageList.bitmaps.Count,
                name_index = indexName + 1,
                angles = ImageList.angles
            };
            string personJson = JsonSerializer.Serialize(b, typeof(Data));
            StreamWriter file = File.CreateText("data.json");
            file.WriteLine(personJson);
            file.Close();

            save_bitmaps();
            MessageBox.Show("Data is saved", "Success");
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ImageList.bitmaps.Count == 0) return;
            Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            bmp.SetResolution(pictureBox1.Image.HorizontalResolution, pictureBox1.Image.VerticalResolution);
            pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height ));
                bmp.Save("c:\\Test\\img.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (ImageList.bitmaps.Count == 0) return;//MessageBox.Show($"before {ImageList.actualIndex}");
            if (ImageList.actualIndex == ImageList.bitmaps.Count - 1)
            {
                //MessageBox.Show($"in first");
                ImageList.actualIndex = 0;
                pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex];
                ImageList.bitmaps.RemoveAt(ImageList.bitmaps.Count - 1);
            }
            else
            {
                //MessageBox.Show($"in second");
                pictureBox1.Image = ImageList.bitmaps[ImageList.actualIndex+1];
                ImageList.bitmaps.RemoveAt(ImageList.actualIndex);
            }

            if (ImageList.bitmaps.Count == 0)
            {
                ImageList.actualIndex = -1;
                pictureBox1.Image = null;
            }
            //MessageBox.Show($"before {ImageList.actualIndex}");

        }

       
    }
}
