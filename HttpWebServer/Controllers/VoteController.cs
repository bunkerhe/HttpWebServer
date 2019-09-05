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
using HttpWebServer.BL;
using HttpWebServer.Infrastructure;

namespace HttpWebServer.Controllers
{
    class VoteController : BaseController
    {
        private IParticipantsService Service { get; set; }
        private ILogger logger { get; set; }
        public VoteController(IParticipantsService service, ILogger logger) : base(logger)
        {
            Service = service;
        }
        public override void Handle(HttpListenerContext httpContext)
        {
            string queryString;
            using (var reader = new StreamReader(httpContext.Request.InputStream, httpContext.Request.ContentEncoding))
            {
                queryString = HttpUtility.UrlDecode(reader.ReadToEnd());
            }

            var params2 = HttpUtility.ParseQueryString(queryString);
            string name = params2["name"];
            var attend = params2["attend"] == "on";
            string reason = params2["reason"];
            if (!String.IsNullOrEmpty(name))
            {
                Service.Vote(name, attend, reason); 
            }

            new IndexController(logger).Handle(httpContext);
            //httpContext.Response.RedirectLocation = "index.html";
            //httpContext.Response.StatusCode = (int)HttpStatusCode.Found;
            //httpContext.Response.Close();
        }
    }
}