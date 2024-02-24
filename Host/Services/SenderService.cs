
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
        private readonly IPool pool;
        private object _lock = new object();


        public bool Cansel { get; private set; }

        public SenderService(ICastsImages castsImages, IPool pool)
        {

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                                 ProtocolType.Udp);
            this.castsImages = castsImages;
            this.pool = pool;
        }

        private void InitTheard()
        {

            

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

                int byteToSend = 0;
                while (byteToSend < buffer.Length)
                {

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
            lock (_lock)
            {
                IpClient = ipConnectedTo;
                Cansel = false;
                _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                         ProtocolType.Udp);
                pool.SetTask(parm =>
                {
                    SendingImg(IPAddress.Parse(IpClient));
                });
            }
        }

        public void Disconect()
        {
            lock (_lock)
            {
                Cansel = true;
            }
        }
    }
}
