
namespace Portal.Data.Sqlite {

    public interface IConnectionFactory {

        /// <summary>
        /// Open a new connection.
        /// </summary>
        IConnection Create();

    }

}
