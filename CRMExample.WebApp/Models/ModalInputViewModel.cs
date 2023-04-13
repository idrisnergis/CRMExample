using System;

namespace CRMExpample.WebApp.Models
{
    public class ModalInputViewModel
    {
        public bool IsReadonly { get; set; } = false;
        public string Description { get; set; } = "Müþteri Bilgileri Alaný";
        public bool HasId { get; set; } = false;
    }
}
