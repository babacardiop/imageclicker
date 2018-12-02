using ImageClicker.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ImageClicker.Extensions
{

    /// <summary>
    /// IApplicationBuilder extension method to make it easy to setup
    ///    the custom API request exception handler middleware when the app starts.
    /// </summary>
    public static class InspectRequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiRequestExceptionHandler(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiRequestExceptionHandler>();
        }
    }

}
