using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// This is the code for your desktop app.
// Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

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

        public Bitmap TgtBitmap { get; set; }

        Point lastPoint = Point.Empty;


        public pixelArtProgram()
        {
            InitializeComponent();
            DoubleBuffered = true;
            GridColor = Color.DimGray;
            DrawColor = Color.Red;

            pictureBox1.Image = new Bitmap(1000, 1000);
            ColorDialog colorDlg = new ColorDialog();

            TgtBitmap = (Bitmap)pictureBox1.Image;

            MouseClick += pictureBox1_MouseClick;
            MouseMove += pictureBox1_MouseMove;
            Paint += pictureBox1_Paint;

            spriteEdit = new SpriteEdit();
            grid = new SpriteGrid();
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
            int x = (e.X -  pictureBox1.Location.X) / PixelSize;
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
            }
        }
    }
}