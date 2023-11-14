using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersService.Core;
using CustomersService.Core.Models;
using CustomersService.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace CustomersService.Services
{
    public class RequisitesService : IRequisitesService
    {
        private readonly IDbRepository _dbRepository;
        private readonly ICustomerService _customerService;

        public RequisitesService(IDbRepository dbRepository, ICustomerService customerService)
        {
            _customerService = customerService;
            _dbRepository = dbRepository;
        }

        public async Task<List<Requisites>> GetByOwnerAsync(Guid ownerId)
        {
            return await _dbRepository.Get<Requisites>(
                x => x.Customer.Id == ownerId
            ).ToListAsync();
        }

        public async Task DeleteAsync(Guid requisitesId)
        {
            await _dbRepository.Delete<Requisites>(requisitesId);
            await _dbRepository.SaveChangesAsync();
        }

        public async Task<Guid> AddAsync(Requisites requisites, Guid ownerId)
        {
            var customer = await _customerService.GetByIdAsync(ownerId);
            
            if (customer == null)
                throw new KeyNotFoundException(
                    $"Customer with ID: {ownerId} not found!"
                );

            customer.Requisites.Add(requisites);
            await _dbRepository.Update(customer);
            await _dbRepository.SaveChangesAsync();

            return requisites.Id;
        }
    }
}