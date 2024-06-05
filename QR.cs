using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
namespace PracticDemoExam
{
    public partial class QR : Form
    {
        public QR()
        {
            InitializeComponent();
            string qrtext = "https://docs.google.com/forms/d/e/1FAIpQLSdrAtH9zUQs_svNjRK61wT_S89XcM56vZKk_FpFMpRnbyVxkg/closedform"; 
            QRCodeEncoder encoder = new QRCodeEncoder(); 
            Bitmap qrcode = encoder.Encode(qrtext);
            pictureBox1.Image = qrcode;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
