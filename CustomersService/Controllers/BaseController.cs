using System.Threading.Tasks;
using CustomersService.Core.Models;
using CustomersService.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace CustomersService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : Controller
    {
        protected readonly IRequestHandlingService RequestHandlingService;
        protected const string CustomerHeaderFieldName = "X-User-Id";

        protected BaseController(IRequestHandlingService requestHandlingService)
        {
            RequestHandlingService = requestHandlingService;
        }
        
        public abstract Task<IActionResult> Set(T newData);
        public abstract Task<IActionResult> Update(T newData);
        public abstract Task<IActionResult> Get();
        public abstract Task<IActionResult> Delete();
    }
}