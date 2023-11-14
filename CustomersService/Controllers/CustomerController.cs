using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomersService.Core.Services;
using CustomersService.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersService.Controllers
{
    public class CustomerController : BaseController<CustomerDto>
    {
        private readonly ICustomerService _customerService;

        public CustomerController(
            ICustomerService customerService,
            IRequestHandlingService requestHandlingService
        ) : base(requestHandlingService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            try
            {
                var customerId =
                    RequestHandlingService.GettingFieldFromHeaders(Request, CustomerHeaderFieldName);
                var res = await _customerService.GetByIdAsync(customerId);
                return res == null
                    ? throw new KeyNotFoundException($"Customer with ID: {customerId} not found!")
                    : Ok(res);
            }
            catch (BadHttpRequestException)
            {
                var res = await _customerService.GetAllAsync();
                return res == null ? throw new KeyNotFoundException("The list of registered users is empty") : Ok(res);
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Set([FromBody] CustomerDto customer)
        {
            var newCustomer = customer.GetCustomer();
            var res = await _customerService.AddAsync(newCustomer);
            
            return string.IsNullOrEmpty(res.ToString())
                ? throw new Exception(
                    "An unexpected error occurred when adding a new Customer"
                )
                : Ok(new AddingObjectDto(res.ToString()));
        }

        [HttpDelete]
        public override async Task<IActionResult> Delete()
        {
            var customerId =
                RequestHandlingService.GettingFieldFromHeaders(Request, CustomerHeaderFieldName);
            await _customerService.DeleteAsync(customerId);

            return Ok();
        }

        [HttpPut]
        public override async Task<IActionResult> Update([FromBody] CustomerDto nCustomer)
        {
            var customer = nCustomer.GetCustomer();
            var id = RequestHandlingService.GettingFieldFromHeaders(Request, CustomerHeaderFieldName);

            var editingCustomer = await _customerService.GetByIdAsync(id);
            if (editingCustomer == null) throw new KeyNotFoundException($"Customer with ID: {id} not found!");

            editingCustomer.Entrepreneur = customer.Entrepreneur;
            editingCustomer.Society = customer.Society;

            await _customerService.EditAsync(editingCustomer, id);
            return Ok();
        }
    }
}