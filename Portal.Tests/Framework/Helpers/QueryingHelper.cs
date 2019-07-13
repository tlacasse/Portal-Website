using Portal.Data.Querying;
using System.Collections.Generic;

namespace Portal.Tests.Framework.Helpers {

    public static class QueryingHelper {

        public static List<TestObject> GetTestEnumerable() {
            List<TestObject> list = new List<TestObject>();
            string[] names = new string[] { "Apple", "Banana", "Waffle" };
            int[] ids = new int[] { 0, 1, 2 };
            bool[] switches = new bool[] { false, true };
            foreach (string n in names) {
                foreach (int i in ids) {
                    foreach (bool s in switches) {
                        list.Add(new TestObject(n, i, s));
                    }
                }
            }
            return list;
        }

        public static Dictionary<string, IWhere> GetTestQueries() {
            return new Dictionary<string, IWhere> {
                ["NameEqualsApple"] = new Equals<string>("Name", "Apple"),
                ["IdEquals1"] = new Equals<int>("Id", 1),
                ["SwitchIsTrue"] = new Equals<bool>("Switch", true)
            };
        }

    }

}
