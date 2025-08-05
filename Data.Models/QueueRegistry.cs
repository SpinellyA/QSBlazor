using Data.Models;
using System;

public class QueueRegistry
{
    public Dictionary<int, QueueSystemInstance> RegisteredQueues { get; set; }

    public QueueRegistry() { 
        RegisteredQueues = new Dictionary<int, QueueSystemInstance>();
    }  

    public void Register(QueueSystemInstance instance)
    {
        RegisteredQueues.Add(instance.QueueId, instance);
    }

    public QueueSystemInstance GetQueue(int id)
    {
        return RegisteredQueues[id];
    }
}