using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using System.Drawing;
using System.Windows.Forms;
namespace Teeths.QRCoder
{
    class QRCoding
    {
        QRCodeGenerator _qrGenerator;

        public QRCoding()
        {
            _qrGenerator = new QRCodeGenerator();
        }

        public void GenerateQr(string data)
        {
            QRCodeData qrCodeData = _qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);

            Bitmap qrCodeImage = new Bitmap(qrCode.GetGraphic(10), new Size(Screen.PrimaryScreen.Bounds.Height-80, Screen.PrimaryScreen.Bounds.Height-80));
                
            Form form = new QRCodeWindow(qrCodeImage, qrCodeImage.Width, qrCodeImage.Height);
            Size s = new Size(qrCodeImage.Width, qrCodeImage.Height);
            form.Size = s;
            form.ShowDialog();
        }
    }
}
