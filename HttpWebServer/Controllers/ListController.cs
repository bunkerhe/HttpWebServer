using HttpWebServer.Controllers;
using HttpWebServer.domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Controllers
{
    class ListController : BaseController
    {
        public override void Handle(HttpListenerContext httpContext)
        {
            var html = GetView("list.html").Replace("{{list}}", Program.Database.GetAll());
            Render(httpContext, html);
        }
    }
}
