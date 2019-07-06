
namespace Portal.Models {

    public interface IModel {

        /// <summary>
        /// Throw an exception if any properties are invalid.
        /// </summary>
        void ValidateData();

    }

}
