using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ValantDemoApi.Middleware;

public class GlobalExceptionMiddleware : IMiddleware
{
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      // Call the next middleware in the pipeline
      await next(context);
    }
    catch (Exception ex)
    {
      // Handle the exception and return an appropriate response
      await HandleExceptionAsync(context, ex);
    }
  }

  private static Task HandleExceptionAsync(HttpContext context, Exception exception)
  {
    // Log the exception
    Console.WriteLine($"An unhandled exception occurred: {exception}");

    var response = new { error = "Sorry, we cant process your request at the moment, please try later" };
    string jsonResponse = System.Text.Json.JsonSerializer.Serialize(response);
    context.Response.ContentType = "application/json";
    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    return context.Response.WriteAsync(jsonResponse);
  }
}
