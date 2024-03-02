using Host.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AdminEmulator.Services
{
    internal class ReceivingPicture
    {
        readonly int sizeBufferImage = 256;
        Socket Client { get; set; }
        public Action<Image> OutputImage { get; set; }

        public ReceivingPicture(Action<Image> action,string ipAdress = "127.0.0.1",
            int port = 8889)
        {
            Client = new Socket(AddressFamily.InterNetwork,
                        SocketType.Dgram,
                        ProtocolType.Udp);
            var adress = new IPEndPoint(IPAddress.Parse(ipAdress), port);

            Client.Bind(adress);

            OutputImage = action;
        }

        public void RunLoopImges(ref bool IsCansel)
        {
            while (!IsCansel)
            {
                GetSizeImg(out byte[] sizeBytes, out int size);
                GetImageByte(size, out List<byte> sizeBytesToImge);
                Dto? data = CatToDto(sizeBytesToImge);

                if (data != null && data.Data != null)
                {
                    SetImage(data);
                }

            }

        }

        private void SetImage(Dto? data)
        {
            if (data != null)
            {
                var bytesData = data.Data
                                ?? throw new ArgumentNullException("Dto не содержит данных.");

                MemoryStream memoryStream = new(bytesData);
                Image img = (Image)Image.FromStream(memoryStream, true, false).Clone();
                OutputImage?.Invoke(img);

                memoryStream.Close();
            }
        }

        private static Dto? CatToDto(List<byte> sizeBytesToImge)
        {
            var arrayBytes = sizeBytesToImge.ToArray();
            var dataDto = Encoding.UTF8.GetString(arrayBytes);
            var data = JsonConvert.DeserializeObject<Dto>(dataDto);

            return data;
        }

        private void GetImageByte(int size, out List<byte> sizeBytesToImge)
        {
            var sizeBytes = new byte[sizeBufferImage];
            sizeBytesToImge = new List<byte>();
            var reciveSize = 0;
            while (reciveSize < size)
            {
                var recive = Client.Receive(sizeBytes);
                sizeBytesToImge.AddRange(sizeBytes.Take(recive));
                reciveSize += recive;
            }
        }

        private void GetSizeImg(out byte[] sizeBytes, out int size)
        {
            sizeBytes = new byte[sizeBufferImage];
            Client.Receive(sizeBytes);
            size = BitConverter.ToInt32(sizeBytes, 0);
        }
    }
}
