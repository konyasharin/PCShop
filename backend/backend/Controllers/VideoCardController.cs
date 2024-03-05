using backend.Entities;
using backend.UpdatedEntities;
using backend.Utils;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCardController : ComponentController
    {
        
        public VideoCardController(ILogger<VideoCardController> logger) : base(logger)
        {
        }

        [HttpPost("createVideoCard")]
        public async Task<IActionResult> CreateVideoCard([FromForm] VideoCard<IFormFile> videoCard)
        {
            try
            {
                string imagePath = BackupWriter.Write(videoCard.Image);
                
                if (videoCard.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (videoCard.Memory_db < 0 || videoCard.Memory_db > 10000)
                {
                    return BadRequest(new { error = "Memory_db must be between 0 and 10000" });
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var data = new
                    {
                        id = videoCard.Id,
                        brand = videoCard.Brand,
                        model = videoCard.Model,
                        country = videoCard.Country,
                        memory_db = videoCard.Memory_db,
                        memory_type = videoCard.Memory_type,
                        price = videoCard.Price,
                        description = videoCard.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    int id = connection.QuerySingleOrDefault<int>("INSERT INTO public.video_card (brand, model, country, memory_db, memory_type," +
                        "price, description, image)" +
                        "VALUES (@brand, @model, @country, @memory_db, @memory_type, @price, @description, @image) RETURNING id", data);
                    logger.LogInformation("VideoCard data saved to database");;
                    return Ok(new { id = id, data });
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"VideoCard data did not save in database. \nException: {ex}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getVideoCard/{id}")]
        public async Task<IActionResult> GetVideoCardById(int id)
        {
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var videoCard = connection.QueryFirstOrDefault<VideoCard<string>>("SELECT * FROM public.video_card WHERE Id = @Id",
                        new { Id = id });

                    if (videoCard != null)
                    {
                        logger.LogInformation($"Retrieved VideoCard with Id {id} from the database");
                        return Ok(new {id=id, videoCard});

                    }
                    else
                    {
                        logger.LogInformation($"VideoCard with Id {id} not found in the database");
                        return NotFound(new {error = $"VideoCard NotFound with {id}"});
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve VideoCard data from the database. \nException {ex}");
                return StatusCode(500, new { error = "Internal server error" });
            }
        }

        [HttpPut("updateVideoCard/{id}")]
        public async Task<IActionResult> UpdateVideoCard(int id, [FromForm] UpdatedVideoCard updatedVideoCard)
        {
            try
            {
                
                if (updatedVideoCard.Price < 0)
                {
                    return BadRequest(new { error = "Price must not be less than 0" });
                }

                if (updatedVideoCard.Memory_db < 0 || updatedVideoCard.Memory_db > 10000)
                {
                    return BadRequest(new { error = "Memory_db must be between 0 and 10000" });
                }

                string imagePath = string.Empty;
               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    string filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.video_card WHERE Id = @id");

                    if (updatedVideoCard.updated)
                    {

                        BackupWriter.Delete(filePath);
                        imagePath = BackupWriter.Write(updatedVideoCard.Image);
                    }
                    else
                    {
                        imagePath = filePath;
                    }

                    var data = new
                    {
                        id = id,
                        brand = updatedVideoCard.Brand,
                        model = updatedVideoCard.Model,
                        country = updatedVideoCard.Country,
                        memory_db = updatedVideoCard.Memory_db,
                        memory_type = updatedVideoCard.Memory_type,
                        price = updatedVideoCard.Price,
                        description = updatedVideoCard.Description,
                        image = imagePath,
                    };

                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("UPDATE public.video_card SET Brand = @brand, Model = @model, Country = @country, Memory_db = @memory_db," +
                        " Memory_type = @memory_db," +
                        " Price = @price, Description = @description, Image = @image WHERE Id = @id", data);

                    logger.LogInformation("VideoCard data updated in the database");

                    return Ok(new { id = id, data });
                }

                
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update VideoCard data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("deleteVideoCard/{id}")]
        public async Task<IActionResult> DeleteVideoCard(int id)
        {
            try
            {
                string filePath;
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    filePath = connection.QueryFirstOrDefault<string>("SELECT image FROM public.video_card WHERE Id = @id", new { Id = id });
                    BackupWriter.Delete(filePath);

                    connection.Execute("DELETE FROM public.video_card WHERE Id = @id", new { id });

                    logger.LogInformation("VideoCard data deleted from the database");

                    return Ok(new {id=id});
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete VideoCard data in database. \nException: {ex}");
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("getAllVideoCards")]
        public async Task<IActionResult> GetAllComputerCases(int limit, int offset)
        {
            logger.LogInformation("Get method has started");
            try
            {
                
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var videoCards = connection.Query<VideoCard<string>>("SELECT * FROM public.video_card LIMIT @Limit OFFSET @Offset",
                        new {Limit = limit, Offset = offset});

                    logger.LogInformation("Retrieved all VideoCard data from the database");

                    return Ok(new {data=videoCards});
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"VideoCard data did not get ftom database. Exception: {ex}");
                return NotFound(new {error = ex.Message});
            }
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchRam(string keyword)
        {
            try
            {
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var videoCard = connection.Query<VideoCard<string>>(@"SELECT * FROM public.video_card " +
                        "WHERE model LIKE @Keyword OR brand LIKE @Keyword " +
                        "LIMIT 3", new { Keyword = "%" + keyword + "%" });

                    return Ok(new { videoCard });

                }
            }
            catch (Exception ex)
            {
                logger.LogError("Error with search");
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
