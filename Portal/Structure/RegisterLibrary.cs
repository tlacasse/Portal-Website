using System;
using System.Collections.Generic;

namespace Portal.Structure {

    public class RegisterLibrary<T> : IRegisterLibrary<T> {

        private readonly Dictionary<Type, T> Map;

        public RegisterLibrary() {
            Map = new Dictionary<Type, T>();
        }

        public void Include<TItem>(TItem request) where TItem : T {
            Map.Add(request.GetType(), request);
        }

        public TItem Get<TItem>() where TItem : T {
            return (TItem)Map[typeof(TItem)];
        }

    }

}
