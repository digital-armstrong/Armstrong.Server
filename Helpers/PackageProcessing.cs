using ArmstrongServer.Models;
using ArmstrongServer.Constants;

namespace ArmstrongServer.Helpers
{
  static public class PackageProcessing
  {
    /// <summary>
    /// Get string statements for channel following measurement values
    /// </summary>
    /// <param name="channel">Channel object</param>
    /// <returns>String state value</returns>
    public static string GetChannelState(Channel channel)
    {
      var systemValue = channel.EventSystemValue;
      var preEmgLimit = channel.PreEmergencyLimit;
      var emgLimit = channel.EmergencyLimit;
      var isOnline = channel.IsOnline;
      var errorCount = channel.EventErrorCount;
      var errorLimit = 60;

      if (isOnline)
      {
        if (errorCount < errorLimit)
        {
          if (systemValue < preEmgLimit)
            return ChannelState.Normal;
          else if (systemValue >= preEmgLimit && systemValue < emgLimit)
            return ChannelState.Warning;
          else
            return ChannelState.Danger;
        }
        else
        {
          return ChannelState.LineDown;
        }
      }
      else
      {
        return ChannelState.Offline;
      }
    }
  }
}
