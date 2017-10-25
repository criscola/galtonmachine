namespace howto_draw_normal_distribution
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtMean = new System.Windows.Forms.TextBox();
            this.txtStdDev = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnDraw = new System.Windows.Forms.Button();
            this.picGraph = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mean:";
            // 
            // txtMean
            // 
            this.txtMean.Location = new System.Drawing.Point(89, 15);
            this.txtMean.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtMean.Name = "txtMean";
            this.txtMean.Size = new System.Drawing.Size(56, 22);
            this.txtMean.TabIndex = 1;
            this.txtMean.Text = "0.0";
            this.txtMean.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtStdDev
            // 
            this.txtStdDev.Location = new System.Drawing.Point(89, 47);
            this.txtStdDev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtStdDev.Name = "txtStdDev";
            this.txtStdDev.Size = new System.Drawing.Size(56, 22);
            this.txtStdDev.TabIndex = 3;
            this.txtStdDev.Text = "0.5";
            this.txtStdDev.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 50);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Std Dev:";
            // 
            // btnDraw
            // 
            this.btnDraw.Location = new System.Drawing.Point(155, 30);
            this.btnDraw.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDraw.Name = "btnDraw";
            this.btnDraw.Size = new System.Drawing.Size(100, 28);
            this.btnDraw.TabIndex = 4;
            this.btnDraw.Text = "Draw";
            this.btnDraw.UseVisualStyleBackColor = true;
            this.btnDraw.Click += new System.EventHandler(this.btnDraw_Click);
            // 
            // picGraph
            // 
            this.picGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picGraph.BackColor = System.Drawing.Color.White;
            this.picGraph.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picGraph.Location = new System.Drawing.Point(16, 79);
            this.picGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picGraph.Name = "picGraph";
            this.picGraph.Size = new System.Drawing.Size(627, 227);
            this.picGraph.TabIndex = 5;
            this.picGraph.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(263, 30);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(114, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "DrawHistogram";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnDraw;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 321);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picGraph);
            this.Controls.Add(this.btnDraw);
            this.Controls.Add(this.txtStdDev);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtMean);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "howto_draw_normal_distribution";
            ((System.ComponentModel.ISupportInitialize)(this.picGraph)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMean;
        private System.Windows.Forms.TextBox txtStdDev;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnDraw;
        private System.Windows.Forms.PictureBox picGraph;
        private System.Windows.Forms.Button button1;
    }
}

