using System.Collections.Generic;
using System.Linq;

namespace Portal.Data.Querying {

    public sealed class And : IWhere {

        private IEnumerable<IWhere> Conditions { get; }

        public And(params IWhere[] conditions) {
            this.Conditions = conditions;
        }

        public override string ToString() {
            return "(" + string.Join(" AND ", Conditions) + ")";
        }

        public bool Validate(object obj) {
            return Conditions.All(c => c.Validate(obj));
        }

    }

}
