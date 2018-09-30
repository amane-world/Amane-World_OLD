using System;

namespace Logger
{
  class Log
  {
    private const string LogTimeFormat = "yyyy-MM-dd h:mm:ss";

    public static void info(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.WriteLine($"[{nowTime}][INFO] {text}");
    }
    public static void warn(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.WriteLine($"[{nowTime}][WARN] {text}");
    }
    public static void err(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.WriteLine($"[{nowTime}][ERR] {text}");
    }
    public static void notice(string text)
    {
      string nowTime = DateTime.Now.ToString(LogTimeFormat);
      Console.WriteLine($"[{nowTime}][NOTICE] {text}");
    }
  }
}
