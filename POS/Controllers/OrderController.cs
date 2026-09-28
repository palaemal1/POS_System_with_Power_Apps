using BAL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Model.Entities;
using Repository.IUnitOfWork;

namespace POS.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController:ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly IOrderService _orderService;
        public OrderController(IUnitofWork unitofWork,IOrderService orderService)
        {
            _unitofWork = unitofWork;
            _orderService = orderService;
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("GetOrder")]
        public async Task<IActionResult> GetOrder()
        {
           var data= await _orderService.GetAllOrder();
            return Ok(new ResponseModel { Data = data });
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("DailySaleOrderReport")]
        public async Task<IActionResult> DailySaleOrderReport()
        {
            var data = await _orderService.GetDailySales();
            return Ok(new ResponseModel { Data = data });
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("MonthlySaleOrderReport")]
        public async Task<IActionResult> MonthlySaleOrderReport()
        {


            var data = await _orderService.GetMonthlySales();
            return Ok(new ResponseModel { Data = data });
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("WeeklySaleOrderReport")]
        public async Task<IActionResult> WeeklySaleOrderReport()
        {
            var data = await _orderService.GetWeeklySales();
            return Ok(new ResponseModel { Data = data });
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpPost("NewOrder")]
        public async Task<IActionResult> newOrder(AddNewOrder input)
        {

            await _orderService.AddOrder(input);
            return Ok("Order successfully");
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpPost("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder(Guid id, UpdateOrderDTO input)
        {
            await _orderService.UpdateOrder(id, input);
            return Ok("Update successfully");
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpPatch("DeleteOrder/{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id, DeleteDTO request)
        {
            await _orderService.DeleteOrder(id,request);
            return Ok("Delete successfully");
        }
    }
}
