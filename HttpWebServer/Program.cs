using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using HttpWebServer.domain;
using Newtonsoft.Json;
using System.Web;

namespace HttpWebServer
{
    class Program
    {
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
                HttpListenerResponse response = context.Response;

                string file = Directory.GetCurrentDirectory() + request.Url.LocalPath;


                if (File.Exists(file))
                {
                    if (request.Url.LocalPath.Contains("list.html"))
                    {
                        file = HandleListRoute(file);
                    }
                }
                else if (request.Url.LocalPath.Contains("vote"))
                {
                    HandleVoteRouter(request.Url.Query);
                    file = File.ReadAllText("" + "list.html");
                    file = HandleListRoute(file);
                }

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(file);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }

        public static void HandleVoteRouter(string queryString)
        {
            var params2 = HttpUtility.ParseQueryString(queryString);
            if (params2["attend"] == true)
            {
                string name = params2["name"];
            }
        }

        public static string HandleListRoute(string file)
        {
            string fileJson = File.ReadAllText(Directory.GetCurrentDirectory() + "json1.json");
            Database data = JsonConvert.DeserializeObject<Database>(fileJson);
            var userList = "";
            foreach (var user in data.Users)
            {
                userList += $"<li>{user.Name}</li>";
            }

            return file.Replace("{{list}}", userList);
        }
    }
}
