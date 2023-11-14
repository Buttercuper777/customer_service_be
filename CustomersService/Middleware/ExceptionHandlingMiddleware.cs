using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using CustomersService.Resources;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CustomersService.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadHttpRequestException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.BadRequest,
                    "Check your request: " + ex.Message
                );
            }
            catch (KeyNotFoundException ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.NotFound,
                    "Looks like the last query " +
                    "didn't yield any results. " + ex.Message
                );
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(
                    context,
                    HttpStatusCode.InternalServerError,
                    "Something went wrong: " + ex.Message
                );
            }
        }

        private async Task HandleExceptionAsync(
            HttpContext context,
            HttpStatusCode code,
            string message
        )
        {
            _logger.LogError(message);

            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            var errEx = new ErrorDto()
            {
                Message = message,
                StatusCode = (int)code
            };

            var result = JsonSerializer.Serialize(errEx);
            await response.WriteAsync(result);
        }
    }
}