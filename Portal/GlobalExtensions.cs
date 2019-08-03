using Portal.Data.Querying;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace Portal {

    public static class GlobalExtensions {

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (T item in enumerable) {
                action(item);
            }
            return enumerable;
        }

        public static bool NotAny<T>(this IEnumerable<T> enumerable, Func<T, bool> test) {
            foreach (T item in enumerable) {
                if (test(item)) {
                    return false;
                }
            }
            return true;
        }

        public static IEnumerable<T> Where<T>(this IEnumerable<T> enumerable, IWhere where) {
            return enumerable.Where(x => where.Validate(x));
        }

        public static void NeedNotNull(this object _, object test, string param = null) {
            if (test == null) {
                throw param == null ? new ArgumentException() : new ArgumentNullException(param);
            }
        }

        public static bool HasAttribute<A>(this MemberInfo attribute) where A : Attribute {
            return attribute.GetCustomAttribute(typeof(A), false) != null;
        }

    }

}
