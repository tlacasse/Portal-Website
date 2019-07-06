using Portal.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace Portal.Testing.Fakes.Data {

    public class FakeConnectionFactory<TModel> : IConnectionFactory {

        private Func<string, IList<TModel>> queryResult;
        private Action<string> nonQueryAction;

        public FakeConnectionFactory(Func<string, IList<TModel>> queryResult, Action<string> nonQueryAction) {
            this.queryResult = queryResult;
            this.nonQueryAction = nonQueryAction;
        }

        public FakeConnectionFactory() : this(_ => throw new InvalidOperationException(), _ => { }) {
        }

        public FakeConnectionFactory(Func<string, IList<TModel>> queryResult) : this(queryResult, _ => { }) {
        }

        public FakeConnectionFactory(Action<string> nonQueryAction) : this(_ => throw new InvalidOperationException(), nonQueryAction) {
        }

        public virtual IConnection Create() {
            return new FakeConnection<TModel>(queryResult, nonQueryAction);
        }

    }

}
