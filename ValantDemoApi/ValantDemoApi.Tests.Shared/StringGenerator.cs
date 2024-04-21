namespace ValantDemoApi.Tests.Shared;

public static class StringGenerator
{
  private static readonly Random _random = new();

  public static string GenerateRandomString()
  {
    int minLength = 2, maxLength = 25;
    const string chars = "SOXE";
    int length = _random.Next(minLength, maxLength + 1);
    char[] randomChars = new char[length];

    for (int i = 0; i < length; i++)
    {
      randomChars[i] = chars[_random.Next(chars.Length)];
    }

    return new(randomChars);
  }
}
