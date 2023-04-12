using System;
using System.ComponentModel.DataAnnotations;

namespace CRMExample.Model
{
    public class CreateCustomerModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public bool IsCorporate { get; set; }
        public bool Locked { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}