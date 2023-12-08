
using CIT.HelpDesk.Shared.Response;
using static CIT.HelpDesk.WebAPI.Exceptions.GlobalException;

#pragma warning disable
namespace CIT.HelpDesk.WebAPI.Middlewares
{
    public class GlobalExceptionMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private static Task HandleException(HttpContext context, Exception ex)
        {
            int statusCode = StatusCodes.Status500InternalServerError;
            switch (ex)
            {
                case NotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    break;
                case BadRequestException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
                case DivideByZeroException:
                    statusCode = StatusCodes.Status400BadRequest;
                    break;
            }
            var errorResponse = new Response
            {
                StatusCode = (statusCode),
                Message = ex.Message,
            };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(errorResponse.ToString());
        }

    }
    public static class ExceptionMiddlewareExtension
    {
        public static void ExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleWare>();
        }
    }
}
