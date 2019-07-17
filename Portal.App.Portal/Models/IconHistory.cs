using Portal.Data.Models;
using Portal.Data.Models.Attributes;
using System;
using System.Collections.Generic;

namespace Portal.App.Portal.Models {

    public class IconHistory : IModel {

        [Identity]
        public int Id { get; set; } = -1;

        public int IconId { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }

        public bool IsNew { get; set; }

        public DateTime DateUpdated { get; set; }

        public void ValidateData() {
        }

        public bool IsRecordEqual(IModel obj) {
            IconHistory other = obj as IconHistory;
            return other != null && this.Id == other.Id;
        }

        // generated

        public override bool Equals(object obj) {
            var history = obj as IconHistory;
            return history != null &&
                   Id == history.Id &&
                   IconId == history.IconId &&
                   Name == history.Name &&
                   Image == history.Image &&
                   Link == history.Link &&
                   IsNew == history.IsNew &&
                   DateUpdated == history.DateUpdated;
        }

        public override int GetHashCode() {
            var hashCode = -30192229;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + IconId.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Image);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Link);
            hashCode = hashCode * -1521134295 + IsNew.GetHashCode();
            hashCode = hashCode * -1521134295 + DateUpdated.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(IconHistory history1, IconHistory history2) {
            return EqualityComparer<IconHistory>.Default.Equals(history1, history2);
        }

        public static bool operator !=(IconHistory history1, IconHistory history2) {
            return !(history1 == history2);
        }

    }

}
