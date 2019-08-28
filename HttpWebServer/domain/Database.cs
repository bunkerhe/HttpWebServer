using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.domain
{
    class Database
    {
        public List<User> Users { get; set; }
    }

    class User
    {
        public string Name { get; set; }
    }
}
