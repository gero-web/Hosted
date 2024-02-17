
using System;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Core.Interfases;
using System.Drawing.Imaging;

namespace Core.Servises
{
    public class ScreenShotService : IScreenShotService
    {
        private Size size;
        /// <summary>
        /// Пользователь может выставить масштаб отличный от 100% в настройках экрана. Вызывая эту функцию (SetProcessDPIAware), 
        /// вы сообщаете системе, что интерфейс вашего приложения умеет сам правильно масштабироваться при высоких значениях DPI 
        /// (точки на дюйм). Если вы не выставите этот флаг, то интерфейс вашего приложения может выглядеть размыто при 
        /// высоких значениях DPI.
        /// </summary>
        [DllImport("user32.dll")]
        static extern bool SetProcessDPIAware();

        public ScreenShotService()
        {
            SetProcessDPIAware();
            IntPtr hProcess = Process.GetCurrentProcess().MainWindowHandle;
            using Graphics graphics = Graphics.FromHwnd(hProcess);
            size = new Size((int)graphics.VisibleClipBounds.Width, (int)graphics.VisibleClipBounds.Height);
        }

        public byte[] GetScreenByByteArray()
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using Graphics graphics = Graphics.FromImage(bitmap);
            graphics.CopyFromScreen(Point.Empty, Point.Empty, bitmap.Size);
            using MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Jpeg);

            return memoryStream.ToArray();
        }
    }
}
