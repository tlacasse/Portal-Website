using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Banking {

    //[Table("BankingSubcategory")]
    public class Subcategory : IHaveDateUpdated {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column("Id")]
        public virtual int Id { get; set; }

        //[Column("Name")]
        public virtual string Name { get; set; }

        //[Column("CategoryId")]
        public virtual int CategoryId { get; set; }
        //[ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //[Column("DateUpdated")]
        public virtual DateTime DateUpdated { get; set; }

    }

}
