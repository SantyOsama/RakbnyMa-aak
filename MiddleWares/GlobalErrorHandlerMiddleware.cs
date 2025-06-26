using RakbnyMa_aak.GeneralResponse;

namespace RakbnyMa_aak.MiddleWares
{
    public class GlobalErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalErrorHandlerMiddleware> _logger;

        public GlobalErrorHandlerMiddleware(ILogger<GlobalErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var response = Response<string>.Fail($"Unexpected Error {ex.Message}", statusCode: 500);

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
