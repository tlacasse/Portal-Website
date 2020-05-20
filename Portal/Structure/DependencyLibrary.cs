using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Structure {

    public class DependencyLibrary : IDependencyLibarySetup {

        private readonly Dictionary<Type, object> Implementations;
        private readonly Dictionary<Type, Type> Marked;

        public DependencyLibrary() {
            Implementations = new Dictionary<Type, object>();
            Marked = new Dictionary<Type, Type>();
        }

        public TInterface Get<TInterface>() {
            return (TInterface)Implementations[typeof(TInterface)];
        }

        public void Include<TInterface>(object item) {
            Implementations.Add(typeof(TInterface), item);
        }

        public void MarkForBuild<TInterface>(Type item) {
            Marked.Add(typeof(TInterface), item);
        }

        public void Build() {
            List<KeyValuePair<Type, Type>> toBuild = Marked
                .OrderBy(p => p.Value.GetConstructors().Single().GetParameters().Length)
                .ToList();
            int countFailed = 0;

            while (toBuild.Count > 0) {
                KeyValuePair<Type, Type> pair = toBuild[0];
                toBuild.RemoveAt(0);
                Type typeInterface = pair.Key;
                Type typeConcrete = pair.Value;
                ConstructorInfo constructor = pair.Value.GetConstructors().Single();
                IEnumerable<object> arguments = constructor
                    .GetParameters()
                    .Select(x => GetArgument(x));

                if (arguments.Any(x => x == null)) {
                    toBuild.Add(pair);
                    countFailed++;
                    if (countFailed == toBuild.Count) {
                        throw new NotImplementedException("Parameters for " + typeInterface.ToString());
                    }
                } else {
                    Implementations.Add(typeInterface, constructor.Invoke(arguments.ToArray()));
                }
            }
        }

        private object GetArgument(ParameterInfo parameter) {
            return Implementations.ContainsKey(parameter.ParameterType)
                ? Implementations[parameter.ParameterType] : null;
        }

    }

}
