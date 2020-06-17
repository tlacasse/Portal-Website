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

        public static IEnumerable<EI<T>> Enumerate<T>(this IEnumerable<T> enumerable) {
            int i = 0;
            foreach (T item in enumerable) {
                yield return new EI<T>(i, item);
                i++;
            }
        }

        public static IEnumerable<ZPair<A, B>> Zip<A, B>(this IEnumerable<A> first, IEnumerable<B> second) {
            return first.Zip(second, (x, y) => new ZPair<A, B>(x, y));
        }

    }

    public class ZPair<A, B> {

        public A First { get; }
        public B Second { get; }

        public ZPair(A First, B Second) {
            this.First = First;
            this.Second = Second;
        }

    }

    public class EI<T> {

        public int Index { get; }
        public T Item { get; }

        public EI(int Index, T Item) {
            this.Index = Index;
            this.Item = Item;
        }

    }

}
