using CRMExample.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMExample.Entities
{
    [Table("Logs")]
    public class Log : EntityBase
    {
        [Required , StringLength(200)]
        public string Text { get; set; }

        public int? UserId { get; set; }

        public LogType Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual User User { get; set; }
    }

    public enum LogType
    {
        General = 0 ,
        SystemEntry = 1
    }
}
