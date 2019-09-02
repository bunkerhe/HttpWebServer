﻿using HttpWebServer.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Controller
{
    class IndexController : BaseController
    {
        public override void Handle(HttpListenerContext httpContext)
        {
            Render(httpContext, GetView("index.html"));
        }
    }
}
