namespace Core.Interfases
{
    public interface IPool
    {
        void SetTask(Action<object?> task, CancellationToken cancellationToken);
    }
}