using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class AddNewEmployeeDTO
    {
        // public Guid employeeId { get; set; }
        [Required]
        public string email{ get; set; } = string.Empty;
        [Required]
        public string password { get; set; } = string.Empty;
        [Required]
        public string role { get; set; } = string.Empty;
    }
}
