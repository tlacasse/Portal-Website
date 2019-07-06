using Portal.Data.Sqlite;
using Portal.Data.Web;
using Portal.Data.Web.Form;

namespace Portal.Requests {

    public abstract class FileReceiverDependentBase : DependentBase {

        protected IFileReceiver FileReceiver { get; }

        public FileReceiverDependentBase(IConnectionFactory ConnectionFactory,
                IWebsiteState WebsiteState, IFileReceiver FileReceiver)
                : base(ConnectionFactory, WebsiteState) {
            this.FileReceiver = FileReceiver;
        }

    }

}
