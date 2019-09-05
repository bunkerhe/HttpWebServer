using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.domain
{
    class Database
    {
        public List<Participant> Participants { get; set; }
    }

    class Participant
    {
        public Participant(string name, bool isAttend, string reason)
        {
            Name = name;
            IsAttend = isAttend;
            Reason = reason;
        }
        public string Name { get; set; }
        public bool IsAttend { get; set; }
        public string Reason { get; set; }
    }
}
