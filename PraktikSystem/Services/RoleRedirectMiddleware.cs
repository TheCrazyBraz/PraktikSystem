using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PraktikSystem.Services;

public class RoleRedirectMiddleware
{
    private readonly RequestDelegate _next;

    public RoleRedirectMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, UserService userService)
    {
        if (context.User.Identity.IsAuthenticated)
        {
            var username = context.User.Identity.Name;
            var user = userService.getUser(username);

            if (user != null)
            {
                // Check if the user has selected a role
                if (string.IsNullOrEmpty(user.Role))
                {
                    // Redirect to the role selection page if the user hasn't selected a role
                    context.Response.Redirect("/RoleSelection");
                    return;
                }

                // Check the user's role and redirect accordingly
                if (user.Role == "Praktikant")
                {
                    context.Response.Redirect("/PraktikantSite");
                    return;
                }
                else if (user.Role == "Admin")
                {
                    context.Response.Redirect("/AdminSite");
                    return;
                }
            }
        }

        // If no redirection is needed, continue with the request
        await _next(context);
    }
}

