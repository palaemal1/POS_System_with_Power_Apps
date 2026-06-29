using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class EmployeeLoginDTO
    {
        [Key]
        public Guid EmployeeId { get; set; } 

        [Required]
        public string Email { get; set; } = string.Empty; 

        [Required]
        public string Password { get; set; } = string.Empty; 

        [Required]
        public string Role { get; set; } = string.Empty; 

        public string? RefreshToken { get; set; } 

        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
