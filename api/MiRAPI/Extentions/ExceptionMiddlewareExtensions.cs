using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiRAPI.DataModels;
using MiRAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MiRAPI.Extentions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var errorId = Math.Abs(RandomGenerator.GenerateInt32);

                        var user = (User)context.Items[MiRConsts.USER_BAG];

                        logger.LogError(contextFeature.Error, $"API Error: {errorId}. Requested path: {context.Request.Path}. User {user.ID}:{user.Name}");

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            ErrorCode = (int)ErrorCodes.ServerError,
                            ServerCode = errorId,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
