using ApiProject.Data;
using ApiProject.Model;
using ApiProject.Model.Dto;
using ApiProject.Service;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ApiProject.Container
{
    public class CustomerService:ICustomerService
    {
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public CustomerService(ApiContext context,IMapper mapper) 
        {
        _context = context;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> GetAllCustomer()
        {
            List<CustomerDto> resource = new List<CustomerDto>();
            var data= await _context.Customers.ToListAsync();
            resource=_mapper.Map<List<Customer>,List<CustomerDto>>(data);
            return resource;
        }
    }
}
