
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace ValantDemoApi.Controllers;

public static class Bootstrapper
{
  public static void AddValidators(this IServiceCollection services)
  {
    services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    services.AddControllers().AddFluentValidation();
  }
}
