using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer.Model
{
    abstract class BaseModel
    {
        public abstract void Add(string name);
        public abstract string GetAll();
    }
}
