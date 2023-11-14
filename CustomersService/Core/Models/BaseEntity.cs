using System;

namespace CustomersService.Core.Models
{
    public abstract class BaseEntity : IEntity
    {
        public virtual Guid Id { get; set; }
    }
}