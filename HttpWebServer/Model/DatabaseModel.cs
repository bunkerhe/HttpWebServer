using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HttpWebServer.domain;
using Newtonsoft.Json;

namespace HttpWebServer.Model
{
    class DatabaseModel : BaseModel
    {
        private string jsonFile = "json1.json";
        public override void Add(string name)
        {
            Database data = JsonConvert.DeserializeObject<Database>(File.ReadAllText(jsonFile));
            data.Users.Add(new User { Name = name });

            File.WriteAllText(jsonFile, JsonConvert.SerializeObject(data));
        }

        public override string GetAll()
        {
            Database data = JsonConvert.DeserializeObject<Database>(File.ReadAllText(jsonFile));
            var userList = "";
            foreach (var user in data.Users)
            {
                userList += $"<li>{user.Name}</li>";
            }

            return userList;
        }
    }
}
