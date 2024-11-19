using Core.CrosCuttingConcerns.Exceptions.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Core.CrosCuttingConcerns.Exceptions.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<ExceptionMiddleware>();
}
