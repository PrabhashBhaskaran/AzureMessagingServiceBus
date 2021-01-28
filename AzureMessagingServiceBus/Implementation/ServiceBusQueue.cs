using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Prabhash.Platform.AzureMessagingServiceBus.Library.Core;
using Azure.Messaging.ServiceBus.Administration;
namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Implementation
{
    public class ServiceBusQueue : IServiceBusQueue
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;
        private readonly IServiceBusDequeue serviceBusDequeue;
        private readonly IServiceBusEnqueue serviceBusEnqueue;
        private readonly string queueName;
        public ServiceBusQueue(string connection, string queue)
        {
            queueName = queue;
            serviceBusAdministrationClient = new ServiceBusAdministrationClient(connection);
            serviceBusDequeue = new ServiceBusDequeue(connection, queue);
            serviceBusEnqueue = new ServiceBusEnqueue(connection, queue);
        }
        public async Task CompleteAsync(IEnumerable<ServiceBusReceivedMessage> messages)
        {
            await serviceBusDequeue.CompleteAsync(messages);
        }

        public async Task<ServiceBusReceivedMessage> DequeueAsync()
        {
           return await serviceBusDequeue.DequeueAsync();
        }

        public async Task<ServiceBusReceivedMessage> DequeueAsync(TimeSpan timeout)
        {
            return await serviceBusDequeue.DequeueAsync(timeout);
        }

        public async Task<IEnumerable<ServiceBusReceivedMessage>> DequeueAsync(int messageCount, TimeSpan timeout)
        {
            return await serviceBusDequeue.DequeueAsync(messageCount,timeout);
        }

        public async Task EnqueueAsync(ServiceBusMessage message)
        {
            await serviceBusEnqueue.EnqueueAsync(message);
        }

        public async Task EnqueueAsync(List<ServiceBusMessage> messages)
        {
            await serviceBusEnqueue.EnqueueAsync(messages);
        }

        public async Task<int> GetActiveMessageCount()
        {
            var response = await serviceBusAdministrationClient.GetQueueRuntimePropertiesAsync(queueName);
            return Convert.ToInt32(response.Value.ActiveMessageCount);

        }
    }
}
