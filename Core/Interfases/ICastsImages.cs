using Host.ValueObjects;

namespace Core.Interfases
{
    public interface ICastsImages
    {
        Dto CreateDto(int size, byte[] data);
        Dto GetDataAndSize(int i);
    }
}