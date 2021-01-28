using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using Azure.Messaging.ServiceBus;
using Prabhash.Platform.AzureMessagingServiceBus.Library.Core;
using Prabhash.Platform.AzureMessagingServiceBus.Library.Implementation;

namespace Test
{
    class Program
    {
       static string queuename = ConfigurationManager.AppSettings["queue"];
        static string connection = ConfigurationManager.AppSettings["connection"];
        static int _batchsize = Convert.ToInt32(ConfigurationManager.AppSettings["batchSize"]);
        static int timeout = Convert.ToInt32(ConfigurationManager.AppSettings["Timeout"]);

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1.Send Messages to queue\n2.Receive Messages from queue\n3.Receive Messages with commit\n4.Clear\n5.Quit");
                ConsoleKey key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        SendMessages(queuename, connection);
                        break;
                    case ConsoleKey.D2:
                        ReadMessages(queuename, connection);
                        break;
                    case ConsoleKey.D3:
                        ReadMessagesAndCommit(queuename, connection);
                        break;
                    case ConsoleKey.D4:
                        Console.Clear();
                        break;
                    case ConsoleKey.D5:
                        return;
                }
            }
        }

        private static void SendMessages(string queue, string connectionString)
        {
            IServiceBusManager manager = new ServiceBusManager(connectionString);
            List<ServiceBusMessage> mesages = GenerateSampleMessages();
            if (manager.QueueExists(queue).Result)
            {
                manager.GetDecoratedServiceBusQueue(queue).Result.EnqueueAsync(mesages);
            }
            else
            {
                Console.WriteLine("Invalid Queue name : " + queue);
            }
        }

        private static void ReadMessages(string queue, string connectionString)
        {
            IServiceBusManager manager = new ServiceBusManager(connectionString);
            if (manager.QueueExists(queue).Result)
            {
                List<ServiceBusReceivedMessage> mesages = manager.GetDecoratedServiceBusQueue(queue).Result.DequeueAsync(_batchsize, TimeSpan.FromSeconds(10)).Result.ToList();
                if (mesages.Count == 0) { Console.WriteLine("\nQueue Empty!"); return; }
                foreach (var m in mesages)
                {
                    string msg = m.Body.ToString();
                    Console.WriteLine(msg);
                }
            }
            else
            {
                Console.WriteLine("Invalid Queue name : " + queue);
            }
        }

        private static void ReadMessagesAndCommit(string queue, string connectionString)
        {
            IServiceBusManager manager = new ServiceBusManager(connectionString);
            if (manager.QueueExists(queue).Result)
            {
                IServiceBusQueue decorator = manager.GetDecoratedServiceBusQueue(queue).Result;
                List<ServiceBusReceivedMessage> mesages = decorator.DequeueAsync(_batchsize, TimeSpan.FromSeconds(timeout)).Result.ToList();
                if (mesages.Count == 0) { Console.WriteLine("\nQueue Empty!"); return; }
                foreach (var m in mesages)
                {
                    string msg = m.Body.ToString();
                    Console.WriteLine(msg);
                }
                decorator.CompleteAsync(mesages);
            }
            else
            {
                Console.WriteLine("Invalid Queue name : " + queue);
            }
        }
        private static List<ServiceBusMessage> GenerateSampleMessages()
        {
            List<ServiceBusMessage> mesages = new List<ServiceBusMessage>();

            for (int i = 0; i < 200; i++)
            {
                mesages.Add(new ServiceBusMessage("Message" + i));
            }

            return mesages;
        }
    }
}


