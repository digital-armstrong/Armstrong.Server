using ArmstrongServer.Models;

namespace ArmstrongServer
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      System.Console.WriteLine("ARMStrong Server  Copyright (C) 2022  Owlscatcher.\nThis program comes with ABSOLUTELY NO WARRANTY.\nThis is free software, and you are welcome to redistribute it under certain conditions.\n\n");

      var port = new ComPort();

      List<Channel> channels = new List<Channel>
      {
        new Channel(1, "ДК-100", port),
      };

      while (true)
      {
        port.Open();

        foreach (var c in channels)
        {
          c.SendMessage(c.Packages.Fetch);
          Thread.Sleep(200);
          c.ReceiveMessage();
          c.SaveEventValue();
          c.PrintChannelInfo();
          Thread.Sleep(1000);
        }

        port.Close();
      }
    }
  }
}
