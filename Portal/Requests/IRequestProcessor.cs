using System;

namespace Portal.Requests {

    public interface IRequestProcessor {

        T Process<T>(Func<T> requestCall);

    }

}
