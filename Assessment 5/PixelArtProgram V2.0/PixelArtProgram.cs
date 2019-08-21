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
using System.IO;


namespace PixelArtProgram_V2._0
{
    public partial class pixelArtProgram : Form
    {
        public SpriteGrid grid { get; private set; }
        public Color DrawColor { get; set; } = Color.Red;
        public Color GridColor { get; set; }
        public Bitmap TgtBitmap { get; set; }

        public int cellSize
        {
            get { return grid.CellSize; }
            set
            {
                cellSize = grid.CellSize;
                Invalidate();
            }
        }

        SaveFileDialog dialog = new SaveFileDialog();

        SpriteEdit spriteEdit;
        Color tempColor;
        Point lastPoint = Point.Empty;

        bool isDrawingGrid = true;
        bool hasUserSaved;
        bool userErasing;
        public bool canvasSizeChanged;

        public pixelArtProgram()
        {
            InitializeComponent();

            ColorDialog colorDlg = new ColorDialog();
            spriteEdit = new SpriteEdit();
            grid = new SpriteGrid();

            DoubleBuffered = true;
            GridColor = Color.DimGray;
            DrawColor = Color.Red;
            tempColor = DrawColor;

            pictureBox1.Width = (grid.NumOfCellsX * cellSize);
            pictureBox1.Height = (grid.NumOfCellsY * cellSize);

            int width = (grid.NumOfCellsX * cellSize) + 60;
            int height = (grid.NumOfCellsY * cellSize) + 75;
            this.Size = new Size(width, height);

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            TgtBitmap = (Bitmap)pictureBox1.Image;

            hasUserSaved = false;
            userErasing = false;
            canvasSizeChanged = false;

            dialog.FileName = "Untitled.png";

            MouseClick += pictureBox1_MouseClick;
            MouseMove += pictureBox1_MouseMove;
            Paint += pictureBox1_Paint;

        }

        // Enlarge factor = 1
        private void SaveBmpAsSpritePNG()
        {
            dialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg;*.jpeg;*.jpe;*.jfif)|*.jpeg|GIF (*.gif)|*.gif|All files(*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            int width = grid.NumOfCellsX;
            int height = grid.NumOfCellsY;
            int EnlargeFactor = 1;

            if (hasUserSaved == false)
            {
                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        isDrawingGrid = false;
                        hasUserSaved = true;
                        Invalidate();

                        // Fancy math that basically divided the thing up how much you want it enlarged by
                        // In this case its getting increased by 10 times 16 px -> 160 px
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
        // Enlarge factor = 10
        private void SaveBmpAsOriginalPNG()
        {
            dialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg;*.jpeg;*.jpe;*.jfif)|*.jpeg|GIF (*.gif)|*.gif|All files(*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            int width = grid.NumOfCellsX;
            int height = grid.NumOfCellsY;
            int EnlargeFactor = 10;

            if (hasUserSaved == false)
            {
                try
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        isDrawingGrid = false;
                        hasUserSaved = true;
                        Invalidate();

                        // Fancy math that basically divided the thing up how much you want it enlarged by
                        // In this case its getting increased by 10 times 16 px -> 160 px
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

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "PNG (*.png)|*.png|JPEG (*.jpeg;*.jpeg;*.jpe;*.jfif)|*.jpeg|GIF (*.gif)|*.gif|" +
                "Bitmap Files (*.bmp;*.dib)|*.bmp;*.dib|All Picture Files |*.png;*.jpeg;*.jpeg;*.jpe,*;jfif;*.gif;*.bmp|All files (*.*)|*.*";
            dialog.FilterIndex = 5;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(dialog.FileName);
                Bitmap resized;

                for (int i = bmp.Width / cellSize; i > grid.NumOfCellsX; i--)
                    grid.NumOfCellsX++;

                for (int i = bmp.Height / cellSize; i > grid.NumOfCellsY; i--)
                    grid.NumOfCellsY++;

                // It just works
                resized = new Bitmap(bmp, new Size(grid.NumOfCellsX, grid.NumOfCellsY));

                int width = (grid.NumOfCellsX * cellSize) + 60;
                int height = (grid.NumOfCellsY * cellSize) + 75;

                pictureBox1.Width = (grid.NumOfCellsX * cellSize);
                pictureBox1.Height = (grid.NumOfCellsY * cellSize);

                this.Size = new Size(width, height);

                TgtBitmap = resized;
                pictureBox1.Image = resized;
                Invalidate();
            }
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBmpAsSpritePNG();
        }
        private void saveOriginalStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveBmpAsOriginalPNG();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;

            Graphics g = e.Graphics;

            if (canvasSizeChanged)
            {
                pictureBox1.Width = (grid.NumOfCellsX * cellSize);
                pictureBox1.Height = (grid.NumOfCellsY * cellSize);

                // Need to update picturebox.image width/height
                Bitmap bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
                pictureBox1.Image = new Bitmap(bmp, pictureBox1.Width, pictureBox1.Height);
                TgtBitmap = (Bitmap)pictureBox1.Image;

                int width = (grid.NumOfCellsX * cellSize) + 60;
                int height = (grid.NumOfCellsY * cellSize) + 75;
                this.MinimumSize = new Size(width, height);
                this.Size = new Size(width, height);

                canvasSizeChanged = false;
            }

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
                        Rectangle rect = new Rectangle((x * cellSize) + toolStrip.Width, (y * cellSize) + menuStrip.Height, cellSize, cellSize);
                        g.FillRectangle(b, rect);
                        if (isDrawingGrid)
                        {
                            DrawCell(p, rect, g);
                        }
                    }
                }
            }
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int x = (e.X - pictureBox1.Location.X) / cellSize;
            int y = (e.Y - pictureBox1.Location.Y) / cellSize;

