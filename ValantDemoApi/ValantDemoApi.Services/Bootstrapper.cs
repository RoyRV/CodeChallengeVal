using Microsoft.Extensions.DependencyInjection;
using ValantDemoApi.Services.Interfaces;

namespace ValantDemoApi.Services;

public static class Bootstrapper
{
  public static void AddServices(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped<IUploadMazeService, UploadMazeService>();
    serviceCollection.AddScoped<IMazeService, MazeService>();
  }
}
