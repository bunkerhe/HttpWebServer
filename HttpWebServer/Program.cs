using System;
using System.Net;
using System.Threading.Tasks;
using HttpWebServer.Controllers;
using HttpWebServer.Model;

namespace HttpWebServer
{
    static class Program
    {
        public static readonly BaseModel Database = new DatabaseModel();
        public static readonly ParticipantsController ParticipantsController = new ParticipantsController();

        static async Task Main(string[] args)
        {
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
                    targetController = new VoteController();
                }
                else
                {
                    if (request.Url.LocalPath.Contains("participants"))
                    {
                        targetController = ParticipantsController;
                    }
                    else
                    {
                        targetController = new IndexController();
                    }
                }

                targetController.Handle(context);
            }
        }
    }
}
