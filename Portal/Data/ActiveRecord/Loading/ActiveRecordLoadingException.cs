using System;

namespace Portal.Data.ActiveRecord.Loading {

    public sealed class ActiveRecordLoadingException : Exception {

        public ActiveRecordLoadingException(string message) : base(message) {
        }

    }

}
