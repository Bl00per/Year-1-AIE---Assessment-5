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


namespace PixelArtProgram_V2._0
{
    public partial class pixelArtProgram : Form
    {
        public SpriteGrid grid { get; private set; }
        SpriteEdit spriteEdit;
        bool isDrawingGrid = true;

        public Color DrawColor { get; set; } = Color.Red;
        public Color GridColor { get; set; }
        int PixelSize = 10;
        Color tempColor;

        public Bitmap TgtBitmap { get; set; }

        Point lastPoint = Point.Empty;

        SaveFileDialog dialog = new SaveFileDialog();

        //public System.Drawing.Size Size { get; set; }
        //pixelArtProgram Form1 = new pixelArtProgram();

        bool hasUserSaved;

        public pixelArtProgram()
        {
            InitializeComponent();

            spriteEdit = new SpriteEdit();
            grid = new SpriteGrid();

            DoubleBuffered = true;
            GridColor = Color.DimGray;
            DrawColor = Color.Red;
            tempColor = DrawColor;

            pictureBox1.Width = (grid.NumOfCellsX * PixelSize);
            pictureBox1.Height = (grid.NumOfCellsY * PixelSize);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            ColorDialog colorDlg = new ColorDialog();

            TgtBitmap = (Bitmap)pictureBox1.Image;

            hasUserSaved = false;

            dialog.FileName = "Untitled.png";

            MouseClick += pictureBox1_MouseClick;
            MouseMove += pictureBox1_MouseMove;
            Paint += pictureBox1_Paint;

        }

        private void SaveBmpAsPNG()
        {
            dialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg;*.jpeg;*.jpe;*.jfif)|*.jpeg|GIF (*.gif)|*.gif|All files(*.*)|*.*";
            dialog.FilterIndex = 1;

