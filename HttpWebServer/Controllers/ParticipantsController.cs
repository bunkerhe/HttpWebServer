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
    class ParticipantsController : BaseController
    {
        private string _oldHtml;
        private DateTime _cashDateTime;
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
            var html = GetView("participants.html").Replace("{{participants}}", Program.Database.GetAll());
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
