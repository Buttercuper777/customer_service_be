using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomersService.Core.Models;
using CustomersService.Core.Services;
using CustomersService.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersService.Controllers
{
    public class RequisitesController : BaseController<Requisites>
    {
        private readonly IRequisitesService _requisites;

        public RequisitesController(IRequisitesService requisites, IRequestHandlingService requestHandlingService) :
            base(requestHandlingService)
        {
            _requisites = requisites;
        }

        [HttpPost]
        public override async Task<IActionResult> Set([FromBody] Requisites requisites)
        {
            var ownerId =
                RequestHandlingService.GettingFieldFromHeaders(Request, CustomerHeaderFieldName);
            var newRequisitesId = await _requisites.AddAsync(requisites, ownerId);
            
            return string.IsNullOrEmpty(newRequisitesId.ToString())
                ? throw new Exception("An unexpected error occurred " +
                                      "when adding a new requisites")
                : Ok(new AddingObjectDto(newRequisitesId.ToString()));
        }

        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var ownerId =
                RequestHandlingService.GettingFieldFromHeaders(Request, CustomerHeaderFieldName);
            var requisitesList = await _requisites.GetByOwnerAsync(ownerId);

            return !requisitesList.Any()
                ? throw new KeyNotFoundException("No records with payment data " +
                                                 $"were found for the user with the ID: {ownerId}")
                : Ok(requisitesList);
        }

        [HttpDelete]
        public override async Task<IActionResult> Delete()
        {
            var requisitesId =
                RequestHandlingService.GettingFieldFromHeaders(Request, "X-Requisites-Id");

            await _requisites.DeleteAsync(requisitesId);
            return Ok();
        }


        public override Task<IActionResult> Update(Requisites newData)
        {
            throw new BadHttpRequestException(
                "You cannot change your payment details. " +
                "Only adding new ones or deleting existing ones is available");
        }
    }
}