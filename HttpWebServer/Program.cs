using System;
using System.Net;
using System.Threading.Tasks;
using HttpWebServer.BL;
using HttpWebServer.Controllers;
using HttpWebServer.DAL;
using HttpWebServer.Infrastructure;

namespace HttpWebServer
{
    static class Program
    {
        private static ServiceLocator ServiceLocator { get; set; }
        private static IParticipantRepository Repository { get; set; }
        private static IParticipantsService Service { get; set; }
        private static ILogger Logger { get; set; }

        static async Task Main(string[] args)
        {
            Logger = new Logger();
            ServiceLocator = new ServiceLocator();
            Repository = new ParticipantRepository(Logger);
            Service = new ParticipantsService(Repository);

            MyMetod();

            Logger = ServiceLocator.Resolve(typeof(ILogger)) as ILogger;
            Repository = ServiceLocator.Resolve(typeof(IParticipantRepository)) as IParticipantRepository;
            Service = ServiceLocator.Resolve(typeof(IParticipantsService)) as IParticipantsService;

            await Listen();
        }

        private static async Task Listen()
        {
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add("http://localhost:8881/");
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (listener.IsListening)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                BaseController targetController;

                if (request.Url.LocalPath.Contains("vote"))
                {
                    targetController = new VoteController(Service, Logger);
                }
                else
                {
                    if (request.Url.LocalPath.Contains("participants"))
                    {
                        targetController = new ParticipantsController(Service, Logger);
                    }
                    else
                    {
                        targetController = new IndexController(Logger);
                    }
                }

                targetController.Handle(context);
            }
        }

        private static void MyMetod()
        {
            ServiceLocator.Register(typeof(ILogger), typeof(Logger));
            ServiceLocator.Register(typeof(IParticipantRepository), typeof(ParticipantRepository));
            ServiceLocator.Register(typeof(IParticipantsService), typeof(ParticipantsService));
        }
    }
}
