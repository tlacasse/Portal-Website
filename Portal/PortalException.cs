using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portal {

    public class PortalException : Exception {

        public string Details { get; }

        public PortalException(string message) : base(message) {
        }

        public PortalException(string message, string Details) : base(message) {
            this.Details = Details;
        }

    }

}
