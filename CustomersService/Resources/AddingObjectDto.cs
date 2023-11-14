using System.Text.Json;

namespace CustomersService.Resources
{
    public class AddingObjectDto
    {
        public string Id { get; set; }

        public AddingObjectDto(string newId)
        {
            Id = newId;
        }
    }
}