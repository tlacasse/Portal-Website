using Portal.Data.ActiveRecord;
using System;

namespace Portal.App.Portal.Models {

    [Table("PortalIcon")]
    public class Icon : ActiveRecordBase {

        [Column("Name")]
        public string Name { get; set; }

        [Column("Image")]
        public string Image { get; set; }

        [Column("Link")]
        public string Link { get; set; }

        [Column("DateCreated")]
        public DateTime DateCreated { get; set; }

        [Column("DateChanged")]
        public DateTime DateChanged { get; set; }

        public bool IsNew {
            get { return Id < 0; }
        }

        public IconHistory ToHistory() {
            return new IconHistory {
                Icon = this,
                Name = Name,
                Image = Image,
                Link = Link,
                IsNew = IsNew,
                DateUpdated = DateTime.Now
            };
        }

        public override string ToString() {
            return string.Format("{0} ({1})", Name, Link);
        }

    }

}
