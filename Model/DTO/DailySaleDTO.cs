using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class DailySaleDTO
    {
        public DateTime saleDate { get; set; }
        public decimal netSaleDaily { get; set; }
        public int totalOrder { get; set; }
    }
}
