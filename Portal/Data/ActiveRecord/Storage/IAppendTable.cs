
namespace Portal.Data.ActiveRecord.Storage {

    public interface IAppendTable : IView {

        int UncommittedChanges { get; }

        int Commit();

    }

    public interface IAppendTable<X> : IView<X>, IAppendTable where X : IActiveRecord {

        void Insert(X item);

        void Update(X item);

    }

}
