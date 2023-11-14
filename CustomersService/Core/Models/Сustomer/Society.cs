using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CustomersService.Core.Models
{
    public class Society : BaseEntity
    {
        [ForeignKey("CustomerId")] public Guid CustomerId { get; set; }
        [JsonIgnore] public Customer Customer { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public string Psrn { get; set; }
        public string PsrnImagePath { get; set; }
    }
}