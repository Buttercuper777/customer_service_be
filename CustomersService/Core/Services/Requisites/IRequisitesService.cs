using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersService.Core.Models;

namespace CustomersService.Core.Services
{
    public interface IRequisitesService
    {
        Task<List<Requisites>> GetByOwnerAsync(Guid ownerId);
        Task<Guid> AddAsync(Requisites requisites, Guid ownerId);
        Task DeleteAsync(Guid requisitesId);
    }
}