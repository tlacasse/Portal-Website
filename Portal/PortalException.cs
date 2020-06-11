using System;

namespace Portal {

    public class PortalException : Exception {

        public string Details { get; }

        public PortalException(string message) : base(message) {
        }

        public PortalException(string message, string Details) : base(message + " | " + Details) {
            this.Details = Details;
        }

        public PortalException(object message, string Details) : this(message.ToString(), Details) {
        }

    }

}
