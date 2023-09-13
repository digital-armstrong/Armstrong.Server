using System.ComponentModel;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Models.ConfigModels
{
  public static class AppSettings
  {
    public static GeneralSettings AppGeneralSettings { get; set; }
    public static SqlConnectionSettings AppSqlConnectionSettings { get; set; }

    public static void Initialization()
    {
      var config = SettingsHelper.GetConfiguration();
      var generalSettingsCfg = config.GetSection("GeneralSettings")
                              .Get<GeneralSettings>();
      var sqlConnectionSettingsCfg = config.GetSection("SqlConnectionSettings")
                              .Get<SqlConnectionSettings>();

      AppGeneralSettings = new GeneralSettings
      {
        AuthToken = generalSettingsCfg.AuthToken,
        ChannelPollingErrorCountLimit = generalSettingsCfg.ChannelPollingErrorCountLimit,
        ServerProps = generalSettingsCfg.ServerProps
      };

      AppSqlConnectionSettings = new SqlConnectionSettings
      {
        Host = sqlConnectionSettingsCfg.Host,
        Username = sqlConnectionSettingsCfg.Username,
        Password = sqlConnectionSettingsCfg.Password,
        Database = sqlConnectionSettingsCfg.Database
      };
    }
  }
}
