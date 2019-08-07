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

namespace PixelArtProgram_V3._0
{
    class PixelEditor :Form
    {
        public Color DrawColor { get; set; }
        public Color GridColor { get; set; }
        int pixelSize = 8;
        public int PixelSize
        {
            get { return pixelSize; }
            set
            {
                pixelSize = value;
                Invalidate();
            }
        }


        public Bitmap TgtBitmap { get; set; }
        public Point TgtMousePos { get; set; }

        Point lastPoint = Point.Empty;

        public PixelEditor()
        {
            DoubleBuffered = true;
            BackColor = Color.White;
            GridColor = Color.DimGray;
            DrawColor = Color.Red;
            PixelSize = 10;
            TgtMousePos = Point.Empty;

            if (APBox != null && APBox.Image != null)
                TgtBitmap = (Bitmap)APBox.Image;

            MouseClick += PixelEditor_MouseClick;
            MouseMove += PixelEditor_MouseMove;
            Paint += PixelEditor_Paint;
        }

        private void PixelEditor_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;

            Graphics g = e.Graphics;

            int cols = ClientSize.Width / PixelSize;
            int rows = ClientSize.Height / PixelSize;

            if (TgtMousePos.X < 0 || TgtMousePos.Y < 0) return;

            for (int x = 0; x < cols; x++)
                for (int y = 0; y < rows; y++)
                {
                    int sx = TgtMousePos.X + x;
                    int sy = TgtMousePos.Y + y;

                    if (sx > TgtBitmap.Width || sy > TgtBitmap.Height) continue;

                    Color col = TgtBitmap.GetPixel(sx, sy);

                    using (SolidBrush b = new SolidBrush(col))
                    using (Pen p = new Pen(GridColor))
                    {
                        Rectangle rect = new Rectangle(x * PixelSize, y * PixelSize,
                                                           PixelSize, PixelSize);
                        g.FillRectangle(b, rect);
                        g.DrawRectangle(p, rect);
                    }
                }
        }

        private void PixelEditor_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            int x = TgtMousePos.X + e.X / PixelSize;
            int y = TgtMousePos.Y + e.Y / PixelSize;

            if (new Point(x, y) == lastPoint) return;

            Bitmap bmp = (Bitmap)APBox.Image;
            bmp.SetPixel(x, y, DrawColor);
            APBox.Image = bmp;
            Invalidate();
            lastPoint = new Point(x, y);
        }

        private void PixelEditor_MouseClick(object sender, MouseEventArgs e)
        {
            int x = TgtMousePos.X + e.X / PixelSize;
            int y = TgtMousePos.Y + e.Y / PixelSize;
            Bitmap bmp = (Bitmap)APBox.Image;
            bmp.SetPixel(x, y, DrawColor);
            APBox.Image = bmp;
            Invalidate();
        }

        private PictureBox APBox;

        private void InitializeComponent()
        {
            this.APBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.APBox)).BeginInit();
            this.SuspendLayout();
            // 
            // APBox
            // 
            this.APBox.Location = new System.Drawing.Point(13, 13);
            this.APBox.Name = "APBox";
            this.APBox.Size = new System.Drawing.Size(259, 236);
            this.APBox.TabIndex = 0;
            this.APBox.TabStop = false;
            // 
            // PixelEditor
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.APBox);
            this.Name = "PixelEditor";
            ((System.ComponentModel.ISupportInitialize)(this.APBox)).EndInit();
            this.ResumeLayout(false);

        }
    }

}