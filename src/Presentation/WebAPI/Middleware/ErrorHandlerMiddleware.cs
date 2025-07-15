using ECommerce.Application.Commons.Response;
using ECommerce.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace ECommerce.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch ( Exception error ) 
            {

                var response = context.Response;

                var responseModel = new ApiExceptionResponse<string>()
                {
                    IsSucceeded = false,
                    Message = error?.Message
                };

                switch (error) 
                {
                    case ApiException:
                        response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);
                        break;


                    default:
                        response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

                        if (error.InnerException != null)
                            responseModel.Message += $" || {error.InnerException}";
                    break;

                }

                response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(responseModel);


                await response.WriteAsync(result);

            }
        }

    }
}
