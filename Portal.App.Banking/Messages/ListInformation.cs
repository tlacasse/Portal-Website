using System;
using System.Collections;
using System.Collections.Generic;

namespace Portal.App.Banking.Messages {

    public class ListInformation {

        public IEnumerable<ListColumn> ListColumns { get; set; }

        public IEnumerable ListQuery { get; set; }

        public Type Type { get; set; }

        public Func<int, object> SelectById { get; set; }

        public Func<string, object> SelectByName { get; set; }

        public Action<object> AddToTable { get; set; }

    }

}
