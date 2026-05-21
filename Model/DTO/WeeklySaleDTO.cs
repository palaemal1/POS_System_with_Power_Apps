using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DTO
{
    public class WeeklySaleDTO
    {
        public int SaleYear { get; set; }
        public int SaleWeek { get; set; } 
        public decimal NetSalesForWeek { get; set; }
        public int TotalOrders { get; set; }
    }
}
