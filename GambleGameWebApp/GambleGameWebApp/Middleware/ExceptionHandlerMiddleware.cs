using System.Net;

namespace WebApp.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                context.Request.EnableBuffering();

                await _next(context);
            }
            catch (Exception exception)
            {
                //log
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }

}