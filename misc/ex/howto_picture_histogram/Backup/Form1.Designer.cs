namespace howto_picture_histogram
{
    partial class Form1
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
            this.picHisto = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picHisto)).BeginInit();
            this.SuspendLayout();
            // 
            // picHisto
            // 
            this.picHisto.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picHisto.BackColor = System.Drawing.Color.White;
            this.picHisto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picHisto.Cursor = System.Windows.Forms.Cursors.Cross;
            this.picHisto.Location = new System.Drawing.Point(12, 12);
            this.picHisto.Name = "picHisto";
            this.picHisto.Size = new System.Drawing.Size(384, 235);
            this.picHisto.TabIndex = 2;
            this.picHisto.TabStop = false;
            this.picHisto.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picHisto_MouseDown);
            this.picHisto.Paint += new System.Windows.Forms.PaintEventHandler(this.picHisto_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(408, 259);
            this.Controls.Add(this.picHisto);
            this.Name = "Form1";
            this.Text = "howto_picture_histogram";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.picHisto)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.PictureBox picHisto;
    }
}

