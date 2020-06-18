using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Banking {

    //[Table("BankingAccountType")]
    public class AccountType : IHaveDateUpdated {

        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        //[Column("Id")]
        public virtual int Id { get; set; }

        //[Column("Name")]
        public virtual string Name { get; set; }

       // [Column("DateUpdated")]
        public virtual DateTime DateUpdated { get; set; }

    }

}
