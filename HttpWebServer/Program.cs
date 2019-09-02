using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HttpWebServer.domain;
using Newtonsoft.Json;
using System.Web;
using HttpWebServer.Controller;
using HttpWebServer.Controllers;
using HttpWebServer.Model;

namespace HttpWebServer
{
    class Program
    {
        public static BaseModel Database = new DatabaseModel();
 
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
                    if (request.Url.LocalPath.Contains("list"))
                    {
                        targetController = new ListController();
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
