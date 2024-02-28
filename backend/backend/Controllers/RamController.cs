using backend.Entities;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace backend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RamController : ControllerBase
    {
        private readonly ILogger<RamController> logger;

        public RamController(ILogger<RamController> logger)
        {
            this.logger = logger;


        }

        [HttpPost("createram")]
        public async void CreateRam(RAM ram)
        {

            try
            {
                DotNetEnv.Env.Load();
                var connectionString = Environment.GetEnvironmentVariable("ConnectionString");

                await using var connection = new NpgsqlConnection(connectionString);
                {
                    var parameters = new
                    {
                        id = ram.Id,
                        brand = ram.Brand,
                        model = ram.Model,
                        country = ram.Country,
                        frequency = ram.Frequency,
                        timings = ram.Timings,
                        capacity_db = ram.Capacity_db,
                        price = ram.Price,
                        description = ram.Description,
                        image = ram.Image,

                    };

                    connection.Open();
                    logger.LogInformation("Connection started");
                    connection.Execute("INSERT INTO public.ram (Id, Brand, Model, Country, Frequency, Timings, Capacity_db" +
                        "Price, Description, Image)" +
                        "VALUES (@Id, @Brand, @Model, @Country, @Frequency, @Timings, @Capacity_db, @Price, @Description, @Image)", ram);
                    logger.LogInformation("Ram data saved to database");
                }
            }
            catch (Exception ex)
            {
                logger.LogError("Ram data did not save in database");
            }
        }
    }
}
