using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IService
{
    public  interface IOrderItemService
    {
        Task AddOrderItem(AddNewOrderItem input);
        Task<IEnumerable<OrderItems>> GetAllOrderItem();
        Task updateOrderItem(Guid id, UpdateOrderItemDTO input);
        Task DeleteOrderItem(Guid id, string updatedBy);

        Task<IEnumerable<object>> GetAllOrderItemList();
        Task<IEnumerable<OrderItems>> GetOrderItemListById(string id);
        Task AddMultipleOrderItem(IEnumerable<AddNewOrderItem> inputs);
        Task<IEnumerable<OrderItems>> GetOrderItemWithPagination(int page, int pageSize);
        Task<IEnumerable<OrderItems>> GetOrderItemsWithPagination(
            int page,
            int pageSize,
            bool descending = false);
        Task<IEnumerable<OrderItems>> GetOrderItemsWithPaginationDesc(int page, int pageSize, string columnName);

        Task<IEnumerable<object>> GerOrderItemListWithPagination(int page, int pageSize);
    }
}
