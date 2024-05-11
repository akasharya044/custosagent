using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace custosWorker.Service
{
    public interface IBus
    {

        Task SendAsync<T>(string queue, T message);
        Task ReceiveAsync<T>(string queue, Action<string> onMessage);
        Task ReceiveFromExchangeAsync<T>(string exchangename, Action<string> onMessage);

        Task RabbitReceiveAsync<T>(string queue, Action<string> onMessage);
    }
}
