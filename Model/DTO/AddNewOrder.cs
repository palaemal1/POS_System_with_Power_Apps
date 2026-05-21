using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class AddNewOrder
    {
        public Guid orderId { get; set; }
        public string? orderNumber { get; set; }
        public string? customerId { get; set; }
        public string? employeeId { get; set; }
        public decimal totalAmt { get; set; }
        public decimal discountAmt { get; set; }
        public decimal taxAmt { get; set; }

        public decimal netAmt { get; set; }
        public string? paymentStatus { get; set; }
        public string? orderStatus { get; set; }
        public bool? activeFlag { get; set; }
        public string? createdBy { get; set; }
        public DateTime createdDate { get; set; } = DateTime.UtcNow;
    }
}
