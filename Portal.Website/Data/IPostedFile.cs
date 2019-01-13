using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PortalWebsite.Data {

    /// <summary>
    /// Interface for HttpPostedFile.
    /// </summary>
    public interface IPostedFile {

        /// <summary>
        /// File name.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// MIME type.
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Number of bytes.
        /// </summary>
        int ContentLength { get; }

        /// <summary>
        /// File Stream.
        /// </summary>
        Stream InputStream { get; }

        /// <summary>
        /// Save the file.
        /// </summary>
        void SaveAs(string filename);

    }

}
