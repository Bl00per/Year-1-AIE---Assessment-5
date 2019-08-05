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
        public SpriteGrid SpriteGrid { get; private set; }
        Bitmap drawArea;

        public Point CurrentTile { get; private set; } = new Point();

        public pixelArtProgram()
        {
            InitializeComponent();

            drawArea = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            drawGrid();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void drawGrid()
        {
            pictureBox1.DrawToBitmap(drawArea, pictureBox1.Bounds);

            Graphics g;
            g = Graphics.FromImage(drawArea);

            g.Clear(Color.White);

            if (SpriteGrid == null)
                return;


            g.DrawImage(SpriteGrid.Image, 0, 0);

            Pen pen = new Pen(Brushes.Black);

            int height = pictureBox1.Height;
            int width = pictureBox1.Width;
            for (int y = 0; y < height; y += SpriteGrid.GridHeight + SpriteGrid.Spacing)
            {
                g.DrawLine(pen, 0, y, width, y);
            }

            for (int x = 0; x < width; x += SpriteGrid.GridWidth + SpriteGrid.Spacing)
            {
                g.DrawLine(pen, x, 0, x, height);
            }

            Pen highlight = new Pen(Brushes.Red);
            g.DrawRectangle(highlight, CurrentTile.X * (SpriteGrid.GridWidth + SpriteGrid.Spacing),
                CurrentTile.Y * (SpriteGrid.GridHeight + SpriteGrid.Spacing),
                SpriteGrid.GridWidth + SpriteGrid.Spacing, SpriteGrid.GridHeight + SpriteGrid.Spacing);

            g.Dispose();

            //pictureBox1.Image = drawArea;
        }


        private void spriteSheetEdit_Click(object sender, EventArgs e)
        {
            SpriteGrid spriteGrid = new SpriteGrid();
        }

        private void gridONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            drawGrid();
        }

        private void gridOFFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpriteGrid = null;
        }
    }
}