using System;

namespace Portal.Data.Sqlite {

    public class NoRowsAffectedException : Exception {

        public string Query { get; }

        public NoRowsAffectedException(string message, string query) : base(message) {
            this.Query = query;
        }

    }

}
