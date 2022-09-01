using AutoMapper;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;

namespace Customers.Application.Features
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public CustomersService(ICustomersRepository customersRepository,
            IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<List<Customer>> GetAllCustomersDetails()
        {
            return await _customersRepository.GetAllCustomersDetails();
        }

        public async Task<Customer> GetSpecificCustomerDetails(Guid customerId)
        {
            return await _customersRepository.GetCustomerDetails(customerId);
        }
    }
}

