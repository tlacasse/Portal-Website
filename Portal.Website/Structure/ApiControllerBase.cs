using Portal.Requests;
using System;
using System.Web.Http;

namespace Portal.Website.Structure {

    public class ApiControllerBase : ApiController, IRequestProcessor {

        protected TRequest Get<TRequest>() where TRequest : IRequest {
            return RequestConfig.RequestLibrary.Get<TRequest>();
        }

        public T Process<T>(Func<T> requestCall) {
            return RequestConfig.RequestProcessor.Process(requestCall);
        }

    }

}
