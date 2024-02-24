using System.Runtime.InteropServices;
using Host.Interface;
using System.Net;
using System.Net.Sockets;

namespace Host.Services
{
    internal class ConnectionService
    {
        private readonly Socket _socketServer;
        private readonly ISenderService senderService;

        public ConnectionService(ISenderService senderService)
        {
            _socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Any, 8888);

            _socketServer.Bind(ep);
            _socketServer.Listen(10);

            this.senderService = senderService;
        }

        public async Task ServerWorker()
        {
            await CommadnsHandlers();
        }

        private async Task CommadnsHandlers()
        {
            bool work = true;
             
            while (work)
            {
                var client = await _socketServer.AcceptAsync();
                var buffer = new byte[1024];
                await client.ReceiveAsync(buffer);
                bool isStart = BitConverter.ToBoolean(buffer, 0);

                if (isStart)
                {
                    var answer = MessageBox((IntPtr)0, "Разрешить подключение удаленному хосту?",
                                 "Запрос на подключение", 1);
                    var answerBytes = BitConverter.GetBytes(answer);
                    await client.SendAsync(answerBytes);
                    if (answer == 1)
                    {
                        var ipHostToConnected = (client.RemoteEndPoint as IPEndPoint)?
                                                 .Address
                                                 .ToString()
                            ?? throw new Exception("Что то пошло нетак");

                        senderService.StartSending(ipHostToConnected);
                         
                    }

                }
                else
                {
                    senderService.Disconect();
                }

                await client.DisconnectAsync(false);
            }

            [DllImport("User32.dll", CharSet = CharSet.Unicode)]
            static extern int MessageBox(IntPtr hWind, string msg, string caption, int type);
        }
    }
}
