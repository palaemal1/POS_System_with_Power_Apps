using Model.DTO;
using Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.IService
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customers>> GetAllCustomer();
        Task AddNewCustomer(AddCustomerDTO input);
        Task UpdateCustomer(Guid id, UpdateCustomer input);
        Task DeleteCustomer(Guid id, DeleteDTO request);
    }
}
