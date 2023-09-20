using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Helpers
{
  public static class SettingsHelper
  {
    public static IConfigurationRoot? GetConfiguration()
    {
      var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json", false);
      var configuration = builder.Build();

      return configuration;
    }
  }
}
