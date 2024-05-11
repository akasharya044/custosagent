using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custosWorker.Service
{
    public class RabbitConnector
    {

        private static ConnectionFactory _factory;
        private static IConnection _connection;
        private static IModel _channel;
        public static IBus CreateBus(string hostName)
        {
            _factory = new ConnectionFactory
            {
                HostName = hostName,
                UserName = "admin",
                Password = "admin@123",
                DispatchConsumersAsync = true
            };
            _connection = _factory.CreateConnection();
            _channel = _connection.CreateModel();
            return new RabbitBus(_channel);
        }
    }
}
