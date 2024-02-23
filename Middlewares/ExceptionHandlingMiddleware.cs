using SorokChatServer.Exceptions;
using SorokChatServer.Models;
using System.Net;

namespace SorokChatServer.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (NotFoundException exception)
            {
                await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.BadRequest);
            }
            catch (AlreadyExistsException exception)
            {
                await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.BadRequest);
            }
            catch (PasswordException exception)
            {
                await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.BadRequest);
            }
            catch(UnauthorizationException exception)
            {
                await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.Unauthorized);
            }
            catch (ServerException exception)
            {
                await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.InternalServerError);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception.Message, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)statusCode;
            ErrorModel result = new ErrorModel(message, statusCode);
            await response.WriteAsJsonAsync(result);
        }
    }
}
