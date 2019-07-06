using System.IO;

namespace Portal.Data.Web.Form {

    public interface IPostedFile {

        string FileName { get; }

        string ContentType { get; }

        int ContentLength { get; }

        Stream InputStream { get; }

        void SaveAs(string filename);

    }

}
