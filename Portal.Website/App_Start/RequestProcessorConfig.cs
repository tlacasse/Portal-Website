using Portal.Data;
using Portal.Structure;
using Portal.Structure.Requests;
using Portal.Structure.Requests.Processors;

namespace Portal.Website {

    public static class RequestProcessorConfig {

        public static IRequestProcessor BuildNestedRequestProcessor(IDependencyLibrary dependencyLibrary) {
            IRequestProcessor p = new BaseRequestProcessor();
            //p = new ErrorLoggingRequestProcessor(p, dependencyLibrary.Get<IConnectionFactory>());
            return p;
        }

    }

}