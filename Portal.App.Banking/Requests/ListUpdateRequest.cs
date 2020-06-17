using Portal.App.Banking.Messages;
using Portal.App.Banking.Services;
using Portal.Data;
using Portal.Data.Models;
using Portal.Structure;
using Portal.Structure.Requests;
using System;
using System.Linq;
using System.Reflection;

namespace Portal.App.Banking.Requests {

    public class ListUpdateRequest : CommonDependent, IRequestIn<ListUpdate> {

        private IListService ListService { get; }

        public ListUpdateRequest(IConnectionFactory ConnectionFactory,
                IListService ListService) : base(ConnectionFactory) {
            this.ListService = ListService;
        }

        public void Process(ListUpdate model) {
            using (IConnection connection = ConnectionFactory.Create()) {
                ListInformation info = ListService.GetListInformation(model.TableName, connection);
                object obj = GetRecord(model, info);

                foreach (ZPair<ListColumn, string> pair in info.ListColumns.Skip(1).Zip(model.Fields)) {
                    SetProperty(pair, obj, connection, info);
                }

                ((IHaveDateUpdated)obj).DateUpdated = DateTime.Now;
                connection.Log("Banking List", string.Format("List '{0}' updated '{1}'", model.TableName, model.Name));
                connection.SaveChanges();
            }
        }

        private object GetRecord(ListUpdate model, ListInformation info) {
            object obj = null;
            if (model.Id < 0) {
                // new
                obj = PortalUtility.ConstructEmpty(info.Type);
                info.AddToTable(obj);
            } else {
                obj = info.SelectById(model.Id);
            }
            return obj;
        }

        private void SetProperty(ZPair<ListColumn, string> pair, object obj, IConnection connection, ListInformation info) {
            ListColumn column = pair.First;
            string propval = pair.Second;

            PropertyInfo prop = info.Type.GetProperties()
                    .Where(p => p.Name == column.Name)
                    .Single();

            if (column.Lookup == null) {
                prop.SetValue(obj, propval);
            } else {
                ListInformation refInfo = ListService.GetListInformation(column.Lookup, connection);
                prop.SetValue(obj, refInfo.SelectByName(propval));
            }
            if (prop.GetValue(obj) == null) {
                throw new PortalException(string.Format("Missing value '{0}' on object property '{1}'", propval, column.Name));
            }
        }

    }

}
