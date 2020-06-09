using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Portal {

    [Table("PortalIconPosition")]
    public class IconPosition {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        [Column("IconId")]
        public virtual int IconId { get; set; }
        [ForeignKey("IconId")]
        public virtual Icon Icon { get; set; }

        [Column("XCoord")]
        public virtual int XCoord { get; set; }

        [Column("YCoord")]
        public virtual int YCoord { get; set; }

        [Column("DateUpdated")]
        public virtual DateTime DateUpdated { get; set; }

        [Column("IsActive")]
        public virtual bool IsActive { get; set; }

    }

}
