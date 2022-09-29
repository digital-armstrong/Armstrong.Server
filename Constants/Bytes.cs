namespace ArmstrongServer.Constants
{
  public static class Bytes
  {
    public static byte SEZE_ERROR => 0xFF;
    public static byte CRC_ERROR => 0xFE;

    public static class Fetch
    {
      public static byte Function => 0x03;
    }

    public static class WorkMode
    {
      public static byte Function => 0x01;

      public static class Mode
      {
        public static byte Silent => 0x00;
        public static byte Frequency => 0x01;
        public static byte Time => 0x02;
        public static byte GetMode => 0x03;
      }
    }

    public static class Service
    {
      public static byte Function => 0x02;

      public static class Action
      {
        public static byte Write => 0x01;
        public static byte Read => 0x02;
      }

      public static class Operation
      {
        public static byte OpenBlanker => 0x01;
        public static byte CloseBlanker => 0x00;
        public static byte StartRewind => 0x02;
        public static byte RewindAndBlanker => 0x03;
        public static byte GetResult => 0x00;
      }
      public static class Result
      {
        public static byte OK => 0x01;
        public static byte ERROR => 0x00;
      }
    }

    public static class LightAlert
    {
      public static byte Function => 0x04;

      public static class Light
      {
        public static byte Normal => 0x03;
        public static byte Warning => 0x02;
        public static byte Danger => 0x01;
      }

      public static class SpecialSignal
      {
        public static byte On => 0x01;
        public static byte Off => 0x00;
      }
    }

    public static class SetAddress
    {
      public static byte Function => 0x09;
    }
  }
}
