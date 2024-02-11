using Host.ValueObjects;
using Core.Interfases;


namespace Core.Servises
{
    public class CastsImages : ICastsImages
    {
        public Dto CreateDto(int size, byte[] data)
        {
            var dto = new Dto()
            {
                Size = size,
                Data = data,
            };

            return dto;
        }

        public Dto GetDataAndSize(int i)
        {

            var path = $"img/{i}/{i}-0000.jpg";
            var byteArray = File.ReadAllBytes(path);

            return CreateDto(byteArray.Length, byteArray);

        }
    }
}
