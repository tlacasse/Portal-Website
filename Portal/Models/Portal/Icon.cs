using System;
using System.Text.RegularExpressions;

namespace Portal.Models.Portal {

    /// <summary>
    /// Represents a clickable image linked with a uri.
    /// </summary>
    public class Icon : IModel {

        /// <summary>
        /// Unique Id.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Title of Icon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Image file extension.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Uri to go to when clicked.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// When created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// When last updated.
        /// </summary>
        public DateTime DateChanged { get; set; }

        /// <summary>
        /// If new Icon to be created.
        /// </summary>
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

        public override string ToString() {
            return string.Format("{0} | {1} | {2} | {3}",
                Name,
                Image,
                Id,
                Link == null ? "" : Link.Substring(0, 50)
            );
        }

        public override bool Equals(object obj) {
            Icon other = obj as Icon;
            if (other == null)
                return false;
            return object.Equals(this.Name, other.Name)
                && object.Equals(this.Link, other.Link)
                && object.Equals(this.Image, other.Image)
                && this.Id == other.Id;
        }

        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

    }

}
