using Core.Interfases;
using System.Collections.Concurrent;
 
namespace Core.Pools
{
    public class Pool : IPool
    {
          
        public void SetTask(Action<object?> task, CancellationToken cancellationToken)
        {
            Task.Factory.StartNew(task, cancellationToken, TaskCreationOptions.LongRunning);
        }
         
    }
}
