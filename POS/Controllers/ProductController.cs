using BAL.IService;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Repository;
using Repository.IUnitOfWork;
using System.Net.NetworkInformation;

namespace Retail_API_.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        //public IActionResult Index()
        //{
        //    return View();
        //}
        private readonly IUnitofWork _uintOfWork;
        private readonly IProductService _ProductService;
        public ProductController(IUnitofWork unitOfWork, IProductService ProductService)
        {
            _uintOfWork = unitOfWork;
            _ProductService = ProductService;
        }

        [HttpGet("GetAllProduct")]
        public async Task<IActionResult> GetProducts()
        {
            var data = await _ProductService.GetAllProduct();
            return Ok(new ResponseModel { Data = data });

        }

        [HttpPost("GetProductByName")]
        public async Task<IActionResult> GetProductByName(string productName)
        {
            var data = await _ProductService.GetByProductName(productName);
            return Ok(new ResponseModel { Data = data });
        }

        [HttpGet("DisplayProductList")]
        public async Task<IActionResult> DisplayProduct()
        {
            var data = await _ProductService.DisplayProduct();
            return Ok(new ResponseModel { Data = data });
        }
        

        [HttpPost("AddNewProduct")]
        public async Task<IActionResult> AddProduct(AddProductDTO input)
        {

            await _ProductService.AddProduct(input);
            return Ok("Ok");

        }

        [HttpPost("Update/Product")]
        public async Task<IActionResult> UpdateProduct(Guid id,UpdateProductDTO input) 
        {
            await _ProductService.UpdateProduct(id, input);
            return Ok("Update successfully");
        }

        [HttpGet("Delete/Product")]
        public async Task<IActionResult> DeleteProduct(Guid id, string updatedBy)
        {
            await _ProductService.DeleteProduct(id,updatedBy);
            return Ok("Delete successfully.");
        }

       
    }
}
