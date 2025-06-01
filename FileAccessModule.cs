using System.IO;

public class FileAccessModule
{
    public static bool FileExists(string path) => File.Exists(path);
    public static byte[] ReadFile(string path) => File.ReadAllBytes(path);
    public static FileInfo GetFileInfo(string path) => new FileInfo(path);
}
