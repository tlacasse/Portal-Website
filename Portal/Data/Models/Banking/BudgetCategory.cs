using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Banking {

    /*[Table("BankingBudgetCategory")]
    public class BudgetCategory {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        [Column("BudgetId")]
        public virtual int BudgetId { get; set; }
        [ForeignKey("BudgetId")]
        public virtual Budget Budget { get; set; }

        [Column("CategoryId")]
        public virtual int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [Column("AllocatedAmount")]
        public virtual decimal AllocatedAmount { get; set; }

        [Column("ActualAmount")]
        public virtual decimal ActualAmount { get; set; }

    }*/

}
