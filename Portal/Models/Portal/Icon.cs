using Portal.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Portal.Models.Portal {

    /// <summary>
    /// Model representing the clickable icon bringing you to the link.
    /// </summary>
    public class Icon {

        /// <summary>
        /// Database Record Id.
        /// </summary>
        public int Id { get; set; } = -1;

        /// <summary>
        /// Short name describing the link.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Icon image extension.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Url.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// DateTime when the icon was created.
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// DateTime when the icon was last changed.
        /// </summary>
        public DateTime DateChanged { get; set; }

        /// <summary>
        /// If this Icon is new and will be added to the Website/Database.
        /// </summary>
        public bool IsNew {
            get { return Id < 0; }
        }

        /// <summary>
        /// To String.
        /// </summary>
        public override string ToString() {
            return string.Format("{0} | {1} | {2} | {3}",
                Name,
                Image,
                Id,
                Link == null ? "" : Link.Substring(0, 50)
            );
        }

        /// <summary>
        /// Equals.
        /// </summary>
        public override bool Equals(object obj) {
            Icon other = obj as Icon;
            if (other == null)
                return false;
            return object.Equals(this.Name, other.Name)
                && object.Equals(this.Link, other.Link)
                && object.Equals(this.Image, other.Image)
                && this.Id == other.Id;
        }

        /// <summary>
        /// Hash Code.
        /// </summary>
        public override int GetHashCode() {
            return ToString().GetHashCode();
        }

        /// <summary>
        /// Throw an exception if any of the values of this Icon are not acceptable for the Database.
        /// </summary>
        public void ValidateData() {
            if (string.IsNullOrWhiteSpace(Name))
                throw new ArgumentNullException("Icon Name");
            if (string.IsNullOrWhiteSpace(Link))
                throw new ArgumentNullException("Icon Link");
            if (IsNew && string.IsNullOrWhiteSpace(Image))
                throw new ArgumentNullException("Icon Image");

            if (Name.Length > 30)
                throw new ArgumentOutOfRangeException("Icon Name", "Must be less than 30 characters.");
            if (Link.Length > 500)
                throw new ArgumentOutOfRangeException("Icon Link", "Must be less than 500 characters.");

            string nameWithOnlyValidChars = Regex.Replace(Name, @"[^a-zA-Z0-9 ]", "");
            if (Name != nameWithOnlyValidChars)
                throw new ArgumentException("Icon Name", "Must only have letters, numbers, and spaces.");
        }

        /// <summary>
        /// Build an INSERT INTO or UPDATE statement for the current state of this Icon.
        /// </summary>
        public string BuildUpdateQuery() {
            DatabaseUpdateQuery query = new DatabaseUpdateQuery(IsNew
                    ? DatabaseUpdateQuery.QueryType.INSERT
                    : DatabaseUpdateQuery.QueryType.UPDATE
                , "PortalIcon");

            query.AddField("Name", Name);
            query.AddField("Link", Link);
            query.AddField("DateChanged", PortalUtility.SqlTimestamp, false);
            if (Image != null) {
                query.AddField("Image", Image);
            }
            if (IsNew) {
                query.AddField("DateCreated", PortalUtility.SqlTimestamp, false);
            } else {
                query.WhereClause = "WHERE Id=" + Id;
            }

            return query.Build();
        }

    }

}
