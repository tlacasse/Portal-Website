
namespace Portal.Data.Sqlite {

    public interface IConnectionCache {

        string ConnectionString { get; }

        IConnection Instance { get; }

    }

}
