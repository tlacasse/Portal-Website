using Portal.App.Banking.Messages;
using Portal.App.Banking.Services;
using Portal.Data;
using Portal.Structure;
using Portal.Structure.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Portal.App.Banking.Requests {

    public class ListDataRequest : CommonDependent, IRequest<string, IEnumerable<IEnumerable<string>>> {

        private IListService ListService { get; }

        public ListDataRequest(IConnectionFactory ConnectionFactory,
                IListService ListService) : base(ConnectionFactory) {
            this.ListService = ListService;
        }

        public IEnumerable<IEnumerable<string>> Process(string model) {
            using (IConnection connection = ConnectionFactory.Create()) {
                ListInformation info = ListService.GetListInformation(model, connection);
                return BuildResults(info).ToList();
            }
        }

        private IEnumerable<IEnumerable<string>> BuildResults(ListInformation info) {
            foreach (object obj in info.ListQuery) {
                yield return BuildRecord(info, obj);
            }
        }

        private IEnumerable<string> BuildRecord(ListInformation info, object obj) {
            return info.ListColumns
                .Select(c => BuildValue(c, info.Type, obj));
        }

        private string BuildValue(ListColumn column, Type type, object obj) {
            object val = type
                .GetProperties()
                .Where(p => p.Name == column.Name)
                .Single()
                .GetValue(obj);
            if (column.Lookup == null) {
                return val.ToString();
            } else {
                return column.Lookup
                    .GetProperties()
                    .Where(p => p.Name == "Name")
                    .Single()
                    .GetValue(val)
                    .ToString();
            }
        }

    }

}
