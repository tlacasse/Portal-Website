using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Structure.DependencyInjection {

    public class DependencyContainer {

        private Dictionary<Type, object> Implementations { get; }

        public DependencyContainer() {
            Implementations = new Dictionary<Type, object>();
        }

        public object GetService<Interface>() {
            return Implementations[typeof(Interface)];
        }

        public void Include<Interface>(Interface item) {
            Implementations.Add(typeof(Interface), item);
        }

        public void BuildFromAssemblies(params Assembly[] assemblies) {
            BuildFromTypes(assemblies.SelectMany(a => a.GetExportedTypes()).ToArray());
        }

        public void BuildFromTypes(params Type[] types) {
            IList<KeyValuePair<Type, Type>> toBuild = GetToBuild(types)
                .OrderBy(p => p.Value.GetConstructors().Single().GetParameters().Length)
                .ToList();
            int countFailed = 0;
            while (toBuild.Count > 0) {
                KeyValuePair<Type, Type> pair = toBuild[0];
                toBuild.RemoveAt(0);
                ConstructorInfo constructor = pair.Value.GetConstructors().Single();
                ParameterInfo[] parameters = constructor.GetParameters();

                List<object> arguments = GetArguments(parameters);
                if (arguments == null) {
                    toBuild.Add(pair);
                    countFailed++;
                    if (countFailed == toBuild.Count) {
                        throw new NotImplementedException("Parameters for " + pair.Value);
                    }
                } else {
                    object newDependency = constructor.Invoke(arguments.ToArray());
                    Implementations.Add(pair.Key, newDependency);
                    countFailed = 0;
                }
            }
        }

        private IList<KeyValuePair<Type, Type>> GetToBuild(IEnumerable<Type> types) {
            IDictionary<Type, Type> needBuilding = types
                .Where(t => t.GetInterfaces().Any(i => i is IService))
                .ToDictionary(t => t.GetInterfaces()
                        .Where(i => i is IService).Single()
                        .GetGenericArguments().Single(),
                        t => t);
            return needBuilding
                .Where(p => !Implementations.ContainsKey(p.Key)).ToList();
        }

        private List<object> GetArguments(ParameterInfo[] parameters) {
            List<object> arguments = new List<object>();
            foreach (ParameterInfo parameter in parameters) {
                if (Implementations.ContainsKey(parameter.ParameterType)) {
                    object argument = Implementations[parameter.ParameterType];
                    arguments.Add(argument);
                } else {
                    return null;
                }
            }
            return arguments;
        }

    }

}
