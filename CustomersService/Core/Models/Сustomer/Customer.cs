using System;
using System.Collections.Generic;

namespace CustomersService.Core.Models
{
    public class Customer : BaseEntity
    {
        public DateTime RegistrationDate { get; set;  } = DateTime.Now;
        public string Tin { get; set;  }
        public string TinImagePath { get; set;  }
        public string LeaseContractImagePath { get; set; }
        public string UsrImagePath { get; set; }
        
        public Society Society { get; set; }
        public Entrepreneur Entrepreneur { get; set; }
        public List<Requisites> Requisites { get; set; }
        
        public Customer UpdateCustomerData(Customer newCustomer)
        {
            Tin = newCustomer.Tin;
            TinImagePath = newCustomer.TinImagePath;
            LeaseContractImagePath = newCustomer.LeaseContractImagePath;
            UsrImagePath = newCustomer.UsrImagePath;

            return this;
        }
        
    }
}