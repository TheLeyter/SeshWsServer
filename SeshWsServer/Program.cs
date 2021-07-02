using System;
using System.Collections.Generic;
using System.Threading;
using Fleck;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SeshWsServer;
using SeshWsServer.Service;
using SeshWsServer.Model;
using SeshWsServer.Model.Message;

namespace SeshWsServer
{
    class Program
    {
        private static string ip = "127.0.0.1";

        private static int port = 5002;

        private static IServiceScope _services;

        private static WsServer server;
        static void Main(string[] args)
        {
            //MessageBase<StrMessage> msg = new MessageBase<StrMessage>(MessageType.stringMsg, new StrMessage() { Msg = "Hello!"});

            //Console.WriteLine(JsonConvert.SerializeObject(msg));

            //var res = JsonConvert.DeserializeObject<MessageBase<object>>(@"{""type"":0,""payload"":{""Msg"":""Hello!""}}");

            ManualResetEvent mre = new ManualResetEvent(false);

            registerService();

            server = new WsServer(System.Net.IPAddress.Parse(ip), port, _services.ServiceProvider.GetService<jwt>(), _services.ServiceProvider.GetService<Database>());

            server.start();

            Console.CancelKeyPress += (sender, e) =>
            {

                e.Cancel = true;
                server.stop();
                mre.Set();

            };

            consoleInfo.stdInfo("Pres Ctrl + C to stoped server");

            mre.WaitOne();

            DisposeServices();
        }

        private static void registerService()
        {
            var services = new ServiceCollection();

            services.AddDbContext<Database>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("db")));

            services.AddSingleton<jwt>();

            _services = services.BuildServiceProvider(true).CreateScope();

        }

        private static void DisposeServices()
        {
            if (_services == null)
            {
                return;
            }
            if (_services is IDisposable)
            {
                ((IDisposable)_services).Dispose();
            }
        }

    }
}
