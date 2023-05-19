using CRMExample.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CRMExample.Entities
{
    [Table("Issue")]
    public class Issue : EntityBase
    {
        [Required, StringLength(400)]
        public string Summary { get; set; }
        public DateTime? DueDate { get; set; }
        public bool Completed { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}
