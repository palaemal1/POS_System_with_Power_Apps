using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class MonthlySaleReportDTO
    {
        public int saleYear { get; set; }
        public int saleMonth { get; set; }
        public string saleMonthName { get; set; }
        public decimal netSaleMonth { get; set; }
        public int totalOrder { get; set; }

    }
}
