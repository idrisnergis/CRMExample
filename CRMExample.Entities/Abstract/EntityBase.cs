using System.ComponentModel.DataAnnotations;

namespace CRMExample.Entities.Abstract
{
    public abstract class EntityBase
    {

        [Key]
        public int Id { get; set; }

    }
}