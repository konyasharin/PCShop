using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SsdController : ControllerBase
    {
        private readonly ILogger<SsdController> logger;

        public SsdController(ILogger<SsdController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createssd")]
        public async void CreateSsd(SSD ssd)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = ssd.Id,
                        brand = ssd.Brand,
                        model = ssd.Model,
                        country = ssd.Country,
                        capacity = ssd.Capacity,
                        price = ssd.Price,
                        description = ssd.Description,
                        image = ssd.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.ssd (Id, Brand, Model, Country, Capacity," +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Capacity, @Price, @Description, @Image)", ssd);
                    logger.LogInformation("SSD data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("SSD data did not save in database");
            }
        }
    }
}
