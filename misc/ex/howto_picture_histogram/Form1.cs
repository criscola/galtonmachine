using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;

namespace howto_picture_histogram
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private const int MIN_VALUE = 0;
        private const int MAX_VALUE = 100;
        private float[] DataValues = new float[6];

        // Make some random data.
        private void Form1_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();

            // Create data.
            for (int i = 0; i < DataValues.Length; i++)
            {
                DataValues[i] = rnd.Next(MIN_VALUE + 5, MAX_VALUE - 5);
                Console.WriteLine(DataValues[i]);//@
            }

            DataValues[0] = 200;

            // Size to make each column 64 pixels wide to match the images.
            int pic_wid = 64 * DataValues.Length;
            picHisto.ClientSize = new Size(pic_wid, picHisto.ClientSize.Height);
            int wid = ClientSize.Width - picHisto.Width + pic_wid;
            ClientSize = new Size(wid, ClientSize.Height);
        }

        // Redraw.
        private void Form1_Resize(object sender, EventArgs e)
        {
            picHisto.Refresh();

        }

        // Draw the histogram.
        private void picHisto_Paint(object sender, PaintEventArgs e)
        {
            // Calculate a transformation to map
            // data values onto the PictureBox.
            float xscale = picHisto.ClientSize.Width / (float)DataValues.Length;
            float yscale = picHisto.ClientSize.Height / (float)(MAX_VALUE - MIN_VALUE);

            DrawHistogram(e.Graphics, picHisto.BackColor, DataValues,
                picHisto.ClientSize.Width, picHisto.ClientSize.Height,
                xscale, yscale);
        }

        // Draw a histogram.
        private void DrawHistogram(Graphics gr, Color back_color,
            float[] values, int width, int height,
            float xscale, float yscale)
        {
            gr.Clear(back_color);

            // The images we will use to fill the rectangles.
            Image[] images =
            {
                Properties.Resources.apple,
                Properties.Resources.banana,
                Properties.Resources.grapes,
                Properties.Resources.pear,
                Properties.Resources.strawberry,
                Properties.Resources.tomato,
            };

            // Draw the histograms.
            for (int i = 0; i < values.Length; i++)
            {
                // Get the rectangle's bounds in device coordinates.
                float rect_wid = xscale;
                float rect_hgt = yscale * values[i];
                float rect_x = i * xscale;
                float rect_y = height - rect_hgt;

                // Make the rectangle.
                RectangleF rect = new RectangleF(
                    rect_x, rect_y, rect_wid, rect_hgt);

                // Fill the rectangle.
                TileRectangle(gr, rect, images[i]);

                // Outline the rectangle.
                gr.DrawRectangle(Pens.Black,
                    rect_x, rect_y, rect_wid, rect_hgt);
            }
        }

        // Display the value clicked.
        private void picHisto_MouseDown(object sender, MouseEventArgs e)
        {
            // Determine which data value was clicked.
            float bar_wid = picHisto.ClientSize.Width / (int)DataValues.Length;
            int i = (int)(e.X / bar_wid);
            MessageBox.Show("Item " + i + " has value " + DataValues[i],
                "Value", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Tile an area from the bottom left corner up.
        private void TileRectangle(Graphics gr, RectangleF rect, Image picture)
        {
            using (TextureBrush brush = new TextureBrush(picture))
            {
                // Move the brush so it starts in the
                // recrangle's lower left corner.
                brush.TranslateTransform(rect.Left, rect.Bottom);

                // Fill.
                gr.FillRectangle(brush, rect);
            }
        }
    }
}
