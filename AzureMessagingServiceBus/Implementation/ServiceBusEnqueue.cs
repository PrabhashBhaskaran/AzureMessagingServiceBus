using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Prabhash.Platform.AzureMessagingServiceBus.Library.Core;

namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Implementation
{
    public class ServiceBusEnqueue : IServiceBusEnqueue
    {
        private readonly ServiceBusSender serviceBusSender;
        public ServiceBusEnqueue(string connection,string queue)
        {
            ServiceBusClient serviceBusClient = new ServiceBusClient(connection);
            serviceBusSender = serviceBusClient.CreateSender(queue);
        }
        async Task IServiceBusEnqueue.EnqueueAsync(ServiceBusMessage message)
        {
             await serviceBusSender.SendMessageAsync(message);
        }

        async Task IServiceBusEnqueue.EnqueueAsync(List<ServiceBusMessage> messages)
        {
            await serviceBusSender.SendMessagesAsync(messages);
        }
    }
}
