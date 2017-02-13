using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teeths.QRCoder
{
    public partial class QRCodeWindow : Form
    {
        public QRCodeWindow(Bitmap img, int width, int height)
        {
            InitializeComponent();
            pictureBox1.Height = height;
            pictureBox1.Width = width;
            pictureBox1.Image = img;
        }
    }
}
