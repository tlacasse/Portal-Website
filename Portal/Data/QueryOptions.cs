using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal.Data {

    /// <summary>
    /// Options for queries.
    /// </summary>
    [Flags]
    public enum QueryOptions {
        /// <summary>
        /// No changes.
        /// </summary>
        None = 0,
        /// <summary>
        /// If the query should be logged.
        /// </summary>
        Log = 1,
        /// <summary>
        /// If a non-result query has no effected rows, an exception will not be thrown.
        /// </summary>
        AllowNoUpdatedRows = 2,
    }

    public static class QueryOptionsExtensions {

        /// <summary>
        /// If a set of options includes a specific option.
        /// </summary>
        public static bool Includes(this QueryOptions opts, QueryOptions test) {
            return (opts & test) == test;
        }

    }

}
