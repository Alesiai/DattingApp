using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using API.Errors;
using System.Text.Json;

namespace API.Middleware
{
    public class ExeptionMiddlewere
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExeptionMiddlewere> _logger;
        private readonly IHostEnvironment _env;



        public ExeptionMiddlewere(RequestDelegate next, ILogger<ExeptionMiddlewere> logger, IHostEnvironment env){
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex){
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                ? new ApiExetion(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                : new ApiExetion(context.Response.StatusCode, "Internal server error");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }
    }
}