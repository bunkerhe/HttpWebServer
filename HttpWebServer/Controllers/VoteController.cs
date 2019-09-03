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
using System.Web;

namespace HttpWebServer.Controllers
{
    class VoteController : BaseController
    {
        public override void Handle(HttpListenerContext httpContext)
        {
            string queryString;
            using (var reader = new StreamReader(httpContext.Request.InputStream, httpContext.Request.ContentEncoding))
            {
                queryString = HttpUtility.UrlDecode(reader.ReadToEnd());
            }

            var params2 = HttpUtility.ParseQueryString(queryString);
            if (params2["attend"] == "on")
            {
                string name = params2["name"];

                Program.Database.Add(name);
                Program.ParticipantsController.ResetCash();
            }

            new IndexController().Handle(httpContext);
        }
    }
}