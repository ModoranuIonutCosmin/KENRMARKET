using AutoMapper;
using Customers.Application.Interfaces;
using Customers.Domain.Entities;
using Customers.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Customers.Application.Features;

public class CustomersService : ICustomersService
{
    private readonly ICustomersRepository      _customersRepository;
    private readonly IUnitOfWork               _unitOfWork;
    private readonly ILogger<CustomersService> _logger;
    private readonly IMapper                   _mapper;

    public CustomersService(ICustomersRepository customersRepository,
        IUnitOfWork unitOfWork,
        ILogger<CustomersService> logger,
        IMapper mapper)
    {
        _customersRepository = customersRepository;
        _unitOfWork          = unitOfWork;
        _logger              = logger;
        _mapper              = mapper;
    }

    public async Task<Customer> RegisterNewCustomer(Guid id,
        string firstName,
        string lastName,
        string? middleName,
        string userName,
        string email,
        string? phoneNumber,
        DateTimeOffset? birthDate,
        Address? address)
    {
        _logger.LogInformation("[Customer service] Creating a new customer entry with email={@email}, username={@userName}",
                               email, userName);

        var alreadyExistentCustomer = await
            _customersRepository.GetAllWhereAsync(c => c.Email.Equals(email) || c.UserName.Equals(userName));

        if (alreadyExistentCustomer.Any())
        {
            _logger.LogInformation("[Customer service] Couldn't create a new customer entry with email={@email}, username={@userName}, user already exists!",
                                   email, userName);

            throw new CustomerAlreadyExistsException("Customer already exists!");
        }

        var result = await _customersRepository.AddAsync(new Customer()
                                                         {
                                                             Id        = id,
                                                             FirstName = firstName,
                                                             LastName  = lastName,
                                                             UserName  = userName,
                                                             Email     = email,
                                                         });

        await _unitOfWork.CommitTransaction();

        _logger.LogInformation("[Customer service] Created a new customer entry with email={@email}, username={@userName}",
                               email, userName);

        return result;
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