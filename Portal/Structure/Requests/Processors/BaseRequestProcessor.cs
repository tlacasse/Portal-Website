using System;

namespace Portal.Structure.Requests.Processors {

    public class BaseRequestProcessor : IRequestProcessor {

        public T Process<T>(Func<T> requestCall) {
            return requestCall.Invoke();
        }

        public void Process(Action requestCall) {
            requestCall.Invoke();
        }

    }

}
