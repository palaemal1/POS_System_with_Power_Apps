using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IService
{
    public interface IOrderService
    {
        Task<IEnumerable<Orders>> GetAllOrder();
        Task<IEnumerable<DailySaleDTO>> GetDailySales();
        Task<IEnumerable<MonthlySaleReportDTO>> GetMonthlySales();
        Task<IEnumerable<WeeklySaleDTO>> GetWeeklySales();
        Task AddOrder(AddNewOrder input);
        Task UpdateOrder(Guid id, UpdateOrderDTO input);
        Task DeleteOrder(Guid id, DeleteDTO request);
    }
}
