using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersService.Core.Models;

namespace CustomersService.Core.Services
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAllAsync();
        Task<Customer> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Customer customer);
        Task DeleteAsync(Guid id);
        Task EditAsync(Customer customer, Guid id);
    }
}