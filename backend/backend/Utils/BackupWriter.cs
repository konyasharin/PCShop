using backend.Entities;

namespace backend.Utils
{
    public static class BackupWriter
    {
        public static string Write(IFormFile file) {
            string currentTime = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            using (var fileStream = new FileStream(Path.Combine(@"./backup", currentTime + file.FileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return currentTime + file.FileName;
        }

        public static void Delete(string filePath)
        {
            if (File.Exists(Path.Combine(@"./backup", filePath)))
            {
                File.Delete(Path.Combine(@"./backup", filePath));
            }
        }

    }
}
