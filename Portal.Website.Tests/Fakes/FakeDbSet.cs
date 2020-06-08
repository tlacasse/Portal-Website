using System.Collections.Generic;
using System.Data.Entity;

namespace Portal.Website.Tests.Fakes {

    public class FakeDbSet<TEntity> : DbSet<TEntity> where TEntity : class {

        public FakeConnectionFactory FakeConnectionFactory { get; set; }
        public List<TEntity> Records { get; } = new List<TEntity>();
        public List<TEntity> ToCommit { get; } = new List<TEntity>();

        public FakeDbSet(FakeConnectionFactory FakeConnectionFactory) {
            this.FakeConnectionFactory = FakeConnectionFactory;
        }

        public void Init(TEntity entity) {
            Records.Add(entity);
        }

        public override TEntity Add(TEntity entity) {
            ToCommit.Add(entity);
            return entity;
        }

        public void SaveChanges() {
            ToCommit.ForEach(x => Records.Add(x));
            ToCommit.Clear();
        }

    }

}
