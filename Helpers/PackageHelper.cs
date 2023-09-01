using ArmstrongServer.Constants;
using ArmstrongServer.Models;

namespace ArmstrongServer.Helpers
{
  public static class PackageHelper
  {
    public static Packages GetPackages(byte address)
    {
      var workMode = new WorkMode
      {
        SilenMode = GeneratePackage(address, Bytes.WorkMode.Function, Bytes.WorkMode.Mode.Silent),
        FrequencyMode = GeneratePackage(address, Bytes.WorkMode.Function, Bytes.WorkMode.Mode.Frequency),
        TimingMode = GeneratePackage(address, Bytes.WorkMode.Function, Bytes.WorkMode.Mode.Time),
        GetMode = GeneratePackage(address, Bytes.WorkMode.Function, Bytes.WorkMode.Mode.GetMode),
      };

      var operation = new Operation
      {
        OpenBlanker = GeneratePackage(address, Bytes.Service.Function, Bytes.Service.Action.Write, Bytes.Service.Operation.OpenBlanker),
        CloseBlanker = GeneratePackage(address, Bytes.Service.Function, Bytes.Service.Action.Write, Bytes.Service.Operation.CloseBlanker),
        StartRewind = GeneratePackage(address, Bytes.Service.Function, Bytes.Service.Action.Write, Bytes.Service.Operation.StartRewind),
        RewindAndBlanker = GeneratePackage(address, Bytes.Service.Function, Bytes.Service.Action.Write, Bytes.Service.Operation.RewindAndBlanker),
        GetResult = GeneratePackage(address, Bytes.Service.Function, Bytes.Service.Action.Read, Bytes.Service.Operation.GetResult),
      };

      var lightAlert = new LightAlert
      {
        Normal = GeneratePackage(address, Bytes.LightAlert.Function, Bytes.LightAlert.Light.Normal, Bytes.LightAlert.SpecialSignal.Off),
        Warning = GeneratePackage(address, Bytes.LightAlert.Function, Bytes.LightAlert.Light.Warning, Bytes.LightAlert.SpecialSignal.Off),
        Danger = GeneratePackage(address, Bytes.LightAlert.Function, Bytes.LightAlert.Light.Danger, Bytes.LightAlert.SpecialSignal.Off),
        SpecialSignal = GeneratePackage(address, Bytes.LightAlert.Function, Bytes.LightAlert.Light.Normal, Bytes.LightAlert.SpecialSignal.On),

      };

      var packages = new Packages
      {
        Fetch = GeneratePackage(address, Bytes.Fetch.Function),
        WorkMode = workMode,
        Operation = operation,
        LightAlert = lightAlert,
      };

      return packages;
    }

    private static byte[] GeneratePackage(byte address, byte function)
    {
      var package = new byte[] { address, function, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
      package = VerifivationPackageHelper.GetVerifiedPackage(package);

      return package;
    }

    private static byte[] GeneratePackage(byte address, byte function, byte action)
    {
      var package = new byte[] { address, function, action, 0x00, 0x00, 0x00, 0x00, 0x00 };
      package = VerifivationPackageHelper.GetVerifiedPackage(package);

      return package;
    }

    private static byte[] GeneratePackage(byte address, byte function, byte action, byte operationType)
    {
      var package = new byte[] { address, function, action, operationType, 0x00, 0x00, 0x00, 0x00 };
      package = VerifivationPackageHelper.GetVerifiedPackage(package);

      return package;
    }
  }
}
