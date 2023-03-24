using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CRMExaple.Entities
{
    [Table("Clients")]
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required,StringLength(60)]
        public string Name { get; set; }

        [Required/*Gerekli Boş Geçilemez*/, StringLength(60)]
        public string Email { get; set; }

        [StringLength(25)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
