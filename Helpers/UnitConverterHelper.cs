using ArmstrongServer.Models;
using ArmstrongServer.Constants;

namespace ArmstrongServer.Helpers
{
  public static class UnitConverterHelper
  {
    enum Type { EqualDoseRate = 1, GasesActivity, AerosolsActivity, ImpulsesPerSecond }

    static public double ToImpulse(byte[] message)
    {
      double impulse = 0;

      if (!message.SequenceEqual(new byte[] { Bytes.CRC_ERROR })
          && !message.SequenceEqual(new byte[] { Bytes.SEZE_ERROR }))
      {
        impulse = BitConverter.ToSingle(message, 2);

        return impulse;
      }
      else
      {
        System.Console.WriteLine($"Com-port ERROR: {BitConverter.ToString(message)}");
        return impulse;
      }
    }

    static public double ToSystem(int? type, double coefficient, double impulse)
    {
      //BDMG coefficient = 1, BDGB coefficient = 0.0000019f, BDAS coefficient = 2.0592f;

      switch (type)
      {
        case (int)Type.EqualDoseRate:
          return impulse * coefficient * 0.001f;                      // type: 1  БДМГ    мкЗв/ч
        case (int)Type.GasesActivity:
          return impulse != 0 ? 1 / (impulse * coefficient) : 0;      // type: 2  БДГБ    Бк/м³
        case (int)Type.AerosolsActivity:
          return impulse != 0 ? impulse / coefficient : 0;            // type: 3  БДАС    Бк/м³
        case (int)Type.ImpulsesPerSecond:
          return impulse != 0 ? impulse * coefficient : 0;            // type: 4  БДБ     имп/с
        default: return 1;
      }
    }

    static public double ToNotSystem(int? type, double value)
    {
      // Пересчет из мкЗв/ч в мкР/с и Бк/м.куб в Ки/л
      // 1 мкЗв/ч     = 27.777        мкР/с
      // 1 Бк/м.куб   = 370000000000  Ки/л

      double curie = 37000000000000;
      double roentgen = 27.777f;

      switch (type)
      {
        case (int)Type.EqualDoseRate: return value * roentgen;                // type: 1  БДМГ    мкЗв/ч
        case (int)Type.GasesActivity: return value / curie;                   // type: 2  БДГБ    Бк/м³
        case (int)Type.AerosolsActivity: return value / curie;                // type: 3  БДАС    Бк/м³
        default: return value;
      }
    }
  }
}
