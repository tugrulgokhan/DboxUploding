using System;
using System.Threading.Tasks;
using Dropbox.Api;
using System.IO;
using Dropbox.Api.Files;


namespace TRY_VS_1
{
    class Program
    {
        static string token = "...";

        static void Main(string[] args)
        {
            var task = Task.Run((Func<Task>)Program.Run);

            Console.ReadLine();
        }

        static async Task Run()
        {

            using (var dbx = new DropboxClient(token))
            {
                string file = @"...";
                string fileNa = Path.GetFileName(file);

                string fileName = "postApp.jpg";
                string folder = "/DB";
                string url = "";

                using (var memory = new MemoryStream(File.ReadAllBytes(fileNa)))
                {
                    var updated = dbx.Files.UploadAsync(folder + "/" + fileName, WriteMode.Overwrite.Instance, body: memory);
                    updated.Wait();
                    var tx = dbx.Sharing.CreateSharedLinkWithSettingsAsync(folder + "/" + fileName);
                    tx.Wait();
                    url = tx.Result.Url;

                }

                Console.WriteLine(url);
            }
        }
    }
}

