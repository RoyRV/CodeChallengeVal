
using Microsoft.Extensions.DependencyInjection;
using ValantDemoApi.Repository.Interfaces;

namespace ValantDemoApi.Repository;

public static class Bootstrapper
{
  public static void AddRepositories(this IServiceCollection serviceCollection)
  {
    serviceCollection.AddScoped<IMazeRepository, MazeRepository>();
    serviceCollection.AddScoped<IFileManager, FileManager>();
  }
}
