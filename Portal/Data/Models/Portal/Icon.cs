﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Portal {

    [Table("PortalIcon")]
    public class Icon {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; } = -1;

        [Column("Name")]
        public virtual string Name { get; set; }

        [Column("Image")]
        public virtual string Image { get; set; }

        [Column("Link")]
        public virtual string Link { get; set; }

        [Column("DateCreated")]
        public virtual DateTime? DateCreated { get; set; }

        [Column("DateChanged")]
        public virtual DateTime? DateChanged { get; set; }

        public bool IsNew {
            get { return Id < 0; }
        }

        public IconHistory ToHistory() {
            return new IconHistory {
                IconId = Id,
                Icon = this,
                Name = Name,
                Image = Image,
                Link = Link,
                IsNew = IsNew,
                DateUpdated = DateTime.Now
            };
        }

    }

}
