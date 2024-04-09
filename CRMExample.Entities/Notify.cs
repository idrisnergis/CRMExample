using CRMExample.Entities.Abstract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMExample.Entities
{
            [Table("Notifies")]
            public class Notify : EntityBase
            {
                [Required, StringLength(400)]
                public string Text { get; set; }
                public int UserId { get; set; }
                public NotifyType Type { get; set; }
                public bool IsRead { get; set; }
                public DateTime CreatedAt { get; set; }

                public virtual User User { get; set; }
            }

            public enum NotifyType
            {
                IssueAdded = 0,
                IssueChanged = 1,
                IssueCompleted = 2,
                LeadAdded = 3,
                LeadChanged = 4,
                LeadCompleted = 5
        }
}
