
namespace Portal.Requests {

    public interface IRequest<TIn, TOut> : IRequest {

        TOut Process(TIn model);

    }

    public interface IRequest { }

}
