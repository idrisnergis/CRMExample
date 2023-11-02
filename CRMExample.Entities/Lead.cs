using CRMExample.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMExample.Entities
{
    [Table("Leads")]
    public class Lead : EntityBase
    {
        [Required , StringLength(250)]
        public string Summary { get; set; }

        [StringLength(250)]
        public string Desc { get; set; }

        public decimal Price { get; set; }

        public LeadType Type { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ModifiedAt { get; set; }

        public int? UserId { get; set; }

        public int? ClientId { get; set; }

        public virtual User User { get; set; }

        public virtual Client Client { get; set; }

    }

     public enum LeadType
        {
            Waiting  = 0 ,
            Offered = 1 ,
            ToCall = 2,
            OnSale = 3,
            Completed = 4
        }
}
