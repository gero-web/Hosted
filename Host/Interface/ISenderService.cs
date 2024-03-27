namespace Host.Interface
{
    public interface ISenderService
    {
       public void StartSending(string ipConnectTo, int port = 8889);
       public void Disconect();
    }
}
