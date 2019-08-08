
namespace Portal.Processing.Requests {

    public interface IRequest<TIn, TOut> : IRequest {

        TOut Process(TIn model);

    }

    public interface IRequestIn<TIn> : IRequest {

        void Process(TIn model);

    }

    public interface IRequestOut<TOut> : IRequest {

        TOut Process();

    }

    public interface IRequest { }

}
