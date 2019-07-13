using Portal.Data.Models;
using Portal.Data.Models.Attributes;

namespace Portal.Tests {

    public class TestObject : IModel {

        public string Name { get; set; }

        [IdentityAttribute]
        public int Id { get; set; }

        public bool Switch { get; set; }

        public TestObject() {
        }

        public TestObject(string Name, int Id, bool Switch) {
            this.Name = Name;
            this.Id = Id;
            this.Switch = Switch;
        }

        public void ValidateData() {
        }

        public bool IsRecordEqual(IModel obj) {
            TestObject other = obj as TestObject;
            return other != null && this.Id == other.Id;
        }

    }

}
