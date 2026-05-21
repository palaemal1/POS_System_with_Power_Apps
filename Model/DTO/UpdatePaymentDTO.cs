using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UpdatePaymentDTO
    {
        public decimal amount { get; set; }
        public string? paymentMethod { get; set; }
        public DateTime paymentDate { get; set; }
        public string? updatedBy { get; set; }
        public DateTime updatedAt { get; set; }
    }
}
