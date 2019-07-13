
namespace Portal.Data.Models {

    public interface IModel {

        void ValidateData();

        bool IsRecordEqual(IModel obj);

    }

}
