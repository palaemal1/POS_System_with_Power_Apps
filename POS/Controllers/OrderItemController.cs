using BAL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Model.Entities;
using Repository.IUnitOfWork;
using System.Linq.Expressions;


namespace POS.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController:ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IOrderItemService _orderItemService;
        public OrderItemController(IUnitofWork unitofWork,IOrderItemService orderItemService)
        {
            _unitofWork = unitofWork;
            _orderItemService = orderItemService;
        }

        
        [Authorize(Roles ="Admin")]
        [HttpGet("GetAllOrderItem")]
        public async Task<IActionResult> GetOrderItem()
        {
            var data = await _orderItemService.GetAllOrderItem();
            return Ok(new ResponseModel { Data = data });
        }

        
        [HttpGet("GetOrderItemWithPagination")]
        public async Task<IActionResult> GetOrderItemWithPagination(int page,int pageSize)
        {
            var data = await _orderItemService.GetOrderItemWithPagination(page, pageSize);
            return Ok(new ResponseModel { Data = data });
        }

        [HttpPost("GetOrderItemListById")]
        public async Task<IActionResult> GetOrderItemListById(string id)
        {
            var data = await _orderItemService.GetOrderItemListById(id);
            return Ok(new ResponseModel { Data = data });
        }

        [HttpGet("GetOrderItemsWithPagination")]
        public async Task<IActionResult> GetOrderItemsWithPagination(
            int page,
            int pageSize,
            bool descending = false) 
        {
            var data = await _orderItemService.GetOrderItemsWithPagination(page, pageSize, descending);
            return Ok(new ResponseModel { Data = data });
        }

        [HttpGet("GetOrderItemListWithPagination")]
        public async Task<IActionResult> GetOrderItemListWithPagination(int page, int pageSize)
        {
            var data = await _orderItemService.GerOrderItemListWithPagination(page, pageSize);
            return Ok(new ResponseModel { Data = data });
        }

        [HttpGet("GetOrderItemsWithPaginationDesc")]
        public async Task<IActionResult> GetOrderItemsWithPaginationDesc(int page,int pageSize,string columnName)
        {
            var data = await _orderItemService.GetOrderItemsWithPaginationDesc(page, pageSize, columnName);
            return Ok(new ResponseModel { Data = data });
        }

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem(AddNewOrderItem input)
        {
            await _orderItemService.AddOrderItem(input);
            return Ok("Add order item successfully");
        }

        [HttpPost("UpdateOrderItem")]
        public async Task<IActionResult> UpdateOrderItem(Guid id,UpdateOrderItemDTO input)
        {
            await _orderItemService.updateOrderItem(id, input);
            return Ok("Update order item successfully");
        }

        [HttpPatch("DeleteOrderItem/{id}")]
        public async Task<IActionResult> DeleteOrderItem(Guid id, DeleteDTO request)
        {
            await _orderItemService.DeleteOrderItem(id,request);
            return Ok("Delete order item successfully");
        }

        [HttpGet("GetAllOrderItemList")]
        public async Task<IActionResult> GetAllOrderItemList()
        {
            var data = await _orderItemService.GetAllOrderItemList();
            return Ok(new ResponseModel { Data = data });
        }

        [HttpPost("AddMultipleOrderItem")]
        public async Task<IActionResult> AddMultipleOrderItem(IEnumerable<AddNewOrderItem> input)
        {
            await _orderItemService.AddMultipleOrderItem(input);
            return Ok("Add multiple order item successfully");
        }
    }
}
