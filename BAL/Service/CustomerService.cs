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
    internal class CustomerService : ICustomerService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly DataContent _content;
        public CustomerService(IUnitofWork unitofWork, DataContent content)
        {
            _unitofWork = unitofWork;
            _content = content;
        }
        public async Task<IEnumerable<Customers>> GetAllCustomer()
        {
            var data = await _unitofWork.Customer.GetAll();
            return data;
        }
        public async Task AddNewCustomer(AddCustomerDTO input)
        {
            var data = new Customers()
            {
                Name = input.Name,
                Email = input.Email,
                Phone = input.Phone,
                Address = input.Address,
                LoyaltyPoints = input.LoyaltyPoints,
                CreatedAt = input.createdDate,
                CreatedBy = input.createdBy,
                ActiveFlag = input.activeFlag
            };
            await _unitofWork.Customer.Add(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task UpdateCustomer(Guid id, UpdateCustomer input)
        {
            var data = (await _unitofWork.Customer.GetByCondition(x => x.CustomerId == id)).FirstOrDefault();
            if (data != null)
            {
                data.Name = input.name;
                data.Email = input.email;
                data.Phone = input.phone;
                data.Address = input.address;
                data.LoyaltyPoints = input.loyaltyPoints;
                data.UpdatedAt = input.updatedDate;
                data.UpdatedBy = input.updatedBy;
                data.ActiveFlag = input.activeFlag;
            }
            _unitofWork.Customer.Update(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task DeleteCustomer(Guid id, string updatedBy)
        {
            var data = (await _unitofWork.Customer.GetByCondition(x => x.CustomerId == id)).FirstOrDefault();
            if (data != null)
            {
                data.ActiveFlag = false;
                data.UpdatedAt = DateTime.UtcNow;
                data.UpdatedBy = updatedBy;
            }
            _unitofWork.Customer.Update(data);
            await _unitofWork.SaveChangesAsync();
        }
    }
}
