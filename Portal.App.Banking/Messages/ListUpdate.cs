using System.Collections.Generic;
using System.Linq;

namespace Portal.App.Banking.Messages {

    public class ListUpdate {

        private string[] Source { get; }

        public IEnumerable<string> Fields {
            get { return Source.Skip(2); }
        }

        public string TableName {
            get { return Source[0]; }
        }

        public int Id {
            get { return int.Parse(Source[1]); }
        }

        public string Name {
            get { return Source[2]; }
        }

        public ListUpdate(string[] Source) {
            this.Source = Source;
        }

    }

}
