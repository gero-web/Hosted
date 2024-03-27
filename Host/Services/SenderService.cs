using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
        public bool Cansel { get; private set; }
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;

        public SenderService(ICastsImages castsImages, IPool pool)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                                 ProtocolType.Udp);
            this.castsImages = castsImages;
            this.pool = pool;

        }

        private void SendingImg(IPEndPoint ip)
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

        public void StartSending(string ipConnectedTo, int port = 8889)
        {

            IpClient = ipConnectedTo;
            Cansel = false;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                     ProtocolType.Udp);
            this.cancellationTokenSource = new CancellationTokenSource();
            this.cancellationToken = cancellationTokenSource.Token;

            pool.SetTask(parm =>
                {
                    while (!Cansel)
                    {
                        if (cancellationToken.IsCancellationRequested)
                        {
                            Cansel = true;
                            break;
                        }

                        IPAddress ipHost = IPAddress.Parse(IpClient);
                        var ip = new IPEndPoint(ipHost, port);
                        SendingImg(ip);

                    }
                }, cancellationToken);

        }

        public void Disconect()
        {
            cancellationTokenSource?.Cancel();
        }
    }
}
