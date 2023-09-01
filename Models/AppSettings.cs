using System.ComponentModel;
using ArmstrongServer.Helpers;
using Microsoft.Extensions.Configuration;

namespace ArmstrongServer.Models
{
  public static class AppSettings
  {
    public static GeneralSettings AppGeneralSettings { get; set; }
    public static PortSettings AppPortSettings { get; set; }
    public static ServerAttributes AppServerAttributes { get; set; }
    public static SqlConnectionSettings AppSqlConnectionSettings { get; set; }

    public static void Initialization()
    {
      var config = SettingsHelper.GetConfiguration();
      var srvAttrCfg = config.GetSection("ServerAttributes")
                              .Get<ServerAttributes>();
      var generalSettingsCfg = config.GetSection("GeneralSettings")
                              .Get<GeneralSettings>();
      var portSettingCfg = config.GetSection("PortSettings")
                              .Get<PortSettings>();
      var sqlConnectionSettingsCfg = config.GetSection("SqlConnectionSettings")
                              .Get<SqlConnectionSettings>();

      AppServerAttributes = new ServerAttributes
      {
        Id = srvAttrCfg.Id,
        Name = srvAttrCfg.Name,
      };

      AppGeneralSettings = new GeneralSettings
      {
        AuthToken = generalSettingsCfg.AuthToken,
        ChannelPolingTimeout = generalSettingsCfg.ChannelPolingTimeout,
        ChannelPollingErrorCountLimit = generalSettingsCfg.ChannelPollingErrorCountLimit,
        MultiportMode = generalSettingsCfg.MultiportMode,
        MultiportCollection = generalSettingsCfg.MultiportCollection
      };

      AppPortSettings = new PortSettings
      {
        PortName = portSettingCfg.PortName,
        BaudRate = portSettingCfg.BaudRate,
        DeadPollingTime = portSettingCfg.DeadPollingTime,
        MinimalPollingTimeout = portSettingCfg.MinimalPollingTimeout
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
