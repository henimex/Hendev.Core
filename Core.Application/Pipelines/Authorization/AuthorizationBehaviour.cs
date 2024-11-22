using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        List<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (userRoleClaims == null || !userRoleClaims.Any())
            throw new AuthorizationException("You are not authenticated.");

        bool isAuthorized = userRoleClaims.Any(userRoleClaim =>
            userRoleClaim == GeneralOperationClaims.Admin ||
            request.Roles.Contains(userRoleClaim));

        if (!isAuthorized)
        {
            string requiredRolesMessage = string.Join(", ", request.Roles);
            throw new AuthorizationException($"You are not authorized. Required Claim(s): {requiredRolesMessage}");
        }

        TResponse response = await next();
        return response;
    }
}

#region SourceCode  

//Code Changed Because of IsNullOrEmpty() and required role info added to UnAuthorized message
//public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
//{
//    List<string>? userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

//    foreach (var item in request.Roles)
//    {
//        Console.WriteLine(item);
//    }

//    if (userRoleClaims == null)
//        throw new AuthorizationException("You are not authenticated.");

//    bool isNotMatchedAUserRoleClaimWithRequestRoles = userRoleClaims
//        .FirstOrDefault(
//            userRoleClaim => userRoleClaim == GeneralOperationClaims.Admin ||
//                             request.Roles.Any(role => role == userRoleClaim)).IsNullOrEmpty();

//    if (isNotMatchedAUserRoleClaimWithRequestRoles)
//        throw new AuthorizationException("You are not authorized.");

//    TResponse response = await next();
//    return response;
//}

#endregion