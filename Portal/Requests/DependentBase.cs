using Portal.Data.ActiveRecord.Storage;

namespace Portal.Requests {

    public abstract class DependentBase {

        protected IActiveContext ActiveContext { get; }

        public DependentBase(IActiveContext ActiveContext) {
            this.ActiveContext = ActiveContext;
        }

    }

}
