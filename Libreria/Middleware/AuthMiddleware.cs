
using Libreria.Models;

namespace Libreria.Middleware;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var authEmail = context.Request.Cookies["auth"];
        if (authEmail is not null)
        {
            Utente? user = (Utente?)DAOUtente.GetInstance().Find(authEmail);
            context.Items["AuthUser"] = user;
        }

        await _next(context);
    }
}

public static class RequestCultureMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthMiddleware(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}