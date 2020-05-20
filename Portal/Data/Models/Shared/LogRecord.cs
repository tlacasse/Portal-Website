using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Portal.Data.Models.Shared {

    [Table("LogRecord")]
    public class LogRecord {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Context { get; set; }

        public virtual string Message { get; set; }

        public virtual string Exception { get; set; }

    }

}
