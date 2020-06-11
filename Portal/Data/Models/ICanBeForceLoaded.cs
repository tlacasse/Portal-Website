
namespace Portal.Data.Models {

    public interface ICanBeForceLoaded<T> {

        T ForceLoad();

    }

}
