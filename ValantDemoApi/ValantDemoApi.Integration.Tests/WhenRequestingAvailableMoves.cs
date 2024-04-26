using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace ValantDemoApi.Integration.Tests;

[TestFixture]
public sealed class WhenRequestingAvailableMoves : BaseTest
{
  [Test]
  public async Task WhenRequestingAvailableMoves_ThenReturnsAvailableOnes()
  {
    // Act
    var result = await _client.GetAsync("/availableMoves");
    result.EnsureSuccessStatusCode();

    // Assert
    var content = JsonConvert.DeserializeObject<string[]>(await result.Content.ReadAsStringAsync());
    content.Should().Contain("Up");
    content.Should().Contain("Down");
    content.Should().Contain("Left");
    content.Should().Contain("Right");
  }
}
