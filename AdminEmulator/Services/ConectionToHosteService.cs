using System.Net.Sockets;

namespace AdminEmulator.Services
{
    internal class ConectionToHosteService
    {
        Socket Serv { get; set; } = null!;
        public double ScreenClentWidth { get; set; } = 1;
        public double ScreenClentHeight { get; set; } = 1;
        readonly int sizeBuffer = 10;
        private ReceivingPicture receivingPicture;
        private bool isCansel = false;
        public bool IsCansel { get => isCansel; set => isCansel = value; }

        private readonly object _syncLock = new();

        public ConectionToHosteService(Action<Image> action)
        {
            receivingPicture = new ReceivingPicture(action);
        }

        public async Task StartAsync()
        {
            InitSocket();
            var answer = await ConfirmConnetionAsync();

            var (screenClentWidth, screenClentHeight) = await GetSizeDisplayToHostedAsync();
            await Serv.DisconnectAsync(false);
            Serv.Close();

            this.ScreenClentWidth = screenClentWidth;
            this.ScreenClentHeight = screenClentHeight;

            if (answer == 1)
            {
                StartTheardGetImage();
            }
            else
            {
                FinishTheardGetImage();
            }
        }

        public async Task EndAsync()
        {
            InitSocket();
            var buffer = BitConverter.GetBytes(false);
            Serv.Send(buffer);
            await Serv.DisconnectAsync(false);
            Serv.Close();

            IsCansel = true;
        }

        private void FinishTheardGetImage()
        {
            lock (_syncLock)
            {
                IsCansel = true;

            }
        }

        private void StartTheardGetImage()
        {

            lock (_syncLock)
            {
                IsCansel = false;
                Thread thread = new(() => receivingPicture.RunLoopImges(ref isCansel))
                {
                    IsBackground = true
                };
                thread.Start();
            }

        }

        private void InitSocket(string ip = "127.0.0.1",
            int port = 8888)
        {
            if (Serv == null || !Serv.Connected)
            {
                Serv = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                    ProtocolType.Tcp);
                Serv.Connect(ip, port);
            }
        }

        private async Task<(int screenClentWidth, int screenClentHeight)>
            GetSizeDisplayToHostedAsync()
        {
            var byteWidth = new byte[sizeBuffer];
            var byteHeigth = new byte[sizeBuffer];
            await Serv.ReceiveAsync(byteWidth);
            await Serv.ReceiveAsync(byteHeigth);
            var screenClentWidth = BitConverter.ToInt32(byteWidth, 0);
            var screenClentHeight = BitConverter.ToInt32(byteHeigth, 0);

            return (screenClentWidth, screenClentHeight);
        }

        private async Task<int> ConfirmConnetionAsync()
        {
            var buffer = BitConverter.GetBytes(true);
            var answerBuff = new byte[sizeBuffer];
            await Serv.SendAsync(buffer);
            await Serv.ReceiveAsync(answerBuff);
            var answer = BitConverter.ToInt32(answerBuff, 0);

            return answer;
        }
    }
}
