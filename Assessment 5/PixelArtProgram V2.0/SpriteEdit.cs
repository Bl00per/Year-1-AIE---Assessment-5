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
        public SpriteGrid grid { get; private set; }

        int height;
        int width;
        int CellSize;

        int tempHeight;
        int tempWidth;
        int tempCellSize;

        public SpriteEdit()
        {
            InitializeComponent();
            grid = new SpriteGrid();

            textBoxHeight.Text = grid.NumOfCellsY.ToString();
            textBoxWidth.Text = grid.NumOfCellsX.ToString();
            textBoxCellSize.Text = grid.CellSize.ToString();
        }

        public void BackupTempValues()
        {
            tempHeight = grid.NumOfCellsY;
            tempWidth = grid.NumOfCellsX;
            tempCellSize = grid.CellSize;
        }

        public void RestoreTempValues()
        {
            grid.NumOfCellsY = tempHeight;
            grid.NumOfCellsX = tempWidth;
            grid.CellSize = tempCellSize;

            textBoxHeight.Text = grid.NumOfCellsY.ToString();
            textBoxWidth.Text = grid.NumOfCellsX.ToString();
            textBoxCellSize.Text = grid.CellSize.ToString();
        }

        private void textBoxHeight_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxHeight.Text, out height) == true)
            {
                grid.NumOfCellsY = height;
            }
        }

        private void textBoxWidth_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxWidth.Text, out width) == true)
            {
                grid.NumOfCellsX = width;
            }
        }

        private void textBoxCellSize_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(textBoxCellSize.Text, out CellSize) == true)
            {
                grid.CellSize = CellSize;
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Hide();
            RestoreTempValues();
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            height = grid.NumOfCellsY;
            width = grid.NumOfCellsX;
            CellSize = grid.CellSize;

            Hide();
        }
    }
}
