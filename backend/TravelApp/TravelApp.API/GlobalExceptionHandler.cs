using System.Text.Json;
using TravelApp.BusinessLogic.Exceptions;

namespace TravelApp.API;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        
        var response = exception switch
        {
            EntityNotFoundException ex => new ErrorResponse{
                StatusCode = StatusCodes.Status404NotFound,
                Message = "Entity not found",
                Details = ex.Message
            },
            DuplicateEntityException ex => new ErrorResponse
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = "Entity already exists",
                Details = ex.Message  
            },
            InvalidFilterException ex => new ErrorResponse
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Invalid filter",
                Details = ex.Message
            },
            UserAlreadyExistsException ex => new ErrorResponse
            {
                StatusCode = StatusCodes.Status409Conflict,
                Message = "User already exists",
                Details = ex.Message
            },
            InvalidCredentialsException ex => new ErrorResponse
            {
                StatusCode = StatusCodes.Status401Unauthorized,
                Message = "Invalid credentials",
                Details = ex.Message
            },
            _ => new ErrorResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = "Internal server error",
                Details = exception.Message
            }
        };
        
        if (response.StatusCode >= 500)
        {
            _logger.LogError(exception, $"Server error occurred: {exception.Message}");
        }
        else
        {
            _logger.LogWarning(exception, $"Client error occurred: {exception.Message}");
        }

        context.Response.StatusCode = response.StatusCode;
        
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response, options));
    }
}

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
