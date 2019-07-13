
namespace Portal.Structure {

    public interface IRegisterLibrary<T> : IReadOnlyRegisterLibrary<T> {

        void Include<TItem>(TItem request) where TItem : T;

    }

}
