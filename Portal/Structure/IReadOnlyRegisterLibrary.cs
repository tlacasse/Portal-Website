
namespace Portal.Structure {

    public interface IReadOnlyRegisterLibrary<T> {

        TItem Get<TItem>() where TItem : T;

    }

}
