namespace Host.Interface
{
    public interface ISenderService
    {
       public void StartSending(string ipConnectTo);
       public void Disconect();
    }
}
