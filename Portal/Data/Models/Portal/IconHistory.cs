using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Portal {

    [Table("PortalIconHistory")]
    public class IconHistory {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        [Column("IconId")]
        public virtual int IconId { get; set; }
        [ForeignKey("IconId")]
        public virtual Icon Icon { get; set; }

        [Column("Name")]
        public virtual string Name { get; set; }

        [Column("Image")]
        public virtual string Image { get; set; }

        [Column("Link")]
        public virtual string Link { get; set; }

        [Column("IsNew")]
        public virtual bool IsNew { get; set; }

        [Column("DateUpdated")]
        public virtual DateTime DateUpdated { get; set; }

    }

}
