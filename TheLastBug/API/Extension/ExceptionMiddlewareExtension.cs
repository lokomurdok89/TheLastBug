
using System.Runtime.CompilerServices;
using System.Net;
using Domain;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;

namespace API.Extension
{
    public static class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app) {
            app.UseExceptionHandler(appError => {
                appError.Run(async conext =>
                {
                    conext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    conext.Response.ContentType = "application/json";
                    var contextFeature = conext.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature!=null) {                        
                        //await conext.Response.WriteAsync(JsonConvert.SerializeObject(new BasicResponse<string?> (conext.Response.StatusCode)));
                        await conext.Response.WriteAsync(JsonConvert.SerializeObject(new BasicResponse<string?> (conext.Response.StatusCode,contextFeature.Error.Message)));
                    }
                });
            });
          }
           public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app) {
            app.UseMiddleware(typeof(RequestResponseLoggingMiddleware));
        }
    }
}