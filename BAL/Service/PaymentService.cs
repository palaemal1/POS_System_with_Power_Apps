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
    internal class PaymentService:IPaymentService
    {
        private readonly IUnitofWork _unitofWork;
        private readonly DataContent _content;
        public PaymentService(IUnitofWork unitofWork,DataContent content)
        {
            _unitofWork = unitofWork;
            _content = content;
        }

        public async Task<Payments> GetPaymentById(Guid id)
        {
            var data = (await _unitofWork.Payment.GetByCondition(x=>x.PaymentId==id)).FirstOrDefault();
            return data;
        }
        public async Task AddNewPayment(AddNewPayment input)
        {
            var data = new Payments()
            {
                OrderId = input.orderId,
                PaymentMethod = input.paymentMethod,
                Amount = input.amount,
                PaymentDate = input.paymentDate,
                CreatedBy = input.createdBy,
                CreatedAt = input.createdDate,
                ActiveFlag = input.activeFlag
            };
            await _unitofWork.Payment.Add(data);
            await _unitofWork.SaveChangesAsync();
        }

        public async Task UpdatePaymentMethod(Guid id,UpdatePaymentDTO input)
        {
            var data = (await _unitofWork.Payment.GetByCondition(x => x.PaymentId == id)).FirstOrDefault();
            if (data != null)
            {
                data.Amount = input.amount;
                data.PaymentMethod = input.paymentMethod;
                data.PaymentDate = input.paymentDate;
                data.UpdatedBy = input.updatedBy;
                data.UpdatedAt = input.updatedAt;
            }
             _unitofWork.Payment.Update(data);
            await _unitofWork.SaveChangesAsync();

        }
    }
}
