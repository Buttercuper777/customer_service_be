using System.Text.Json;

namespace CustomersService.Resources
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}