using System;

namespace CRMExpample.WebApp.Models
{
    public class ModalInputViewModel
    {
        public bool IsReadonly { get; set; } = false;
        public string Description { get; set; } = "M��teri Bilgileri Alan�";
        public bool HasId { get; set; } = false;
    }
}
