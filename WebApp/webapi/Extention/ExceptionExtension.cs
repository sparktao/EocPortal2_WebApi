using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Extention
{
    /// <summary>
    /// 全局自定义的处理异常的句柄
    /// </summary>
    public static class ExceptionExtension
    {
        public static void UseMyExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory) {
            app.UseExceptionHandler(builder => {
                builder.Run(async context => {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/json";


                    var ex = context.Features.Get<IExceptionHandlerFeature>();
                    if(ex!= null)
                    {
                        var _loggger = loggerFactory.CreateLogger("webapi.Extention.ExceptionExtension");
                        _loggger.LogError(500, ex.Error, ex.Error.Message);
                    }

                    await context.Response.WriteAsync(ex?.Error?.Message??"An Error Occurred.");
                });
            });
        }
    }
}
