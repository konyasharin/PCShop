using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[Route("api/backup")]
[ApiController]
public class BackupController: ControllerBase
{
    [HttpGet("getImage")]
    public IActionResult GetImage(string imageName)
    {
        // Считываем содержимое файла в массив байтов
        byte[] fileBytes = System.IO.File.ReadAllBytes(Path.Combine(@"./backup", imageName));

        // Возвращаем файл в формате blob
        return Ok(new {file = File(fileBytes, "image/jpeg")});
    }
}