using backend.Entities;

namespace backend.Utils
{
    public static class BackupWriter
    {
        public static string Write(string fileName, IFormFile file) {
            string currentTime = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            using (var fileStream = new FileStream(Path.Combine(@"./backup", currentTime + fileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return currentTime + fileName;
        }
    }
}
