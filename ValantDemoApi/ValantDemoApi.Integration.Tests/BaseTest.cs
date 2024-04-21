using NUnit.Framework;
using System.Net.Http;
using ValantDemoApi.Tests;

namespace ValantDemoApi.Integration.Tests;

public abstract class BaseTest
{
  protected HttpClient _client;

  [OneTimeSetUp]
  public void Setup()
  {
    var factory = new APIWebApplicationFactory();
    _client = factory.CreateClient();
  }
}
