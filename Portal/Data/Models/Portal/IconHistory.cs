using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Portal {

    [Table("PortalIconHistory")]
    public class IconHistory {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual int IconId { get; set; }
        [ForeignKey("IconId")]
        public virtual Icon Icon { get; set; }

        public virtual string Name { get; set; }

        public virtual string Image { get; set; }

        public virtual string Link { get; set; }

        public virtual bool IsNew { get; set; }

        public virtual DateTime DateUpdated { get; set; }

    }

}
