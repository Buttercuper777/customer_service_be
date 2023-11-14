using System;

namespace CustomersService.Core.Models
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}