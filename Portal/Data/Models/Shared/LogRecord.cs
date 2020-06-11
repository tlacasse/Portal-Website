using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Shared {

    [Table("LogRecord")]
    public class LogRecord {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public virtual int Id { get; set; }

        [Column("Date")]
        public virtual DateTime Date { get; set; }

        [Column("Context")]
        public virtual string Context { get; set; }

        [Column("Message")]
        public virtual string Message { get; set; }

        [Column("Exception")]
        public virtual string Exception { get; set; }

    }

}
