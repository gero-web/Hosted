using Host.ValueObjects;
using Core.Interfases;
using System.Drawing;
using System.Drawing.Imaging;


namespace Core.Servises
{
    public class CastsImages : ICastsImages
    {
        private readonly IScreenShotService screenShotService;

        public CastsImages(IScreenShotService screenShotService)
        {
            this.screenShotService = screenShotService;
        }
     
        private Dto CreateDto(int size, byte[] data)
        {
            var dto = new Dto()
            {
                Size = size,
                Data = data,
            };

            return dto;
        }

        public Dto GetDataAndSize()
        {
            var bmpByte = screenShotService.GetScreenByByteArray();
            return CreateDto(bmpByte.Length, bmpByte);

        }
    }
}
