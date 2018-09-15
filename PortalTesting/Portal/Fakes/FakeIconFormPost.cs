using PortalWebsite.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTesting.Portal.Fakes {

    public class FakeIconFormPost : IFormPost {

        private Dictionary<string, string> Values { get; }

        private IPostedFile File { get; }

        public FakeIconFormPost(string name, string link, int? id, IPostedFile File) {
            Values = new Dictionary<string, string>();
            if (name != null) Values.Add("Name", name);
            if (link != null) Values.Add("Link", link);
            Values.Add("Id", id == null ? "-1" : ("" + id));
            this.File = File;
        }

        public string this[string key] {
            get { return Values.ContainsKey(key) ? Values[key] : null; }
        }

        public IPostedFile GetPostedFile() {
            return File;
        }

    }

}
