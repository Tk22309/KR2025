using System;
using System.IO;

public static class Logger
{
    private static readonly string LogPath = "log.txt";

    public static void Log(string message)
    {
        File.AppendAllText(LogPath, $"[{DateTime.Now}] {message}\n");
    }

    public static void LogError(string message)
    {
        Log("ERROR: " + message);
    }
}
