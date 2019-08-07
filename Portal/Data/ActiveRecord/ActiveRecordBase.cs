
namespace Portal.Data.ActiveRecord {

    public abstract class ActiveRecordBase : IActiveRecord {

        [Identity]
        public int Id { get; set; }

        public bool IsRecordEqual(object other) {
            if (other == null)
                return false;
            if (!this.GetType().Equals(other.GetType()))
                return false;
            IActiveRecord record = other as IActiveRecord;
            return this.Id == record.Id;
        }

    }

}
