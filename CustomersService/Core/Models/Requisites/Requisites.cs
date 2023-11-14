using System.Text.Json.Serialization;

namespace CustomersService.Core.Models
{
    public class Requisites : BaseEntity
    {
        [JsonIgnore] public Customer Customer { get; set; }
        public string CodeOfBank { get; set; }
        public string PaymentNumber { get; set; }
        
        // public Requisites AddRequisitesData(Requisites requisites)
        // {
        //     Customer.Requisites = 
        //     return this;
        // }
        
    }
    

}