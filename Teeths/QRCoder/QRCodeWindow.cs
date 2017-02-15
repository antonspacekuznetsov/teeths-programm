using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teeths.QRCoder
{
    public partial class QRCodeWindow : Form
    {
        private Bitmap _bmp;
        public QRCodeWindow(Bitmap img, int width, int height)
        {
            InitializeComponent();
            _bmp = img;
            pictureBox1.Height = height;
            pictureBox1.Width = width;
            pictureBox1.Image = img;
        }

        private void увеличитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
        }

        private void уменьшитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void менюToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void сохранитьQRКодToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            dialog.FileName = "QrCode";
            dialog.DefaultExt = "jpg";
            dialog.Filter = "JPG images (*.jpg)|*.jpg";
            if (dialog.ShowDialog() == DialogResult.OK)
                {
                   int width = Convert.ToInt32(pictureBox1.Width); 
                   int height = Convert.ToInt32(pictureBox1.Height); 
                   Bitmap bmp = new Bitmap(width,height);    
                   pictureBox1.DrawToBitmap(bmp, new Rectangle(0, 0, width, height));
                    bmp.Save(dialog.FileName, ImageFormat.Jpeg);
                }
        }
    }
}
