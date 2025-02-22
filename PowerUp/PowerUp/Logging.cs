using Microsoft.Extensions.Logging;

namespace PowerUp
{
  public static class Logging
  {
    public static ILogger Logger { get; private set; } = null!;

    public static void Initialize(ILogger logger)
    {
      Logger = logger;
    }
  }
}
