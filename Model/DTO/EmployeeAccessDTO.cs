using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class EmployeeAccessDTO
    {
       
        public required string employeeId { get; set; }
        public required string employeeName { get; set; }
        public required string role { get; set; }
    }
}
