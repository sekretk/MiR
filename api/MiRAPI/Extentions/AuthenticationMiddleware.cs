using DataModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using MiRAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiRAPI.Extentions
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<AuthenticationMiddleware> logger)
        {
            if (context.Request.Method == "OPTIONS")
            {
                await _next.Invoke(context);
                return;
            }

            if (context.Request.GetEncodedUrl().EndsWith("startpage"))
            {
                var response = context.Response;
                response.Redirect(@"http://localhost:8080/");
                return;
            }

            string authHeader = context.Request.Headers[MiRConsts.Authorization];

            if (authHeader != null)
            {               

                if (!AppState.Tokens.ContainsKey(authHeader))
                {
                    logger.LogError($"Request for {context.Request.Path} has incorrect auth token {authHeader} ");

                    await GenerateAuthResponse(context, ErrorCodes.IncorrectToken);
                }
                else if (AppState.Tokens[authHeader].ActiveTill < DateTime.Now)
                {
                    logger.LogError($"Request for {context.Request.Path} has expired authorization token {authHeader} ");

                    await GenerateAuthResponse(context, ErrorCodes.TokenExpired);
                }
                else
                {
                    using (var db = new IR2016DB())
                    {
                        var user = db.Users.First(u => u.ID == AppState.Tokens[authHeader].UserId);

                        context.Items[MiRConsts.USER_BAG] = user;
                    }

                    await _next.Invoke(context);
                }
            }
            else
            {
                logger.LogError($"Request for {context.Request.Path} has not auth info in header item {MiRConsts.Authorization} ");
                await GenerateAuthResponse(context, ErrorCodes.NoAuthInfo);
            }
        }

        private static async Task GenerateAuthResponse(HttpContext context, ErrorCodes code)
        {
            var response = context.Response;
            response.StatusCode = 401; //Unauthorized
            response.ContentType = "application/json";
            await response.WriteAsync(new ErrorDetails()
            {
                Message = code.GetDescription(),
                ErrorCode = (int)code
            }.ToString());
        }
    }
}
