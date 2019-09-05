using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpWebServer
{
    public class ServiceLocator
    {
        List<ServiceImp> Registered = new List<ServiceImp>();

        public void Register(Type type, Type serviceType)
        {
            var test = serviceType.GetConstructors()[0];
            if (test.GetParameters().Length == 0)
            {
                var inst = Activator.CreateInstance(serviceType);
                Registered.Add(new ServiceImp { ServiceType = type, Service = inst });
            }
            else
            {
                var dependencies = new List<object>();
                var parameters = test.GetParameters();
                foreach (var parameter in parameters)
                {
                    var di = Resolve(parameter.ParameterType);
                    dependencies.Add(di);
                }

                var inst = Activator.CreateInstance(serviceType, dependencies.ToArray());
                Registered.Add(new ServiceImp { ServiceType = type, Service = inst });
            }
        }

        public Object Resolve(Type type)
        {
            foreach (var item in Registered)
            {
                if (type == item.ServiceType)
                {
                    return item.Service;
                }
            }

            return null;
        }
    }

    public class ServiceImp
    {
        public Type ServiceType { get; set; }
        public Object Service { get; set; }
    }
}
