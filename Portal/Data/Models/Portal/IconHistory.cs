using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Portal {

    [Table("PortalIcon")]
    public class IconHistory {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        [ForeignKey("PortalIconHistoryPortalIcon")]
        public int IconId { get; set; }
        public Icon Icon { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public string Link { get; set; }

        public bool IsNew { get; set; }

        public DateTime DateUpdated { get; set; }

    }

}
