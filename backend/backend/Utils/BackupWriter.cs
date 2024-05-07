using backend.Entities;

namespace backend.Utils
{
    public static class BackupWriter
    {
        /// <summary>
        /// Записывает файл на сервер и возвращает его имя с добавлением текущего времени.
        /// </summary>
        /// <param name="file">Файл для записи.</param>
        /// <returns>Имя файла с добавлением текущего времени.</returns>
        public static string Write(IFormFile file) {
            string currentTime = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            using (var fileStream = new FileStream(Path.Combine(@"./backup", currentTime + file.FileName), FileMode.Create))
            {
                file.CopyTo(fileStream);
            }
            return currentTime + file.FileName;
        }
        
        /// <summary>
        /// Удаляет файл с указанным путем, если он существует.
        /// </summary>
        /// <param name="filePath">Путь к файлу для удаления.</param>
        public static void Delete(string filePath)
        {
            if (File.Exists(Path.Combine(@"./backup", filePath)))
            {
                File.Delete(Path.Combine(@"./backup", filePath));
            }
        }

    }
}
