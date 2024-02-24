using Core.Interfases;
using System.Collections.Concurrent;
 
namespace Core.Pools
{
    public class Pool : IPool
    {
        private Thread[] threads;
        private readonly object syncLock = new();
        private readonly ConcurrentQueue<Action<object?>> taskActionsQueue;

        public Pool(int theardCount = 2)
        {
            taskActionsQueue = new ConcurrentQueue<Action<object?>>();
            Init(theardCount);
        }

        private void Init(int theardCount = 2)
        {
            threads = new Thread[theardCount];
            for (int i = 0; i < theardCount; i++)
            {
                threads[i] = new Thread(Works)
                {
                    Name = $"CustomPool -- Theard -- {i}",
                    IsBackground = true,
                    Priority = ThreadPriority.Normal,
                };
                threads[i].Start();
            }
        }

        public void SetTask(Action<object?> task)
        {
            lock (syncLock)
            {
                taskActionsQueue.Enqueue(task);
                if(taskActionsQueue.Count == 1)
                     Monitor.Pulse(syncLock);
            }
        }

        private void Works(object? obj)
        {
            while (true)
            {
                lock (syncLock)
                {
                    if (!taskActionsQueue.IsEmpty)
                    {
                        var isTask = taskActionsQueue.TryDequeue(out var action);
                        if (isTask)
                        { 
                            action?.Invoke(obj);
                        }
                    }
                    else
                    {
                        Monitor.Wait(syncLock);
                    }
                }
            }
        }
    }
}
