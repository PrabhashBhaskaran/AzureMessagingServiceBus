using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Core
{
    public interface IServiceBusManager
    {
        Task<IServiceBusQueue> GetDecoratedServiceBusQueue(string name);
        Task<bool> QueueExists(string name);

    }
}
