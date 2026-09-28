using BAL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Repository.IUnitOfWork;

namespace POS.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController:ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUnitofWork _unitofWork;
        public PaymentController(IUnitofWork unitofWork,IPaymentService paymentService)
        {
            _unitofWork = unitofWork;
            _paymentService = paymentService;
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("GetPaymentById")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
           var data= await _paymentService.GetPaymentById(id);
            return Ok(new ResponseModel { Data = data });
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpPost("AddNewPayment")]
        public async Task<IActionResult> AddNewPayment(AddNewPayment input)
        {
            await _paymentService.AddNewPayment(input);
            return Ok("Add payment successfully.");
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpPost("UpdatePaymentMethod")]
        public async Task<IActionResult> UpdatePaymentMethod(Guid id,UpdatePaymentDTO input)
        {
            await _paymentService.UpdatePaymentMethod(id, input);
            return Ok("Update payment method successfully.");
        }
    }
}
