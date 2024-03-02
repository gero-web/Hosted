using AdminEmulator.Services;
using Host.Model;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace AdminEmulator
{
    public partial class Form1 : Form
    {
        private readonly ConectionToHosteService conectionToHosteService;

        public Form1()
        {
            InitializeComponent();
            conectionToHosteService = new ConectionToHosteService((img) => {
                imgBox.BeginInvoke(() =>
                {
                    imgBox.Image = img;
                });
            });
        }

        private async void SendBtn_Click(object sender, EventArgs e)
        {
             await conectionToHosteService.StartAsync();

        }

        

        private async void CanselBtn_Click(object sender, EventArgs e)
        {
          await conectionToHosteService.EndAsync();
        }

        private void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            var x = e.X;
            var y = e.Y;
            var widthImgBox = imgBox.Width;
            var heightImgBox = imgBox.Height;
            double scaleWidth, scaleHeight;

            ScreenScaleCalculation(widthImgBox, heightImgBox, out scaleWidth, out scaleHeight);

            Debug.Print($"{x} -- {y} -- {scaleWidth} -- " +
                $"{scaleHeight} ");

        }

        private void ScreenScaleCalculation(int widthImgBox, int heightImgBox, out double scaleWidth, out double scaleHeight)
        {
            var screenClentWidth = conectionToHosteService.ScreenClentWidth;
            var screenClentHeight = conectionToHosteService.ScreenClentHeight;

            scaleWidth = Math.Round(screenClentWidth / widthImgBox, 4);
            scaleHeight = Math.Round(screenClentHeight / heightImgBox, 4);
        }
    }
}
