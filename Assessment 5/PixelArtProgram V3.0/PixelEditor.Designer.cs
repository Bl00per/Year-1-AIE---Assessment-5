using System.Drawing;
using System.Windows.Forms;

namespace PixelArtProgram_V3._0
{
    partial class PixelEditorGrid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public SizeF AutoScaleDimensions { get; private set; }
        public AutoScaleMode AutoScaleMode { get; private set; }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.APBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.APBox)).BeginInit();
            this.SuspendLayout();
            // 
            // APBox
            // 
            this.APBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.APBox.Location = new System.Drawing.Point(12, 12);
            this.APBox.Name = "APBox";
            this.APBox.Size = new System.Drawing.Size(493, 315);
            this.APBox.TabIndex = 0;
            this.APBox.TabStop = false;
            // 
            // PixelEditor
            // 
            this.ClientSize = new System.Drawing.Size(517, 339);
            this.Controls.Add(this.APBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "PixelEditor";
            this.Text = "PixelEditor";
            ((System.ComponentModel.ISupportInitialize)(this.APBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox APBox;
    }
}

