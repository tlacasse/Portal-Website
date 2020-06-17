using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Banking {

    [Table("BankingAccount")]
    public class Account : IHaveDateUpdated {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        [Column("Name")]
        public virtual string Name { get; set; }

        [Column("AccountTypeId")]
        public virtual int AccountTypeId { get; set; }
        [ForeignKey("AccountTypeId")]
        public virtual AccountType AccountType { get; set; }

        [Column("DateUpdated")]
        public virtual DateTime DateUpdated { get; set; }

    }

}
