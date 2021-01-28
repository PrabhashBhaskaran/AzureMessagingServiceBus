using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus.Administration;
using Prabhash.Platform.AzureMessagingServiceBus.Library.Core;

namespace Prabhash.Platform.AzureMessagingServiceBus.Library.Implementation
{
    public class ServiceBusManager : IServiceBusManager
    {
        private readonly ServiceBusAdministrationClient serviceBusAdministrationClient;
        private readonly IServiceBusQueue serviceBusQueue;
        private readonly string _connection;
        public ServiceBusManager(string connection)
        {
            _connection = connection;
            serviceBusAdministrationClient = new ServiceBusAdministrationClient(connection);

        }
        public async Task<IServiceBusQueue> GetDecoratedServiceBusQueue(string name)
        {
            IServiceBusQueue queue=null;
            if (await QueueExists(name))
            {
                queue = new ServiceBusQueue(_connection, name);
            }
            return queue;
        }

        public async Task<bool> QueueExists(string name)
        {
            return await serviceBusAdministrationClient.QueueExistsAsync(name);
        }
    }
}
