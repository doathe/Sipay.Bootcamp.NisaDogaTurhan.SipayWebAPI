namespace SipayWebAPI.Middlewares;

public class GlobalLoggingHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalLoggingHandler> _logger;

    public GlobalLoggingHandler(RequestDelegate next, ILogger<GlobalLoggingHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Log the request
            _logger.LogInformation("Incoming request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await _next(context);

            // Log the action
            if (context.Response.StatusCode == 200)
            {
                _logger.LogInformation("Action {Method} {Path} executed successfully.", context.Request.Method, context.Request.Path);
            }
            else
            {
                _logger.LogWarning("Action {Method} {Path} executed with status code {StatusCode}.", context.Request.Method, context.Request.Path, context.Response.StatusCode);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
            // End the request and send the exception to global exception handler.
            throw;
        }
    }
}
