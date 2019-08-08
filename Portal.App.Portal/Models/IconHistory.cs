using Portal.Data.ActiveRecord;
using System;

namespace Portal.App.Portal.Models {

    [Table("PortalIconHistory")]
    public class IconHistory : ActiveRecordBase {

        [References(typeof(Icon))]
        public Icon Icon { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Image")]
        public string Image { get; set; }

        [Column("Link")]
        public string Link { get; set; }

        [Column("IsNew")]
        public bool IsNew { get; set; }

        [Column("DateUpdated")]
        public DateTime DateUpdated { get; set; }

    }

}
