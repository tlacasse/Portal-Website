using System;

namespace Portal.Structure.Requests {

    public interface IRequestProcessor {

        T Process<T>(Func<T> requestCall);

        void Process(Action requestCall);

    }

}
