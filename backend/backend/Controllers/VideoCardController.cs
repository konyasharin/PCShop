using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VideoCardController : ControllerBase
    {
        private readonly ILogger<VideoCardController> logger;

        public VideoCardController(ILogger<VideoCardController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createvideocard")]
        public async void CreateVideoCard(VideoCard videoCard)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = videoCard.Id,
                        brand = videoCard.Brand,
                        model =videoCard.Model,
                        country = videoCard.Country,
                        memory_db = videoCard.Memoty_db,
                        memory_type = videoCard.Memory_type,
                        price =videoCard.Price,
                        description = videoCard.Description,
                        image = videoCard.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.videocard (Id, Brand, Model, Country, Memory_db, Memory_type" +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Memory_db, @Memory_type, @Price, @Description, @Image)", videoCard);
                    logger.LogInformation("VideoCard data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("VideoCard data did not save in database");
            }
        }
    }
}
