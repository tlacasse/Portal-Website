using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Structure.DependencyInjection {

    public class DependencyContainer {

        private Dictionary<Type, object> Implementations { get; }

        public DependencyContainer() {
            Implementations = new Dictionary<Type, object>();
        }

        public void Include<Interface>(Interface item) {
            Implementations.Add(typeof(Interface), item);
        }

        public void BuildFromAssemblies(params Assembly[] assemblies) {
            IDictionary<Type, Type> toBuild = assemblies
                .SelectMany(a => a.ExportedTypes)
                .Where(t => t.GetInterfaces().Any(i => i is IService))
                .ToDictionary(t => t,
                    t => t.GetInterfaces()
                        .Where(i => i is IService).Single()
                        .GetGenericArguments().Single());
            toBuild = toBuild
                .Where(p => !Implementations.ContainsKey(p.Value))
                .ToDictionary(p => p.Key, p => p.Value);
        }

    }

}
