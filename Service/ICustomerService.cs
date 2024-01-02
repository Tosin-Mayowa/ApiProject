using ApiProject.Model.Dto;

namespace ApiProject.Service
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllCustomer();
    }
}
