using System;
using System.Threading.Tasks;

public class BackupManager
{
    public async Task BackupFile(string path)
    {
        if (!FileAccessModule.FileExists(path))
        {
            Logger.LogError("Файл не знайдено.");
            return;
        }

        var fileData = FileAccessModule.ReadFile(path);
        var key = EncryptionModule.GenerateKey();
        var iv = EncryptionModule.GenerateIV();
        var encrypted = EncryptionModule.Encrypt(fileData, key, iv);

        var tempFile = Path.GetTempFileName();
        await File.WriteAllBytesAsync(tempFile, encrypted);

        var uploader = new GoogleDriveUploader();
        await uploader.UploadFileAsync(tempFile);

        File.Delete(tempFile); // очищаємо тимчасовий файл


        Logger.Log("Файл успішно збережено.");
    }
}
