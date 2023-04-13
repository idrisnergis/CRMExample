using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CRMExample.Entities.Abstract;

namespace CRMExample.Entities
{
    [Table("Users")]
    public class User : EntityBase
    {

        [Required, StringLength(60)]
        public string Name { get; set; }

        [Required/*Gerekli Boş Geçilemez*/, StringLength(60)]
        public string Email { get; set; }

        [Required, StringLength(25)]
        public string Username { get; set; }

        [Required, StringLength(500)]
        public string Password { get; set; }

        public string Role { get; set; }

        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
