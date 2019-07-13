using System.Collections.Generic;
using System.Linq;

namespace Portal.Data.Querying {

    public sealed class Or : IWhere {

        private IEnumerable<IWhere> Conditions { get; }

        public Or(params IWhere[] conditions) {
            this.Conditions = conditions;
        }

        public override string ToString() {
            return "(" + string.Join(" OR ", Conditions) + ")";
        }

        public bool Validate(object obj) {
            return Conditions.Any(c => c.Validate(obj));
        }

    }

}
