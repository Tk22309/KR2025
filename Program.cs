using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Введіть повний шлях до файлу, який потрібно завантажити в Google Drive:");
        string? input = Console.ReadLine();
        string path = input ?? string.Empty;
        if (string.IsNullOrWhiteSpace(path) || !System.IO.File.Exists(path))
        {
            Console.WriteLine(" Файл не знайдено або шлях некоректний.");
            return;
        }
        var uploader = new GoogleDriveUploader();
        try
        {
            await uploader.UploadFileAsync(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Помилка під час завантаження: {ex.Message}");
        }
    }
}
