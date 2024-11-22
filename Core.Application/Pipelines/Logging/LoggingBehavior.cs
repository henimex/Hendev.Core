using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Serilog;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Core.Application.Pipelines.Logging;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ILogRequest
{
    private IHttpContextAccessor _httpContextAccessor;
    private readonly LoggerServiceBase _loggerServiceBase;

    public LoggingBehavior(IHttpContextAccessor httpContextAccessor, LoggerServiceBase loggerServiceBase)
    {
        _httpContextAccessor = httpContextAccessor;
        _loggerServiceBase = loggerServiceBase;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        List<LogParameter> logParameters = new()
        {
            new LogParameter
            {
                Name = request.GetType().Name,
                Type = _httpContextAccessor.HttpContext?.Request?.Method ?? "NoData",
                Value = request,
            }
        };

        LogDetail logDetail = new()
        {
            FullName = request.GetType().FullName ?? "NoData",
            MethodName = request.GetType().Name,
            Parameters = logParameters,
            User = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "NoData"
        };

        _loggerServiceBase.Info(JsonSerializer.Serialize(logDetail));
        return await next();
    }
}