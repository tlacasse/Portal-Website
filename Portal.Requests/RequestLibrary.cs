using System;
using System.Collections.Generic;

namespace Portal.Requests {

    public class RequestLibrary {

        private Dictionary<Type, object> Map { get; }

        public RequestLibrary() {
            Map = new Dictionary<Type, object>();
        }

        public void Include<TIn, TOut>(IRequest<TIn, TOut> request) {
            Map.Add(request.GetType(), request);
        }

        public TRequest Get<TRequest>() {
            return (TRequest)Map[typeof(TRequest)];
        }

    }

}
