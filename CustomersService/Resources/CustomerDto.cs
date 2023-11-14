using CustomersService.Core.Models;

namespace CustomersService.Resources
{
    public class CustomerDto 
    {
        public string Tin { get; set; }
        public string TinImagePath { get; set;  }
        public string LeaseContractImagePath { get; set; }
        public string UsrImagePath { get; set; }
        public Entrepreneur Entrepreneur { get; set; }
        public Society Society { get; set; }

        
        public Customer GetCustomer()
        {
            var newCustomer = new Customer
            {
                Tin = Tin,
                TinImagePath = TinImagePath,
                LeaseContractImagePath = LeaseContractImagePath,
                UsrImagePath = UsrImagePath,
                
                Entrepreneur = Entrepreneur,
                Society = Society,
            };

            return newCustomer;
        }
        
        
    }
}