using Common.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.Responses.Interfaces;
using Models.Responses;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System;
using Common.Enums;
using Common.Extensions;

namespace WebAPI.Middlewares
{
    public class ExceptionsMiddleware
    {
        #region Data members/Constants 

        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> logger;
        private readonly JsonSerializerOptions serializerOptions;

        #endregion

        #region Construction

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            this.logger = logger;
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            serializerOptions.Converters.Add(new JsonStringEnumConverter());
        }

        #endregion

        #region Methods

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            IApiResponse<object> resp = new ApiResponse<object>();
            var response = context.Response;
            response.ContentType = "application/json";

            if (exception is ArgumentException || exception is ArgumentNullException)
            {
                resp.HttpCode = StatusCodes.Status400BadRequest;
                resp.ErrorCode = ErrorCodes.BadRequest;
                resp.ErrorMessage = exception.Message;
            }
            else if (exception is UnauthorizedException)
            {
                resp.HttpCode = ((UnauthorizedException)exception).HttpStatusCode;
                resp.ErrorCode = ((UnauthorizedException)exception).ErrorCode.Value;
                resp.ErrorMessage = ((UnauthorizedException)exception).ErrorCode.Value.GetDescription();
            }
            else if (exception is ForbiddenException)
            {
                resp.HttpCode = ((ForbiddenException)exception).HttpStatusCode;
                resp.ErrorCode = ((ForbiddenException)exception).ErrorCode.Value;
                resp.ErrorMessage = ((ForbiddenException)exception).ErrorCode.Value.GetDescription();
            }
            else if (exception is BadRequestException)
            {
                resp.HttpCode = ((BadRequestException)exception).HttpStatusCode;
                resp.ErrorCode = ((BadRequestException)exception).ErrorCode.Value;
                resp.ErrorMessage = ((BadRequestException)exception).ErrorCode.Value.GetDescription();
            }
            else if (exception is NotFoundException)
            {
                resp.HttpCode = ((NotFoundException)exception).HttpStatusCode;
                resp.ErrorCode = ((NotFoundException)exception).ErrorCode.Value;
                resp.ErrorMessage = ((NotFoundException)exception).ErrorCode.Value.GetDescription();
            }
            else
            {
                var ex = exception as BaseException;
                if (ex != null)
                {
                    response.StatusCode = ex.HttpStatusCode;
                    resp.ErrorCode = ex.ErrorCode.Value;
                    resp.HttpCode = (int)ex.HttpStatusCode;
                }
                else
                {

                    response.StatusCode = StatusCodes.Status500InternalServerError;
                    resp.ErrorCode = ErrorCodes.InternalServerError;
                    resp.HttpCode = (int)StatusCodes.Status500InternalServerError;
                }
            }

            var body = JsonSerializer.Serialize(resp, serializerOptions);
            logger.LogError(exception.ToString());
            await response.WriteAsync(body);
        }

        #endregion
    }

    public static class CustomExceptionsMiddlewareExtensions
    {
        /// <summary>
        /// UseCustomExceptions() - Uses a middleware that handles exception thrown in code, and converts them to appropriate HTTP responses
        /// </summary>
        /// <param name="builder"></param>
        /// <returns>IApplicationBuilder</returns>
        public static IApplicationBuilder UseCustomExceptionsMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionsMiddleware>();
        }
    }
}
