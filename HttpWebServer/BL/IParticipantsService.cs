using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.DAL;
using HttpWebServer.domain;

namespace HttpWebServer.BL
{
    interface IParticipantsService
    {
        void Vote(string name, bool isAttend, string reason);

        List<Participant> ListAll();

        List<Participant> ListAttendend();
        List<Participant> ListMissed();
    }
}