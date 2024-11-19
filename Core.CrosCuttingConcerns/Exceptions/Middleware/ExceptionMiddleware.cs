using Core.CrosCuttingConcerns.Exceptions.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.CrosCuttingConcerns.Exceptions.Middleware;

public class ExceptionMiddleware 
{
    private readonly RequestDelegate _next;
    private readonly ExceptionHandler _exceptionHandler;

    public ExceptionMiddleware(ExceptionHandler exceptionHandler, RequestDelegate next)
    {
        _exceptionHandler = exceptionHandler;
        _next = next;
    }
}