            Bitmap bmp = (Bitmap)pictureBox1.Image;

            // Check if user is erasing
            if (!userErasing)
                bmp.SetPixel(x, y, DrawColor);
            else
                bmp.SetPixel(x, y, Color.Empty);

            pictureBox1.Image = bmp;
            Invalidate();
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int x = (e.X - pictureBox1.Location.X) / cellSize;
            int y = (e.Y - pictureBox1.Location.Y) / cellSize;

            if (new Point(x, y) == lastPoint) return;

            Bitmap bmp = (Bitmap)pictureBox1.Image;
            if (!(x >= pictureBox1.Width / cellSize || x < 0 || y >= pictureBox1.Height / cellSize || y < 0))
            {
                // Check if the user is erasing
                if (!userErasing)
                    bmp.SetPixel(x, y, DrawColor);
                else
                    bmp.SetPixel(x, y, Color.Empty);

                pictureBox1.Image = bmp;
                Invalidate();
                lastPoint = new Point(x, y);
            }
        }

        private void DrawCell(Pen pen, Rectangle rectangle, Graphics g)
        {
            g.DrawRectangle(pen, rectangle);
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
        private void spriteSheetEdit_Click(object sender, EventArgs e)
        {
            spriteEdit.programReference = this;
            spriteEdit.SetTextBoxValues();
            spriteEdit.Text = "Canvas Size";
            spriteEdit.BackupTempValues();
            spriteEdit.Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void drawToolStripButton_Click(object sender, EventArgs e)
        {
            DrawColor = tempColor;
            userErasing = false;
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
            userErasing = true;
        }
        private void gridONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDrawingGrid = true;
            Invalidate();
        }
        private void gridOFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isDrawingGrid = false;
            Invalidate();
        }

        // Read color from .txt file
        private void ReadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamReader myStream = new StreamReader(dialog.FileName);

                DrawColor = Color.FromName(myStream.ReadLine());
                myStream.Close();
            }
        }

        // Print color to .txt file
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = "color.txt";
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter myStream = new StreamWriter(dialog.FileName);

                myStream.WriteLine("Red");
                myStream.Close();

            }
        }
        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = "color.txt";
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter myStream = new StreamWriter(dialog.FileName);

                myStream.WriteLine("Green");
                myStream.Close();

            }
        }
        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = "color.txt";
            dialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dialog.FilterIndex = 1;
            dialog.RestoreDirectory = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter myStream = new StreamWriter(dialog.FileName);

                myStream.WriteLine("Blue");
                myStream.Close();

            }
        }
    }
}