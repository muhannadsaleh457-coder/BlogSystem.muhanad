using BlogSystem.muhanad.Domain.Exceptions.BadRequest;
using BlogSystem.muhanad.Domain.Exceptions.NotFound;
using BlogSystem.muhanad.Domain.Exceptions.UnuAuthrize;
using BlogSystem.muhanad.Shared.Dtos.Exceptions;

namespace BlogSystem.muhanad.Web.Middlewares
{
    public class ExceptionMiddleware(RequestDelegate  _next)
    {
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next(context);


            }catch (Exception ex)
            {

                context.Response.StatusCode = ex switch
                {
                    BadRequestException => StatusCodes.Status400BadRequest,
                    NotFoundException => StatusCodes.Status404NotFound,
                    UnAuthrizeException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError
                };

                var response = new ExceptionResponse()
                {
                    message = ex.Message,
                    statusCode = context.Response.StatusCode
                };

                await context.Response.WriteAsJsonAsync(response);
                

            }
          
        }
    }
}
