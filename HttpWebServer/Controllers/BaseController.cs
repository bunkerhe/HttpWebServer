using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using HttpWebServer.Infrastructure;

namespace HttpWebServer.Controllers
{
    abstract class BaseController
    {
        public ILogger Logger { get; set; }
        public BaseController(ILogger logger)
        {
            Logger = logger;
        }
        public abstract void Handle(HttpListenerContext httpContext);

        protected string GetView(string viewName)
        {
            if (File.Exists(viewName))
            {
                return File.ReadAllText(viewName);
            }
            return string.Empty;
        }

        protected void Render(HttpListenerContext httpContext, string html)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);
            httpContext.Response.ContentLength64 = buffer.Length;
            Stream output = httpContext.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

            Logger.Log(httpContext.Request.Url.ToString());
        }
    }

}
