using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRMExample.Entities.Abstract;

namespace CRMExample.Entities
{

    [Table("Clients")]
    public class Client : EntityBase
    {

        [Required, StringLength(60)]
        public string Name { get; set; }

        [Required/*Gerekli Boş Geçilemez*/, StringLength(60)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool IsCorporate { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
