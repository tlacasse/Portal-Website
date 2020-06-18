using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Banking {

    /*[Table("BankingPurchase")]
    public class Purchase {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        [Column("AccountId")]
        public virtual int AccountId { get; set; }
        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }

        [Column("SubcategoryId")]
        public virtual int SubcategoryId { get; set; }
        [ForeignKey("SubcategoryId")]
        public virtual Subcategory Subcategory { get; set; }

        [Column("Description")]
        public virtual string Description { get; set; }

        [Column("Amount")]
        public virtual decimal Amount { get; set; }

        [Column("Date")]
        public virtual DateTime Date { get; set; }

        public Category Category {
            get {
                return Subcategory.Category;
            }
        }

    }*/

}
