using AutoFixture;
using AutoFixture.Xunit2;

namespace ValantDemoApi.Tests.Shared;

public sealed class AutoMoqDataAttribute : AutoDataAttribute
{
  public AutoMoqDataAttribute() : base(CreateFixture)
  {
  }

  private static IFixture CreateFixture() =>
      new Fixture();
}
