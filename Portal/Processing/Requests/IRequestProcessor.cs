using System;

namespace Portal.Processing.Requests {

    public interface IRequestProcessor {

        T Process<T>(Func<T> requestCall);

    }

}
