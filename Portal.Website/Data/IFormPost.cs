using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Website.Data {

    /// <summary>
    /// Interface for a posted form.
    /// </summary>
    public interface IFormPost {

        /// <summary>
        /// Returns the value of the provided key. Returns null if the key does not exist.
        /// </summary>
        string this[string key] { get; }

        /// <summary>
        /// Returns the uploaded file. Returns null if no files were uploaded.
        /// </summary>
        IPostedFile GetPostedFile();

    }

}
