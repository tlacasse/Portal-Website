
namespace Portal.Data.ActiveRecord {

    public interface IActiveRecord {

        int Id { get; set; }

        bool IsRecordEqual(object other);

    }

}
