using Portal.Data.Models;
using Portal.Data.Models.Attributes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Portal.App.Portal.Models {

    public class Icon : IModel {

        [Identity]
        public int Id { get; set; } = -1;

        public string Name { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateChanged { get; set; }

        [UpdateIgnore]
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
            return string.Format("{0} ({1})",
                Name, Link == null ? "" : Link.Substring(0, 50)
            );
        }

        public bool IsRecordEqual(IModel obj) {
            Icon other = obj as Icon;
            return other != null && this.Id == other.Id && this.Id >= 0;
        }

        // generated

        public override bool Equals(object obj) {
            var icon = obj as Icon;
            return icon != null &&
                   Id == icon.Id &&
                   Name == icon.Name &&
                   Image == icon.Image &&
                   Link == icon.Link;
        }

        public override int GetHashCode() {
            var hashCode = 259765913;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Link);
            return hashCode;
        }

        public static bool operator ==(Icon icon1, Icon icon2) {
            return EqualityComparer<Icon>.Default.Equals(icon1, icon2);
        }

        public static bool operator !=(Icon icon1, Icon icon2) {
            return !(icon1 == icon2);
        }

    }

}
