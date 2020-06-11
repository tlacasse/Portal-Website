using Portal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal {

    public static class GlobalExtensions {

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {
            foreach (T item in enumerable) {
                action(item);
            }
            return enumerable;
        }

        public static bool NotAny<T>(this IEnumerable<T> enumerable) {
            return !enumerable.Any();
        }

        public static bool NotAny<T>(this IEnumerable<T> enumerable, Func<T, bool> test) {
            foreach (T item in enumerable) {
                if (test(item)) {
                    return false;
                }
            }
            return true;
        }

        public static void NeedNotNull(this object _, object test, string param = null) {
            if (test == null) {
                throw param == null ? new ArgumentException() : new ArgumentNullException(param);
            }
        }

        public static IEnumerable<T> ForceLoad<T>(this IEnumerable<T> enumerable) where T : ICanBeForceLoaded<T> {
            return enumerable.Select(x => x.ForceLoad()).ToList();
        }

    }

}
