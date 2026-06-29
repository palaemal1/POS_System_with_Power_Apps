//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Model.Common;
using Microsoft.Identity.Client;
namespace Model.Entities
{
    public class Employees:CommonCreated
    {
        [Key]
        public Guid EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Password { get; set; }
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public string? Status { get; set; }

        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Department { get; set; }
        public string? Position { get; set; }

    }
}
