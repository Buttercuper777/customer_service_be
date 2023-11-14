using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersService.Core;
using CustomersService.Core.Models;
using CustomersService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomersService.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDbRepository _repository;

        public CustomerService(IDbRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<List<Customer>> GetAllAsync()
        {
            return await _repository.GetAll<Customer>()
                .Include(x => x.Entrepreneur)
                .Include(x => x.Society)
                .Include(x => x.Requisites)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _repository.Get<Customer>(x => x.Id == id)
                .Include(x => x.Entrepreneur)
                .Include(x => x.Society)
                .Include(x => x.Requisites)
                .FirstOrDefaultAsync();
        }

        public async Task<Guid> AddAsync(Customer customer)
        {
            var newCustomer = await _repository.Add(customer);
            await _repository.SaveChangesAsync();
            return newCustomer;
        }

        public async Task DeleteAsync(Guid id)
        {
            await _repository.Delete<Customer>(id);
            await _repository.SaveChangesAsync();
        }

        public async Task EditAsync(Customer customer, Guid id)
        {
            customer.Id = id;
            await _repository.Update(customer);
            await _repository.SaveChangesAsync();
        }
    }
}