using Portal.App.Banking.Messages;
using Portal.App.Banking.Services;
using Portal.Structure.Requests;
using System.Collections.Generic;

namespace Portal.App.Banking.Requests {

    public class ListColumnsRequest : IRequest<string, IEnumerable<ListColumn>> {

        private IListService ListService { get; }

        public ListColumnsRequest(IListService ListService) {
            this.ListService = ListService;
        }

        public IEnumerable<ListColumn> Process(string model) {
            return ListService.GetListInformation(model).ListColumns;
        }

    }

}
