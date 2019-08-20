using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PixelArtProgram_V2._0
{
    public partial class SpriteEdit : Form
    {
        public pixelArtProgram programReference;

        int height;
        int width;

        int tempHeight;
        int tempWidth;

        public SpriteEdit()
        {
            InitializeComponent();
        }

        public void SetTextBoxValues()
        {
            textBoxHeight.Text = programReference.grid.NumOfCellsY.ToString();
            textBoxWidth.Text = programReference.grid.NumOfCellsX.ToString();
        }

        public void BackupTempValues()
        {
            tempHeight = programReference.grid.NumOfCellsY;
            tempWidth = programReference.grid.NumOfCellsX;
        }
        public void RestoreTempValues()
        {
            programReference.grid.NumOfCellsY = tempHeight;
            programReference.grid.NumOfCellsX = tempWidth;

            textBoxHeight.Text = programReference.grid.NumOfCellsY.ToString();
            textBoxWidth.Text = programReference.grid.NumOfCellsX.ToString();
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxWidth.Text, out width) == true)
            {
                int x = 0;
                Int32.TryParse(textBoxWidth.Text, out x);

                width = x;
            }
        }
        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxHeight.Text, out height) == true)
            {
                int y = 0;
                Int32.TryParse(textBoxHeight.Text, out y);

                height = y;
            }
        }

        // Apply values to the grid size
        public void buttonApply_Click(object sender, EventArgs e)
        {
            // Logic checks
            if (width > 160)
            {
                width = 160;
            }
            if (width <= 0)
            {
                width = tempWidth;
            }
            if (height > 95)
            {
                height = 95;
            }
            if (height <= 0)
            {
                height = tempHeight;
            }
            if (height > 0 && width > 0)
            {
                // Set the NumOfCells to equal user input
                programReference.grid.NumOfCellsX = width;
                programReference.grid.NumOfCellsY = height;

                // Update both windows
                Invalidate();
                programReference.Invalidate();
                Hide();
            }
        }
        // Restore the previous input values if the user cancels
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
            RestoreTempValues();
        }

        // Only allows numbers to be inputted
        private void textBoxHeight_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBoxWidth_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void textBoxCellSize_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
