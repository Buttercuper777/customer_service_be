using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CustomersService.Core.Models
{
    public class Entrepreneur : BaseEntity
    {
        [ForeignKey("CustomerId")] public Guid CustomerId { get; set; }
        [JsonIgnore] public Customer Customer { get; set; }
        public string Epsrn { get; set; }
        public string EpsrnImagePath { get; set; }
    }
}