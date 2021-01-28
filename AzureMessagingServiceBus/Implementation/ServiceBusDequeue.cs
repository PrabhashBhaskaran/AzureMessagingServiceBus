using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Prabhash.Platform.AzureMessagingServiceBus.Library.Core;

namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Implementation
{
    public class ServiceBusDequeue : IServiceBusDequeue
    {

        private readonly ServiceBusReceiver serviceBusReceiver;
        public ServiceBusDequeue(string connection, string queue)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient(connection);
            serviceBusReceiver = serviceBusClient.CreateReceiver(queue);
           
        }

        async Task IServiceBusDequeue.CompleteAsync(IEnumerable<ServiceBusReceivedMessage> messages)
        {
            foreach (var msg in messages)
            {
                await serviceBusReceiver.CompleteMessageAsync(msg);
            }
        }

        async Task<ServiceBusReceivedMessage> IServiceBusDequeue.DequeueAsync()
        {
            return await serviceBusReceiver.ReceiveMessageAsync();
        }

        async Task<ServiceBusReceivedMessage> IServiceBusDequeue.DequeueAsync(TimeSpan timeout)
        {
            return await serviceBusReceiver.ReceiveMessageAsync(timeout);
        }

        async Task<IEnumerable<ServiceBusReceivedMessage>> IServiceBusDequeue.DequeueAsync(int messageCount, TimeSpan timeout)
        {
            return await serviceBusReceiver.ReceiveMessagesAsync(messageCount, timeout);
        }

    }
}

