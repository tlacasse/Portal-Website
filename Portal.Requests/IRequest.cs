
namespace Portal.Requests {

    public interface IRequest<TIn, TOut> {

        TOut Process(TIn model);

    }

}
