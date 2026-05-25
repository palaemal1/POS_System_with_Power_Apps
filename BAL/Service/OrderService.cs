using BAL.IService;
using Microsoft.EntityFrameworkCore;
using Model;
using Model.DTO;
using Model.Entities;
using Repository.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    internal class OrderService:IOrderService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly DataContent _content;
        public OrderService(IUnitofWork unitofWork,DataContent content)
        {
            _unitofWork = unitofWork;
            _content = content;
        }

        public async Task<IEnumerable<Orders>> GetAllOrder()
        {
            var data = await _unitofWork.Order.GetAll();
            return data;
        }
        //public async Task AddOrder(AddNewOrder input)
        //{
        //    var data = new Orders()
        //    {
        //        OrderId=input.orderId,
        //        OrderNumber = input.orderNumber,
        //        CustomerId = input.customerId,
        //        EmployeeId = input.employeeId,
        //        TotalAmount = input.totalAmt ,
        //        DiscountAmount = input.discountAmt,
        //        TaxAmount = input.taxAmt,
        //        NetAmount = input.totalAmt -input.discountAmt + input.taxAmt,
        //        PaymentStatus = input.paymentStatus,
        //        OrderStatus=input.orderStatus,
        //        ActiveFlag = input.activeFlag,
        //        CreatedAt = input.createdDate,
        //        CreatedBy = input.createdBy
        //    };
        //    await _unitofWork.Order.Add(data);
        //    await _unitofWork.SaveChangesAsync();
        //}


        public async Task AddOrder(AddNewOrder input)
        {
            
            var lastOrderCount = await _content.Orders.CountAsync();
            var newOrderNumber = $"ORD-{(lastOrderCount + 1):D6}";
            var data = new Orders()
            {
                OrderId = input.orderId, 
                OrderNumber = newOrderNumber, 
                CustomerId = input.customerId,
                EmployeeId = input.employeeId,
                TotalAmount = input.totalAmt,
                DiscountAmount = input.discountAmt,
                TaxAmount = input.taxAmt,
                NetAmount = input.totalAmt - input.discountAmt + input.taxAmt,
                PaymentStatus = "Pending",
                OrderStatus = "Open",
                ActiveFlag = true,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = input.createdBy
            };

            await _unitofWork.Order.Add(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<DailySaleDTO>> GetDailySales()
        {
            var orders = await _unitofWork.Order.GetByCondition(x => x.OrderStatus == "Completed" && x.PaymentStatus == "Paid" && x.ActiveFlag == true);

            var dailySales = orders.GroupBy(o => o.CreatedAt.Value)
                                   .Select(g => new DailySaleDTO
                                   {
                                       saleDate = g.Key,
                                       netSaleDaily = g.Sum(o => o.NetAmount),
                                       totalOrder = g.Count()
                                   })
        .OrderByDescending(d => d.saleDate)
        .ToList();
        return dailySales;

        }

        public async Task<IEnumerable<MonthlySaleReportDTO>> GetMonthlySales()
        {
            var orders = await _unitofWork.Order.GetByCondition(x => x.OrderStatus == "Completed" && x.PaymentStatus == "Paid" && x.ActiveFlag == true && x.CreatedAt.HasValue);
            var monthlySale = orders.GroupBy(o => new
            {
                Year = o.CreatedAt.Value.Year,
                Month = o.CreatedAt.Value.Month
            })
                .Select(g => new MonthlySaleReportDTO
                {
                    saleYear = g.Key.Year,
                    saleMonth = g.Key.Month,
                    saleMonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM yyyy"),
                    netSaleMonth = g.Sum(o => o.NetAmount),
                    totalOrder = g.Count()
                })
                .OrderByDescending(d => d.saleYear)
                .ThenByDescending(d => d.saleMonth)
                .ToList();
            return monthlySale;
        }

        private int GetIso8601WeekOfYear(DateTime time)
        {
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public async Task<IEnumerable<WeeklySaleDTO>> GetWeeklySales()
        {
            var orders = await _unitofWork.Order.GetByCondition(x =>
          x.OrderStatus == "Completed" &&
          x.PaymentStatus == "Paid" &&
          x.ActiveFlag == true &&
          x.CreatedAt.HasValue
      );

            var weeklySales = orders

        .GroupBy(o => new {
            Year = o.CreatedAt.Value.Year,
            Week = GetIso8601WeekOfYear(o.CreatedAt.Value) 
        })

        .Select(g => new WeeklySaleDTO
        {
            SaleYear = g.Key.Year,
            SaleWeek = g.Key.Week,

            NetSalesForWeek = g.Sum(o => o.NetAmount),
            TotalOrders = g.Count()
        })
                   .OrderByDescending(d => d.SaleYear)
        .ThenByDescending(d => d.SaleWeek)
        .ToList();

            return weeklySales;
        }




        public async Task UpdateOrder(Guid id,UpdateOrderDTO input)
        {
            var data = (await _unitofWork.Order.GetByCondition(x => x.OrderId == id)).FirstOrDefault();
            if (data != null)
            {
                data.OrderNumber = input.orderNumber;
                data.CustomerId = input.customerId;
                data.EmployeeId = input.employeeId;
                data.TotalAmount = input.totalAmt;
                data.DiscountAmount = input.discountAmt;
                data.TaxAmount = input.taxAmt;
                data.PaymentStatus = input.paymentStatus;
                data.OrderStatus = input.orderStatus;
                data.NetAmount = input.totalAmt - input.discountAmt + input.taxAmt;
                data.ActiveFlag = input.activeFlag;
                data.UpdatedBy = input.updatedBy;
                data.UpdatedAt = input.updatedDate;
                
            }
             _unitofWork.Order.Update(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task DeleteOrder(Guid id, DeleteDTO request)
        {
            var data = (await _unitofWork.Order.GetByCondition(x => x.OrderId == id)).FirstOrDefault();
            if (data != null)
            {
                data.ActiveFlag = false;
                data.UpdatedAt = DateTime.UtcNow;
                data.UpdatedBy = request.updatedBy;
            }
             _unitofWork.Order.Update(data);
            await _unitofWork.SaveChangesAsync();
        }
    }
}
