
namespace Portal.Structure.Requests {

    public interface IRequest<TIn, TOut> : IRequest {

        TOut Process(TIn model);

    }

    public interface IRequestIn<TIn> : IRequest {

        void Process(TIn model);

    }

    public interface IRequestOut<TOut> : IRequest {

        TOut Process();

    }

    public interface IRequestEvent : IRequest {

        void Process();

    }

    public interface IRequest { }

}
