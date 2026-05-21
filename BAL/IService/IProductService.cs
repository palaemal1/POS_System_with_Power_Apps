using BAL.Service;
using Model.Entities;
using Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IService
{
    public interface IProductService
    {

        
        Task<IEnumerable<Products>> GetAllProduct();
       // Task<IEnumerable<Product>> GetProductWithActiveFlag();
        Task AddProduct(AddProductDTO input);
        Task UpdateProduct(Guid id, UpdateProductDTO inputModel);
        Task DeleteProduct(Guid id, string updatedBy);
        Task<IEnumerable<Products>> GetByProductName(string productName);
        Task<IEnumerable<object>> DisplayProduct();

    }
}