            if (hasUserSaved == false)
            {
                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        int width = grid.NumOfCellsX;
                        int height = grid.NumOfCellsY;
                        isDrawingGrid = false;
                        hasUserSaved = true;
                        Invalidate();

                        // Fancy math that basically devided the thing up how much you want it enlarged by
                        // In this case its getting increased by 10 times 16 px -> 160 px
                        int EnlargeFactor = 1;
                        Bitmap bmp = new Bitmap(width * EnlargeFactor, height * EnlargeFactor);
                        for (int x = 0; x < width * EnlargeFactor; x++)
                        {
                            for (int y = 0; y < height * EnlargeFactor; y++)
                            {
                                bmp.SetPixel(x, y, TgtBitmap.GetPixel(x / EnlargeFactor, y / EnlargeFactor));
                            }
                        }

                        //bmp is our new image, specifically for the sake of saving to disk.

                        bmp.Save(dialog.FileName, ImageFormat.Png);

                        isDrawingGrid = true;
                        Invalidate();
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There was a problem saving the file." + " Check the file permissions.");
                }
            }
            else
            {
                int width = grid.NumOfCellsX;
                int height = grid.NumOfCellsY;

                int EnlargeFactor = 10;
                Bitmap bmp = new Bitmap(width * EnlargeFactor, height * EnlargeFactor);
                for (int x = 0; x < width * EnlargeFactor; x++)
                {
                    for (int y = 0; y < height * EnlargeFactor; y++)
                    {
                        bmp.SetPixel(x, y, TgtBitmap.GetPixel(x / EnlargeFactor, y / EnlargeFactor));
                    }
                }

                bmp.Save(dialog.FileName, ImageFormat.Png);

                isDrawingGrid = true;
                Invalidate();
            }
        }

        //public int PixelSize
        //{
        //    get { return pixelSize; }
        //    set
        //    {
        //        pixelSize = value;
        //        Invalidate();
        //    }
        //}

        private void DrawCell(Pen pen, Rectangle rectangle, Graphics g)
        {
            g.DrawRectangle(pen, rectangle);
        }

        private void spriteSheetEdit_Click(object sender, EventArgs e)
        {
            spriteEdit.Text = "Canvas Size";
            spriteEdit.BackupTempValues();
            spriteEdit.Show();
        }

        private void gridONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDrawingGrid = true;
            pictureBox1.Invalidate();
        }

        private void gridOFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDrawingGrid = false;
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;

            Graphics g = e.Graphics;

            int cols = grid.NumOfCellsX;
            int rows = grid.NumOfCellsY;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    if (x > pictureBox1.Image.Width || y > pictureBox1.Image.Height) continue;

                    Color col = TgtBitmap.GetPixel(x, y);

                    using (SolidBrush b = new SolidBrush(col))
                    using (Pen p = new Pen(GridColor))
                    {
                        Rectangle rect = new Rectangle((x * PixelSize) + toolStrip.Width, (y * PixelSize) + menuStrip.Height, PixelSize, PixelSize);
                        g.FillRectangle(b, rect);
                        if (isDrawingGrid)
                        {
                            DrawCell(p, rect, g);
                        }
                    }
                }
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int x = (e.X - pictureBox1.Location.X) / PixelSize;
            int y = (e.Y - pictureBox1.Location.Y) / PixelSize;

            if (new Point(x, y) == lastPoint) return;

            Bitmap bmp = (Bitmap)pictureBox1.Image;
            if (!(x >= pictureBox1.Width / PixelSize || x < 0 || y >= pictureBox1.Height / PixelSize || y < 0))
            {
                bmp.SetPixel(x, y, DrawColor);
                pictureBox1.Image = bmp;
                Invalidate();
                lastPoint = new Point(x, y);
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (e.X - pictureBox1.Location.X) / PixelSize;
            int y = (e.Y - pictureBox1.Location.Y) / PixelSize;

            Bitmap bmp = (Bitmap)pictureBox1.Image;
            bmp.SetPixel(x, y, DrawColor);
            pictureBox1.Image = bmp;
            Invalidate();
        }

        private void pixelArtProgram_Load(object sender, EventArgs e)
        {

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                DrawColor = colorDialog1.Color;
                tempColor = DrawColor;
            }
        }

        private void eraserToolStripButton_Click(object sender, EventArgs e)
        {
            tempColor = DrawColor;
            DrawColor = Color.White;
        }

        private void drawToolStripButton_Click(object sender, EventArgs e)
        {
            DrawColor = tempColor;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void newSprite_Click(object sender, EventArgs e)
        {

            int cols = grid.NumOfCellsX;
            int rows = grid.NumOfCellsY;

            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows; y++)
                {
                    Bitmap bmp = (Bitmap)pictureBox1.Image;
                    bmp.SetPixel(x, y, Color.White);
                    pictureBox1.Image = bmp;
                    Invalidate();
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBmpAsPNG();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg;*.jpeg;*.jpe;*.jfif)|*.jpeg|GIF (*.gif)|*.gif|All files(*.*)|*.*";
            dialog.FilterIndex = 1;



            if (dialog.ShowDialog() == DialogResult.OK)
            {


                Bitmap bmp = new Bitmap(dialog.FileName);
                Bitmap resized = new Bitmap(bmp, new Size(bmp.Width / 15, bmp.Height / 15));

                for (int i = bmp.Width / PixelSize; i > grid.NumOfCellsX; i--)
                    grid.NumOfCellsX++;

                for (int i = bmp.Height / PixelSize; i > grid.NumOfCellsY; i--)
                    grid.NumOfCellsY++;

                int width = bmp.Width + 34;
                int height = bmp.Height + 24;

                pictureBox1.Width = (grid.NumOfCellsX * PixelSize);
                pictureBox1.Height = (grid.NumOfCellsY * PixelSize);

                //pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

                this.Size = new Size(width, height);

                TgtBitmap = resized;
                pictureBox1.Image = resized;
                Invalidate();
            }
        }
    }
}