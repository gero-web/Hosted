using System.Net.Sockets;
using System.Net;
using System.Text;
using Host.Model;
using Newtonsoft.Json;
using System.Runtime.Serialization.Formatters.Binary;

namespace AdminEmulator
{
    public partial class Form1 : Form
    {
        Socket serv { get; set; }
        Socket client { get; set; }
        private object _lock = new object();

        public bool IsCansel { get; private set; } = false;

        public Form1()
        {
            InitializeComponent();


            client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
                            ProtocolType.Udp);
            var ip = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8889);
            client.Bind(ip);

        }

        private async void SendBtn_Click(object sender, EventArgs e)
        {
            await InitSocket();
            var buffer = BitConverter.GetBytes(true);
            serv.Send(buffer);
            var answerBuff = new Byte[10];
            serv.Receive(answerBuff);
            var answer = BitConverter.ToInt32(answerBuff, 0);
            await serv.DisconnectAsync(false);
            serv.Close();



            if (answer == 1)
            {
                lock (_lock)
                {
                    answer = 0;
                    IsCansel = false;
                    Thread thread = new(() => RunLoopImges());
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
            else
            {
                lock (_lock)
                {
                    IsCansel = true;

                }
            }


        }

        private async Task InitSocket()
        {
            if (serv == null || !serv.Connected)
            {
                serv = new Socket(AddressFamily.InterNetwork, SocketType.Stream,
                    ProtocolType.Tcp);
                await serv.ConnectAsync("127.0.0.1", 8888);
            }
        }

        private void RunLoopImges()
        {

            while (!IsCansel)
            {
                lock (_lock)
                {
                    byte[] sizeBytes = new byte[256];

                    client.Receive(sizeBytes);
                    var size = BitConverter.ToInt32(sizeBytes, 0);
                    var reciveSize = 0;
                    var sizeBytesToImge = new List<byte>();
                    sizeBytes = new byte[256];
                   
                    while (reciveSize < size)
                    {
                       
                        var recive = client.Receive(sizeBytes);
                        sizeBytesToImge.AddRange(sizeBytes.Take(recive));

                        reciveSize += recive;
                    }
                    var arrayBytes = sizeBytesToImge.ToArray();
                    var dataDto = Encoding.UTF8.GetString(arrayBytes);
                    if (dataDto != null)
                    {
                        var data = JsonConvert.DeserializeObject<Dto>(dataDto);
                        if (data != null && data.Data != null)
                        {
                            MemoryStream memoryStream = new(data.Data);
                            Image img = (Image)Image.FromStream(memoryStream, true, false).Clone();
                            imgBox.Image = img;

                            memoryStream.Close();

                        }
                    }

                }
            }

        }



        private async void CanselBtn_Click(object sender, EventArgs e)
        {

            await InitSocket();
            var buffer = BitConverter.GetBytes(false);
            serv.Send(buffer);
            await serv.DisconnectAsync(false);
            serv.Close();


            IsCansel = true;


        }
    }
}
