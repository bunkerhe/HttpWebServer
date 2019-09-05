using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.DAL;
using HttpWebServer.domain;

namespace HttpWebServer.BL
{
    class ParticipantsService : IParticipantsService
    {
        private IParticipantRepository Repository { get; set; }
        public ParticipantsService(IParticipantRepository repository)
        {
            Repository = repository;
        }

        public void Vote(string name, bool isAttend, string reason)
        {
            var participant = new Participant(name, isAttend, reason);
            Repository.Save(participant);
        }

        public List<Participant> ListAll()
        {
            return Repository.List();
        }

        public List<Participant> ListAttendend()
        {
            return Repository.List().Where(x => x.IsAttend).ToList();
        }

        public List<Participant> ListMissed()
        {
            return Repository.List().Where(x => !x.IsAttend).ToList();
        }
    }
}
