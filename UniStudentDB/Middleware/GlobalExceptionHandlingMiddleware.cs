using System.Net;
using System.Text.Json;
using UniStudentDB.Core.Models;

namespace UniStudentDB.Middlewares
{
    // Implementing IMiddleware interface
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                // All requests will pass through here
                await next(context);
            }
            catch (Exception ex)
            {
                // If any exception occurs anywhere in the app, it will be caught here
                _logger.LogError(ex, ex.Message); // Log the error for developers

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; // 500

            // Creating BaseResponse consistent with the project structure
            var response = new BaseResponse
            {
                Success = false,
                Message = "Internal Server Error. Please try again later.",
                // For security, actual error details should only be shown in Development mode.
                // However, for learning purposes, we are exposing the exception message here.
                Errors = new List<string> { exception.Message }
            };

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}