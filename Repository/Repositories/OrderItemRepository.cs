using Microsoft.EntityFrameworkCore;
using Model;
using Model.Entities;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderItemRepository : GenericRepository<OrderItems>, IOrderItemRepository
    {
        private readonly DataContent _content;
       
        public OrderItemRepository(DataContent context) : base(context) {
           
            _content = context;
        }

        public async Task<IEnumerable<object>> GetAllOrderItemList()
        {
            var orderItem = _content.OrderItems.Where(x => x.ActiveFlag == true);
            var order = _content.Orders.Where(x => x.ActiveFlag == true);
            var product = _content.Products.Where(x => x.ActiveFlag == true);
            var employee = _content.Employees.Where(x => x.ActiveFlag == true);
            var category = _content.Categories.Where(x => x.ActiveFlag == true);

            var orderItemList = await (from oIL in orderItem
                                       join p in product on oIL.ProductId equals p.ProductId.ToString()
                                       join c in category on p.CategoryId equals c.CategoryId.ToString()
                                       join o in order on oIL.OrderId equals o.OrderId.ToString()
                                       join e in employee on o.EmployeeId equals e.EmployeeId.ToString()
                                       select new
                                       {
                                           orderItemId=oIL.OrderItemId,
                                           orderId=oIL.OrderId,
                                           productId=oIL.ProductId,
                                           qty = oIL.Qty,
                                           lineTotal = oIL.LineTotal,
                                           productName = p.ProductName,
                                           unitCost=p.Cost,
                                           paymentStatus = o.PaymentStatus,
                                           employeeName = e.EmployeeName,
                                           orderStatus = o.OrderStatus,
                                           discount = oIL.Discount,
                                           unitPrice = oIL.UnitPrice,
                                           orderNumber = o.OrderNumber,
                                           taxAmt = o.TaxAmount,
                                           categoryName = c.CategoryName,
                                           createdBy = oIL.CreatedAt,
                                           updateAt = oIL.UpdatedAt,
                                           updatedBy = oIL.UpdatedBy,
                                           createdAt = oIL.CreatedAt
                                       }).ToListAsync();

            return orderItemList;
        }

        public async Task<IEnumerable<object>> GetAllOrderItemListWithPagination(int page, int pageSize)
        {
            //  var orderItems = await _unitofWork.OrderItem.GetAllAsyncWithPagination(page, pageSize);
            if (page < 1 || pageSize <= 0)
            {
                throw new ArgumentException("Page number must be greater than 0 and page size must be greater than 0.");
            }
            var orderItems = _content.OrderItems.Where(x => x.ActiveFlag == true);

            var totalCount = await orderItems.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            if (page > totalPages)
            {
                throw new ArgumentException($"Invalid page number. The page number should be between 1 and {totalPages}.");
            }

            
            var orderItem = orderItems
                .OrderByDescending(oi=>oi.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

           
            var product = _content.Products.Where(x => x.ActiveFlag == true);
            var order = _content.Orders.Where(x => x.ActiveFlag == true);          
            var employee = _content.Employees.Where(x => x.ActiveFlag == true);
            var category = _content.Categories.Where(x => x.ActiveFlag == true);
            var data =  (from oi in orderItem
                         join p in product on oi.ProductId equals p.ProductId.ToString()
                         join c in category on p.CategoryId equals c.CategoryId.ToString()
                         join o in order on oi.OrderId equals o.OrderId.ToString()
                         join e in employee on o.EmployeeId equals e.EmployeeId.ToString()
                         select new
                              {
                             qty = oi.Qty,
                             lineTotal = oi.LineTotal,
                             productName = p.ProductName,
                             paymentStatus = o.PaymentStatus,
                             employeeName = e.EmployeeName,
                             orderStatus = o.OrderStatus,
                             discount = oi.Discount,
                             unitPrice = oi.UnitPrice,
                             orderNumber = o.OrderNumber,
                             taxAmt = o.TaxAmount,
                             categoryName = c.CategoryName,
                             createdBy = oi.CreatedAt,
                             updateAt = oi.UpdatedAt,
                             updatedBy = oi.UpdatedBy,
                             createdAt = oi.CreatedAt

                         }).ToList();
            return data;
        }
    
    }
}
