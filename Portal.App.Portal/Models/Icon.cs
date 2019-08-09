using Portal.Data.ActiveRecord;
using System;
using System.Text.RegularExpressions;

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

        public virtual void ValidateData() {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Icon Name");
            if (string.IsNullOrWhiteSpace(Link))
                throw new ArgumentNullException("Icon Link");
            if (IsNew && string.IsNullOrWhiteSpace(Image))
                throw new ArgumentNullException("Icon Image");

            if (Name.Length > 30)
                throw new ArgumentOutOfRangeException("Icon Name", "Length must be less than 30 characters.");
            if (Link.Length > 500)
                throw new ArgumentOutOfRangeException("Icon Link", "Length must be less than 500 characters.");

            string nameWithOnlyValidChars = Regex.Replace(Name, @"[^a-zA-Z0-9 ]", "");
            if (Name != nameWithOnlyValidChars)
                throw new ArgumentException("Icon Name", "Must only have letters, numbers, and spaces.");
        }

        public IconHistory ToHistory() {
            return new IconHistory {
                IconId = Id,
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
