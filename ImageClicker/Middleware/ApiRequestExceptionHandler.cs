using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ImageClicker.Middleware
{
    public class ApiRequestExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ApiRequestExceptionHandler(RequestDelegate next, ILogger<ApiRequestExceptionHandler> logger, IHostingEnvironment hostingEnvironment)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));
        }

        public async Task Invoke(HttpContext context)
        {
            bool isApiRequest = false;
            Exception ex1 = null;

            // Pre-processing before calling the next middleware in the pipeline.
            string requestUrl = context.Request.Path;
            // ReSharper disable once UnusedVariable
            var requestQueryString = context.Request.QueryString;
            // ReSharper disable once UnusedVariable
            var requestQuery = context.Request.Query;
            if (requestUrl.Contains("/api/"))
            {
                // break point is Api request true.
                // Only log Web API requests and not request for static files.
                isApiRequest = true;
                // TODO Log request information prior to processing it.
            }
            else if (!requestUrl.Contains("/dist/") && !requestUrl.Contains("favicon.ico"))
            {
                // This is a browser refresh on a client side local route, redirect to home to reload the
                //   application and let the client Angular application internally route to the requested URL.
                context.Request.Path = "/home";
            }

            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next.Invoke(context);

                // Log Web API response information, or we let each type of request log its own response?
                if (isApiRequest)
                {
                    // TODO: Log response data.
                }
            }
            catch (Exception ex)
            {
                ex1 = ex;
            }

            if (ex1 != null)
            {
                _logger.LogError(0, ex1, $"An unhandled exception has occurred while executing the request: {requestUrl}.",
                    Array.Empty<object>());
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning(
                        "The response has already started, the error response will not be sent to the consumer application.",
                        Array.Empty<object>());
                }
                else
                {
                    try
                    {
                        context.Response.Clear();
                        context.Response.StatusCode = 500;

                        // Return extensive debug information only if in DEV environment.
                        if (_hostingEnvironment.IsDevelopment())
                        {
                            string shortStackTrace =
                                ex1.StackTrace.IndexOf("--- End of stack trace", StringComparison.Ordinal) > -1
                                    ? ex1.StackTrace.Substring(0,
                                        ex1.StackTrace.IndexOf("--- End of stack trace", StringComparison.Ordinal))
                                    : ex1.StackTrace;

                            JsonResponse(context, shortStackTrace);
                        }
                    }
                    catch (Exception ex2)
                    {
                        _logger.LogError(0, ex2,
                            "An exception was thrown attempting to return an error response to the consumer application.",
                            Array.Empty<object>());
                    }
                }
            }
        }

        public async void JsonResponse(HttpContext context, Object errResponse)
        {
            context.Response.ContentType = "application/json";
            string jsonData = JsonConvert.SerializeObject(errResponse);
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            await context.Response.Body.WriteAsync(data, 0, data.Length);
        }
    }
}
