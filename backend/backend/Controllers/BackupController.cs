using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/backup")]
[ApiController]
public class BackupController: ControllerBase
{
    /// <summary>
    /// Метод для получения изображения из резервной копии.
    /// </summary>
    /// <param name="imageName">Имя изображения.</param>
    /// <returns>Изображение в формате blob.</returns>
    [HttpGet("getImage")]
    public IActionResult GetImage(string imageName)
    {
        // Считываем содержимое файла в массив байтов
        byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(@"./backup", imageName));

        // Возвращаем файл в формате blob
        return Ok(new {file = File(fileBytes, "image/jpeg")});
    }
}