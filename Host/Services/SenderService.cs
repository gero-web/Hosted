
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Host.Interface;
using Host.Model;
using Newtonsoft.Json;
namespace Host.Services
{
    public class SenderService : ISenderService
    {
        private Socket _socket;
        private string IpClient = null!;
        private object _lock = new object();
        private Thread _threadSendImg;

        public bool Cansel { get; private set; }

        public SenderService()
        {

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                                 ProtocolType.Udp);
            InitTheard();
        }

        private void InitTheard()
        {
            
                _threadSendImg = new(() =>
                {
                    if (IpClient != null)
                    {
                        SendingImg(IPAddress.Parse(IpClient));
                    }
                    else
                    {
                        throw new Exception("Ip клиента не было предоставлено.");
                    }
                })
                {
                    IsBackground = true
                };
            
        }

        private void SendingImg(IPAddress ipHost, int port = 8889)
        {

            var ip = new IPEndPoint(ipHost, port);
            while (!Cansel)
            {
                var n = 18;
                for (int i = 0; i < n; i++)
                {
                    var dto = GetDataAndSize(i);
                    var data = JsonConvert.SerializeObject(dto);
                    var buffer = Encoding.UTF8.GetBytes(data);

                    _socket.SendTo(buffer, ip);

                }

            }

        }


        public void StartSending(string ipConnectedTo)
        {

            if (!_threadSendImg.IsAlive)
            {
                lock (_lock)
                {
                    IpClient = ipConnectedTo;
                    Cansel = false;
                    _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                             ProtocolType.Udp);

                    InitTheard();
                    _threadSendImg.Start();
                }
            }

        }

        public void Disconect()
        {

            if (_threadSendImg.IsAlive)
            {
                lock (_lock)
                {
                    Cansel = true;
                    _socket.DisconnectAsync(true);
                    _socket.Close();

                }
            }

        }

        private static Dto GetBitData(int size, byte[] data)
        {
            var dto = new Dto()
            {
                Size = size,
                Data = data,
            };

            return dto;
        }

        private static Dto GetDataAndSize(int i)
        {
            
            MemoryStream ms = new();
            var path = $"img/{i}/{i}-0000.jpg";
            Bitmap bmp = new(path);
            bmp.Save(ms, ImageFormat.Jpeg);
            bmp.Dispose();
            var data = ms.ToArray();
            ms.Close();
            return GetBitData(data.Length, data);

        }
    }
}
