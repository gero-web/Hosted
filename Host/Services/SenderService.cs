 
using System.Net;
using System.Net.Sockets;
using System.Text;
using Core.Interfases;
using Host.Interface;
using Newtonsoft.Json;
namespace Host.Services
{
    public class SenderService : ISenderService
    {
        private Socket _socket;
        private readonly ICastsImages castsImages;
        private string IpClient = null!;
        private object _lock = new object();
        private Thread _threadSendImg;

        public bool Cansel { get; private set; }

        public SenderService(ICastsImages castsImages)
        {

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                                 ProtocolType.Udp);
            InitTheard();
            this.castsImages = castsImages;
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

                var dto = castsImages.GetDataAndSize();
                var data = JsonConvert.SerializeObject(dto);
                var buffer = Encoding.UTF8.GetBytes(data);
                var byteSize = BitConverter.GetBytes(buffer.Length); 
                _socket.SendTo(byteSize, ip);
                int count = 0;
                int byteToSend = 0;
                while (byteToSend < buffer.Length)
                {
                    count++;
                    var sendByte = buffer.Skip(byteToSend)
                                         .Take(256)
                                         .ToArray();
                    byteToSend += _socket.SendTo(sendByte, ip);
                }
                //Выделяем время перед следующей отправкой
                Thread.Sleep(200);
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
                }
            }

        }
    }
}
