using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Core
{
    public interface IServiceBusEnqueue
    {
        Task EnqueueAsync(ServiceBusMessage message);
        Task EnqueueAsync(List<ServiceBusMessage> messages);
    }
}
