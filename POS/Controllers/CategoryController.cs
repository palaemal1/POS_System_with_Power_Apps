using BAL.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DTO;
using Repository.IUnitOfWork;
using System.Security.AccessControl;

namespace POS.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly IUnitofWork _unitofWork;
        private readonly ICategoryService _categoryService;
        public CategoryController(IUnitofWork unitofWork,ICategoryService categoryService)
        {
            _unitofWork = unitofWork;
            _categoryService = categoryService;
        }

        [Authorize(Roles = "Admin,Manager,Cashier")]
        [HttpGet("GetAllCategory")]
        public async Task<IActionResult> GetCategories()
        {
            var data = await _categoryService.GetAllCategory();
            return Ok(new ResponseModel { Data = data });

        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("AddNewCategory")]
        public async Task<IActionResult> AddCategory(CategoryDTO input)
        {
            await _categoryService.AddNewCategory(input);
            return Ok("Add new Category");
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Guid id,UpdateCategoryDTO input)
        {
            await _categoryService.UpdateCategory(id, input);
            return Ok("Update successfully");
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPatch("DeleteCategory/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id, DeleteDTO request)
        {
            await _categoryService.DeleteCategory(id,request);
            return Ok("Delete Successfully");
        }
    }
}
