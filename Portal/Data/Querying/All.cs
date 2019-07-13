
namespace Portal.Data.Querying {

    public sealed class All : IWhere {

        public override string ToString() {
            return "";
        }

        public bool Validate(object obj) {
            return true;
        }

    }

}
