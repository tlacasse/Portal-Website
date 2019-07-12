using System;
using System.Linq;
using System.Reflection;

namespace Portal {

    public static class PortalUtility {

        public static T ConstructEmpty<T>() {
            ConstructorInfo constructor = typeof(T).GetConstructors()
                .Where(c => c.GetParameters().Length == 0).Single();
            return (T)(constructor.Invoke(null));
        }

    }

}
