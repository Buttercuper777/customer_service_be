using System;
using CustomersService.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomersService.Services
{
    public class RequestHandlingService : IRequestHandlingService
    {
        public Guid GettingFieldFromHeaders(HttpRequest request, string headerFieldName)
        {
            return request.Headers.TryGetValue(headerFieldName, out var userId)
                ? new Guid(userId)
                : throw new BadHttpRequestException($"Header ${headerFieldName} not found");
        }
    }
}