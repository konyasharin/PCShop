using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCardController : ControllerBase
    {
        private readonly ILogger<VideoCardController> logger;

        public VideoCardController(ILogger<VideoCardController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createVideoCard")]
        public async Task<IActionResult> CreateVideoCard(VideoCard videoCard)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (videoCard.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                if (videoCard.Memory_db < 0 || videoCard.Memory_db > 10000)
                {
                    return BadRequest("Memory_db must be between 0 and 10000");
                }

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = videoCard.Id,
                        brand = videoCard.Brand,
                        model =videoCard.Model,
                        country = videoCard.Country,
                        memory_db = videoCard.Memory_db,
                        memory_type = videoCard.Memory_type,
                        price =videoCard.Price,
                        description = videoCard.Description,
                        image = videoCard.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.video_card (Id, Brand, Model, Country, Memory_db, Memory_type," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Memory_db, @Memory_type, @Price, @Description, @Image)", videoCard);

                    logger.LogInformation("VideoCard data saved to database");

                    String result = "VideoCard data saved to database";
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"VideoCard data did not save in database. \nException: {ex}");
                return BadRequest(ex);
            }
        }

        [HttpGet("GetVideoCard/{id}")]
        public async Task<IActionResult> GetVideoCardById(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");


                    var videoCard = connection.QueryFirstOrDefault<VideoCard>("SELECT * FROM public.video_card WHERE Id = @Id",
                        new { Id = id });

                    if (videoCard != null)
                    {
                        logger.LogInformation($"Retrieved VideoCard with Id {id} from the database");
                        return Ok(videoCard);

                    }
                    else
                    {
                        logger.LogInformation($"VideoCard with Id {id} not found in the database");
                        return NotFound();
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to retrieve VideoCard data from the database. \nException {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateVideoCard/{id}")]
        public async Task<IActionResult> UpdateVideoCard(int id, VideoCard updatedVideoCard)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                if (updatedVideoCard.Price < 0)
                {
                    return BadRequest("Price must not be less than 0");
                }

                if (updatedVideoCard.Memory_db < 0 || updatedVideoCard.Memory_db > 10000)
                {
                    return BadRequest("Memory_db must be between 0 and 10000");
                }

               
                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = id,
                        brand = updatedVideoCard.Brand,
                        model = updatedVideoCard.Model,
                        country = updatedVideoCard.Country,
                        memory_db = updatedVideoCard.Memory_db,
                        memory_type = updatedVideoCard.Memory_type,
                        price = updatedVideoCard.Price,
                        description = updatedVideoCard.Description,
                        image = updatedVideoCard.Image
                    };

                }

                connection.Open();
                logger.LogInformation("Connection started");

                connection.Execute("UPDATE public.video_card SET Brand = @brand, Model = @model, Country = @country, Memory_db = @memory_db," +
                    " Memory_type = @memory_db," +
                    " Price = @price, Description = @description, Image = @image WHERE Id = @id", updatedVideoCard);

                logger.LogInformation("VideoCard data updated in the database");

                return Ok("VideoCard data updated successfully");
            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to update VideoCard data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("DeleteVideoCard/{id}")]
        public async Task<IActionResult> DeleteVideoCard(int id)
        {
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    connection.Execute("DELETE FROM public.video_card WHERE Id = @id", new { id });

                    logger.LogInformation("VideoCard data deleted from the database");

                    return Ok("VideoCard data deleted successfully");
                }

            }
            catch (Exception ex)
            {
                logger.LogError($"Failed to delete VideoCard data in database. \nException: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetAllVideoCards")]
        public async Task<IActionResult> GetAllComputerCases()
        {
            logger.LogInformation("Get method has started");
            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    connection.Open();
                    logger.LogInformation("Connection started");

                    var videoCards = connection.Query<VideoCard>("SELECT * FROM public.video_card");

                    logger.LogInformation("Retrieved all VideoCard data from the database");

                    return Ok(videoCards);
                }


            }
            catch (Exception ex)
            {
                logger.LogError($"VideoCard data did not get ftom database. Exception: {ex}");
                return NotFound();
            }
        }
    }
}
