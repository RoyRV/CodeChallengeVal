using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ValantDemoApi.Integration.Tests;

[TestFixture]
public sealed class WhenRequestingAllMazes : BaseTest
{
  private readonly string MAZES_PATH = "\\ValantDemoApi\\mazes\\";

  [Test]
  public async Task WhenRequestingAvailableMoves_ThenReturnsAvailableOnes()
  {
    // Act
    var result = await _client.GetAsync("/all");

    // Assert
    result.EnsureSuccessStatusCode();
    var content = JsonConvert.DeserializeObject<string[]>(await result.Content.ReadAsStringAsync());
    // the directory is getting the values from the Integration tests
    // TODO : check how to fix this

  } 
}
