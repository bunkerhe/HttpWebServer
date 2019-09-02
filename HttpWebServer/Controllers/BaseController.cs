using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpWebServer.Controllers
{
    abstract class BaseController
    {
        public abstract void Handle(HttpListenerContext httpContext);

        protected string GetView(string viewName)
        {
            if (File.Exists(viewName))
            {
                return File.ReadAllText(viewName);
            }
            return string.Empty;
        }

        protected void Render(HttpListenerContext hhtpContext, string html)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);
            hhtpContext.Response.ContentLength64 = buffer.Length;
            Stream output = hhtpContext.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }

}
