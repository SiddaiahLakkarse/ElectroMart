using System.Net;
using System.Text.Json;
namespace ElectroMart.API.Middleware;
public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> log){public async Task Invoke(HttpContext ctx){try{await next(ctx);}catch(Exception ex){log.LogError(ex,"Unhandled request error");ctx.Response.StatusCode=ex switch{KeyNotFoundException=>404,InvalidOperationException=>400,_=>500};ctx.Response.ContentType="application/json";await ctx.Response.WriteAsync(JsonSerializer.Serialize(new{error=ex.Message}));}}}
