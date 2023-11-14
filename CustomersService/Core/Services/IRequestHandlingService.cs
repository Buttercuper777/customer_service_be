using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersService.Core.Services
{
    public interface IRequestHandlingService
    { 
        Guid GettingFieldFromHeaders(HttpRequest request, string headerFieldName);
    }
}