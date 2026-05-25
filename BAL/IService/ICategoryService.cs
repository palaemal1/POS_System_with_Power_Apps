using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IService
{
    public interface ICategoryService
    {
        Task<IEnumerable<Categories>> GetAllCategory();
        Task AddNewCategory(CategoryDTO input);
        Task UpdateCategory(Guid id, UpdateCategoryDTO input);
        Task DeleteCategory(Guid id, DeleteDTO request);
    }
}
