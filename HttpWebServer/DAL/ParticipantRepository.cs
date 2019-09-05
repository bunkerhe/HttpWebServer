using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.domain;
using HttpWebServer.Infrastructure;
using Newtonsoft.Json;

namespace HttpWebServer.DAL
{
    class ParticipantRepository : IParticipantRepository
    {
        private static string jsonFile = "json1.json";

        public ILogger Logger { get; set; }
        private static Database Data { get; set; }
        private static List<Participant> Participants { get; set; }

        public ParticipantRepository(ILogger logger)
        {
            Data = JsonConvert.DeserializeObject<Database>(File.ReadAllText(jsonFile));
            Participants = Data.Participants;
        }


        public void Save(Participant participant)
        {
            var newParticipant = Participants.SingleOrDefault(x => x.Name == participant.Name);
            if (newParticipant != null)
                Delete(newParticipant.Name);

            Participants.Add(participant);
            //Data.Participants.Add(participant);
            Commit();
        }

        public List<Participant> List()
        {
            return Participants;
        }

        public void Delete(string name)
        {
            var participant = Participants.SingleOrDefault(x => x.Name == name);
            Participants.Remove(participant);
            Commit();
        }

        public Participant Get(string name)
        {
            throw new NotImplementedException();
        }

        private void Commit()
        {
            File.WriteAllText(jsonFile, JsonConvert.SerializeObject(Data));
        }
    }
}
