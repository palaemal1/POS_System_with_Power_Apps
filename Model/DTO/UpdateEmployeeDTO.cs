using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class UpdateEmployeeDTO
    {
        public string? employeeName { get; set; }
        public string? password { get; set; }
        public string? fullName { get; set; }
        public string? role { get; set; }
        public string? status { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? department { get; set; }
        public string? position { get; set; }
        public bool? activeFlag { get; set; }
        public string? updatedBy { get; set; }
        public DateTime updatedDate { get; set; } = DateTime.UtcNow;
    }

    //public class DeleteEmployeeDTO
    //{
    //    public bool? activeFlag { get; set; }
    //    public string? updateBy { get; set; }
    //    public DateTime updatedAt { get; set; } = DateTime.UtcNow;
    //}
}
