using System;

namespace Portal.Data.Sqlite {

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

        public static bool Includes(this QueryOptions opts, QueryOptions test) {
            return (opts & test) == test;
        }

    }

}
