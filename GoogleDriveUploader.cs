using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

public class GoogleDriveUploader
{
    private static readonly string[] Scopes = { DriveService.Scope.DriveFile };
    private const string ApplicationName = "CloudBackupKursova";

    public async Task UploadFileAsync(string filePath)
    {
        string credentialsPath = "credentials/credentials.json";
        string tokenPath = "token.json";

        if (!File.Exists(credentialsPath))
        {
            Console.WriteLine("Файл credentials.json не знайдено.");
            return;
        }

        UserCredential credential;
        using (var stream = new FileStream(credentialsPath, FileMode.Open, FileAccess.Read))
        {
            var secrets = await GoogleClientSecrets.FromStreamAsync(stream);
            credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                secrets.Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(tokenPath, true));
        }

        var service = new DriveService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        });

        var fileMetadata = new Google.Apis.Drive.v3.Data.File()
        {
            Name = Path.GetFileName(filePath)
        };

        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            var request = service.Files.Create(fileMetadata, stream, "application/octet-stream");
            request.Fields = "id";
            await request.UploadAsync();

            var file = request.ResponseBody;
            Console.WriteLine($" Файл завантажено: https://drive.google.com/file/d/{file.Id}/view");
        }
    }
}
