namespace PixelArtProgram_V2._0
{
    partial class SpriteEdit
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpriteEdit));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxHeight = new System.Windows.Forms.TextBox();
            this.textBoxWidth = new System.Windows.Forms.TextBox();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.maximumValues = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Width";
            // 
            // textBoxHeight
            // 
            this.textBoxHeight.Location = new System.Drawing.Point(56, 32);
            this.textBoxHeight.Name = "textBoxHeight";
            this.textBoxHeight.ShortcutsEnabled = false;
            this.textBoxHeight.Size = new System.Drawing.Size(114, 20);
            this.textBoxHeight.TabIndex = 2;
            this.textBoxHeight.Text = "\r\n";
            this.textBoxHeight.TextChanged += new System.EventHandler(this.textBoxHeight_TextChanged);
            this.textBoxHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxHeight_KeyPress);
            // 
            // textBoxWidth
            // 
            this.textBoxWidth.Location = new System.Drawing.Point(56, 6);
            this.textBoxWidth.Name = "textBoxWidth";
            this.textBoxWidth.ShortcutsEnabled = false;
            this.textBoxWidth.Size = new System.Drawing.Size(114, 20);
            this.textBoxWidth.TabIndex = 1;
            this.textBoxWidth.TextChanged += new System.EventHandler(this.textBoxWidth_TextChanged);
            this.textBoxWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWidth_KeyPress);
            // 
            // buttonApply
            // 
            this.buttonApply.Location = new System.Drawing.Point(12, 99);
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.Size = new System.Drawing.Size(75, 23);
            this.buttonApply.TabIndex = 3;
            this.buttonApply.Text = "Apply";
            this.buttonApply.UseVisualStyleBackColor = true;
            this.buttonApply.Click += new System.EventHandler(this.buttonApply_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(93, 99);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // maximumValues
            // 
            this.maximumValues.AutoSize = true;
            this.maximumValues.Location = new System.Drawing.Point(4, 63);
            this.maximumValues.Name = "maximumValues";
            this.maximumValues.Size = new System.Drawing.Size(171, 26);
            this.maximumValues.TabIndex = 8;
            this.maximumValues.Text = "The maximum value for Width: 160\r\nThe maximum value for Height: 95";
            // 
            // SpriteEdit
            // 
            this.AcceptButton = this.buttonApply;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(182, 134);
            this.Controls.Add(this.maximumValues);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.textBoxWidth);
            this.Controls.Add(this.textBoxHeight);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "SpriteEdit";
            this.Text = "SpriteEdit";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxHeight;
        private System.Windows.Forms.TextBox textBoxWidth;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Label maximumValues;
    }
}