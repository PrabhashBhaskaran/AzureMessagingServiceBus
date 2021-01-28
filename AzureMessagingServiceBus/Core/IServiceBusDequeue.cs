using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Core
{
    public interface IServiceBusDequeue
    {
        Task CompleteAsync(IEnumerable<ServiceBusReceivedMessage> messages);
        Task<ServiceBusReceivedMessage> DequeueAsync();
        Task<ServiceBusReceivedMessage> DequeueAsync(TimeSpan timeout);
        Task<IEnumerable<ServiceBusReceivedMessage>> DequeueAsync(int messageCount, TimeSpan timeout);
        //Task<bool> HasMessages();
    }
}
