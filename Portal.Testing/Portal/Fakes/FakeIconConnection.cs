using Portal.Data;
using Portal.Models.Portal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTesting.Portal.Fakes {

    public class FakeIconConnection : IConnection {

        public void Dispose() {
        }

        public IList<Model> Execute<Model>(string query, QueryOptions options = QueryOptions.None) {
            if (query.IndexOf("Existing Icon") == -1) {
                return new List<Model>();
            }
            return (IList<Model>)new List<Icon> {
                new Icon() {
                    Name = "Existing Icon",
                    Link = "apple.apple",
                    Id = 1,
                    Image = "png"
                }
            };
        }

        public int ExecuteNonQuery(string query, QueryOptions options = QueryOptions.None) {
            return 1;
        }

    }

}
