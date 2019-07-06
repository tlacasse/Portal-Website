
namespace Portal.Requests {

    public interface IRequestLibrary {

        void Include<TIn, TOut>(IRequest<TIn, TOut> request);

        TRequest Get<TRequest>();

    }

}
