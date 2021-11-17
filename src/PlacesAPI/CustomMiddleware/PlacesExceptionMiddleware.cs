using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace PlacesAPI.CustomMiddleware
{
    public class PlacesExceptionMiddleware : IMiddleware
    {
        private readonly ILogger _logger;
        public PlacesExceptionMiddleware(ILogger<PlacesExceptionMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(Exception exception)
            {
                _logger.LogWarning($"\n{exception.Message} At stack trace: {exception.StackTrace}");
            }
        }
    }
}