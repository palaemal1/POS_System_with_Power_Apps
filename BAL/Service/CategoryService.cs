using BAL.IService;
using Model;
using Model.DTO;
using Model.Entities;
using Repository.IUnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Service
{
    internal class CategoryService:ICategoryService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly DataContent _context;
        public CategoryService(IUnitofWork unitofWork,DataContent context)
        {
            _unitofWork = unitofWork;
            _context = context;
        }

        public async Task<IEnumerable<Categories>> GetAllCategory()
        {
            var data = await _unitofWork.Category.GetAll();
            return data;
        }

        public async Task AddNewCategory(CategoryDTO input)
        {
            var data = new Categories()
            {
                CategoryName = input.categoryName,
                Description = input.description,
                CategoryCode = input.categoryCode, 
                ActiveFlag=input.activeFlag, 
                CreatedAt=input.createdDate,
                CreatedBy=input.createdBy

            };
            await _unitofWork.Category.Add(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task UpdateCategory(Guid id, UpdateCategoryDTO input)
        {
            var data = (await _unitofWork.Category.GetByCondition(x => x.CategoryId == id)).FirstOrDefault();
            if (data != null)
            {
                data.CategoryName = input.categoryName;
                data.CategoryCode = input.categoryCode;
                data.Description = input.description;
                data.UpdatedAt = input.updatedDate;
                data.ActiveFlag = input.activeFlag;
                data.UpdatedBy = input.updatedBy;

            }
            _unitofWork.Category.Update(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task DeleteCategory(Guid id,string updatedBy )
        {
            var data = (await _unitofWork.Category.GetByCondition(x => x.CategoryId == id)).FirstOrDefault();
            if (data != null)
            {
                data.ActiveFlag = false;
                data.UpdatedBy = updatedBy;
                data.UpdatedAt = DateTime.UtcNow;
            }

             _unitofWork.Category.Update(data);
            await _unitofWork.SaveChangesAsync();
        }
    }
}
