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
using HttpWebServer.BL;
using HttpWebServer.DAL;
using HttpWebServer.Infrastructure;

namespace HttpWebServer.Controllers
{
    class ParticipantsController : BaseController
    {
        private IParticipantsService Service { get; set; }
        private ILogger Logger { get; set; }
        private string _oldHtml;
        private DateTime _cashDateTime;

        public ParticipantsController(IParticipantsService service, ILogger logger) : base(logger)
        {
            Service = service;
            Logger = logger;
        }
        public override void Handle(HttpListenerContext httpContext)
        {
            if (String.IsNullOrEmpty(_oldHtml))
            {
                Cashing(httpContext);
            }
            else
            {
                if (_cashDateTime + new TimeSpan(0,2,0) > DateTime.Now)
                    Render(httpContext, _oldHtml);
                else
                    Cashing(httpContext);
            }
        }

        private void Cashing(HttpListenerContext httpContext)
        {
            var userList = "";
            foreach (var user in Service.ListAttendend())
            {
                userList += $"<li>{user.Name}</li>";
            }

            var html = GetView("participants.html").Replace("{{participants}}", userList);
            Render(httpContext, html);
            _oldHtml = html;
            _cashDateTime = DateTime.Now;
        }

        public void ResetCash()
        {
            _oldHtml = string.Empty;
        }
    }
}
